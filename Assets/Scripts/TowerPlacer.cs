  using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    public Tilemap PlacementMap;
    public Tilemap NonPlacementMap;

    public GameObject ghostPrefab;

    private HashSet<Vector3Int> occupiedtiles = new HashSet<Vector3Int>();
    private GameObject ghostInstance;

    void Update()
    {
        HandlePlacementHover();
        HandlePlacementClick();
    }

    void HandlePlacementHover()
    {
        if (TowerSelectionUI.SelectedTowerPrefab == null)
        {
            if (ghostInstance != null)
                Destroy(ghostInstance);
                return;
        }

        if (ghostInstance == null)
            ghostInstance = Instantiate(ghostPrefab);

        ghostInstance.GetComponent<SpriteRenderer>().sprite = TowerSelectionUI.SelectedTowerPrefab.GetComponent<SpriteRenderer>().sprite;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Ensure the ghost is at the correct z position

        Vector3Int cellPosition = PlacementMap.WorldToCell(mouseWorldPos);

        Vector3 worldCenter = PlacementMap.GetCellCenterWorld(cellPosition);
        worldCenter.z = 0; // Ensure the ghost is at the correct z position

        ghostInstance.transform.position = worldCenter + new Vector3(0, PlacementMap.cellSize.y * 0.25f); // Slightly above the tilemap for visibility

        bool valid = PlacementMap.GetTile(cellPosition) != null && !occupiedtiles.Contains(cellPosition);

        ghostInstance.GetComponent<GhostTower>().SetValid(valid);
    }

    void HandlePlacementClick()
    {
        if(!Input.GetMouseButtonDown(0))return;
        if(TowerSelectionUI.SelectedTowerPrefab == null) return;

        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) 
        return; // Prevent placement when clicking on UI

         Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // Ensure the ghost is at the correct z position

        Vector3Int cellPosition = PlacementMap.WorldToCell(mouseWorldPos);

        if(!PlacementMap.HasTile(cellPosition)) return;
        if(occupiedtiles.Contains(cellPosition)) return;

        Instantiate(TowerSelectionUI.SelectedTowerPrefab, ghostInstance.transform.position, Quaternion.identity);

        CoinManager.instance.UpdateCoins(-TowerSelectionUI.SelectedTowerPrefab.GetComponent<Tower>().towerPrice); // Deduct the tower price from coins

        TowerSelectionUI.SelectedTowerPrefab = null; // Deselect the tower after placement

        occupiedtiles.Add(cellPosition);
    }
}
