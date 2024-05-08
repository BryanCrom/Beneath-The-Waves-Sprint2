using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCoinCount(int coins)
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins;
        }
        else
        {
            Debug.LogError("Coin text component not set in UIManager.");
        }
    }

    public void UpdateAmmoCount(int bullets)
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + bullets;
        }
        else
        {
            Debug.LogError("Ammo text component not set in UIManager.");
        }
    }

    public static void SafeUpdateCoinCount(int coins)
    {
        if (Instance != null)
        {
            Instance.UpdateCoinCount(coins);
        }
        else
        {
            Debug.LogError("UIManager instance is null when trying to update coins.");
        }
    }

    public static void SafeUpdateAmmoCount(int ammo)
    {
        if (Instance != null && Instance.ammoText != null)
        {
            Instance.ammoText.text = "Ammo: " + ammo;
        }
        else
        {
            Debug.LogError("UIManager instance or ammo text is null!");
        }
    }
}

