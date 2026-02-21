using Unity.VisualScripting;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{

    public GameObject orbPrefab;
    public float spawnRate = 10f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnOrb", 2f, spawnRate);
    }

    // Update is called once per frame
    void SpawnOrb()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(randomX, randomY);

        GameObject newOrb = Instantiate(orbPrefab, spawnPos, Quaternion.identity);
        UpdatePlayerTargetLists();
    }

    void UpdatePlayerTargetLists()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject player in allPlayers)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            pc.abilityOrb = GameObject.FindGameObjectsWithTag("Ability");
            
        }
    }
}
