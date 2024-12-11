using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectiblePrefabs; // List of collectible prefabs
    [SerializeField] private List<Transform> spawnPoints; // List of spawn points

    private void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        // Ensure we have enough spawn points
        if (spawnPoints.Count < 5)
        {
            Debug.LogWarning("Not enough spawn points for five collectibles.");
            return;
        }

        // Shuffle the spawn points to select five random ones
        List<Transform> selectedSpawnPoints = new List<Transform>(spawnPoints);
        selectedSpawnPoints.Shuffle(); // Custom shuffle extension (see below for code)

        // Spawn five collectibles at the chosen locations
        for (int i = 0; i < 5; i++)
        {
            // Randomly select a collectible from the list
            GameObject collectible = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Count)];

            // Instantiate the collectible at the selected spawn point
            Instantiate(collectible, selectedSpawnPoints[i].position, Quaternion.identity);
        }
    }
}

// Extension method to shuffle a list
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int r = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }
}