using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public int playerCount = 4;

    public Transform[] spawnPoints;
    public List<int> openSpawnPoints = new List<int> {0, 1, 2, 3, 4, 5};

    void Start()
    {  
        SpawnPlayers();   
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            int spawnPoint = openSpawnPoints[Random.Range(0, openSpawnPoints.Count)];

            Instantiate(playerPrefab, spawnPoints[spawnPoint].position, Quaternion.identity, transform).GetComponent<PlayerController>().index = i;

            openSpawnPoints.Remove(spawnPoint);
        }
    }
}
