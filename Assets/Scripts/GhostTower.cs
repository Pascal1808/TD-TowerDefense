using UnityEngine;

public class GhostTower : MonoBehaviour
{
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0.5f; // Set transparency
        sr.color = c;
    }

    public void SetValid(bool valid)
    {
        sr.color = valid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f); // Green for valid, red for invalid
    }
}
