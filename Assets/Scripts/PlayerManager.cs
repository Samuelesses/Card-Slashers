using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public int playerCount = 4;

    public Transform[] spawnPoints;
    public List<int> openSpawnPoints = new List<int> {0, 1, 2, 3, 4, 5};
    public GameObject[] players = new GameObject[4];

    void Start()
    {  
        SpawnPlayers();   
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            int spawnPoint = openSpawnPoints[Random.Range(0, openSpawnPoints.Count)];

            players[i] = Instantiate(playerPrefab, spawnPoints[spawnPoint].position, Quaternion.identity, transform);
            players[i].GetComponent<PlayerController>().index = i;
            openSpawnPoints.Remove(spawnPoint);
        }
    }
    public void abilityPlayer(int playerIndex)
    {
        if (players[playerIndex].GetComponent<PlayerController>().hasAbility)
        {
            players[playerIndex].GetComponent<PlayerController>().hasAbility = false;
            players[playerIndex].GetComponent<AbilityManager>().UseAbility();
        }
    }
}
