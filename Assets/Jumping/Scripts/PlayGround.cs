using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a collection of flower plants and attached flowers
/// </summary>
public class PlayGround : MonoBehaviour
{
    // The diameter of the area where the agent and flowers can be
    // used for observing relative distance from agent to flower
    public const float AreaDiameter = 20f;

    /// <summary>
    /// The list of all flowers in the flower area
    /// </summary>
    public List<Coin> GoodCoins { get; private set; }
    public List<Coin> BadCoins { get; private set; }
    public List<Jumping> Enemies { get; private set; }

    /// <summary>
    /// Reset the flowers and flower plants
    /// </summary>
    public void ResetPlayGound()
    {
        // Reset each flower
        foreach (Coin goodCoin in GoodCoins)
        {
            goodCoin.SetStartLocation(transform.position);
        }

        // Reset each flower
        foreach (Coin badCoin in BadCoins)
        {
            badCoin.SetStartLocation(transform.position);
        }

        // Reset each flower
        foreach (Jumping enemy in Enemies)
        {
            enemy.SetStartLocation(transform.position);
        }
    }

    /// <summary>
    /// Called when the area wakes up
    /// </summary>
    private void Awake()
    {
        // Initialize variables
        GoodCoins = new List<Coin>();
        BadCoins = new List<Coin>();
        Enemies = new List<Jumping>();
    }

    private void Start()
    {
        // Find all flowers that are children of this GameObject/Transform
        FindCoinsAndEnemies(transform);
        ResetPlayGound();
    }

    /// <summary>
    /// Recursively finds all flowers and flower plants that are children of a parent transform
    /// </summary>
    /// <param name="parent">The parent of the children to check</param>
    private void FindCoinsAndEnemies(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag("Coin"))
            {
                // Found a flower plant, add it to the flowerPlants list
                Coin coin = child.GetComponent<Coin>();
                coin.playGround = this;
                GoodCoins.Add(coin);
            }
            else if (child.CompareTag("BadCoin"))
            {
                // Found a flower plant, add it to the flowerPlants list
                Coin coin = child.GetComponent<Coin>();
                coin.playGround = this;
                BadCoins.Add(coin);
            }
            else if (child.CompareTag("Jumping"))
            {
                // Found a flower plant, add it to the flowerPlants list
                Jumping jumping = child.GetComponent<Jumping>();
                Enemies.Add(jumping);
            }
            else
            {
                // Flower component not found, so check children
                FindCoinsAndEnemies(child);
            }
        }
    }

    public void OnCoinCollected(Coin coin, bool goodCoin)
    {
        coin.SetStartLocation(transform.position);
        if (goodCoin)
        {
            GoodCoins.Add(coin);
        }
        else
        {
            BadCoins.Add(coin);
        }
    }
}
