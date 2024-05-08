using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAccount
{
    private static int coins = 0;
    private static int bullets = 0;

    // Adds coins and updates the UI safely
    public static void AddCoin(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);

        // Check if UIManager instance is available before trying to update the UI
        if (UIManager.Instance != null)
        {
            UIManager.SafeUpdateCoinCount(coins);
        }
        else
        {
            Debug.LogError("Failed to update coins count: UIManager instance is null.");
        }
    }

    // Adds bullets and updates the UI safely
    public static void AddBullets(int amount)
    {
        bullets += amount;
        Debug.Log("Bullets: " + bullets);

        // Check if UIManager instance is available before trying to update the UI
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCount(bullets);
        }
        else
        {
            Debug.LogError("Failed to update bullets count: UIManager instance is null.");
        }
    }

    // Returns the current amount of coins
    public static int GetCoins()
    {
        return coins;
    }

    // Returns the current amount of bullets
    public static int GetBullets()
    {
        return bullets;
    }
}
