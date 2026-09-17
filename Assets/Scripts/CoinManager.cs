using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
        UpdateCoins(0); // Initialize the coin display
    }

    public void UpdateCoins(int changeAmount)
    {
        coins += changeAmount;

        coinText.text = coins.ToString();
    }
}
