using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("---- Cosmetic Variables ----")]
    [SerializeField] GameObject[] hats;
    public int hatIndex;

    [Header ("---- Combat Variables ----")]
    public Rigidbody2D rigidBody;
    private Transform targetedPlayer;
    private float closestPlayer = 100f;
    public GameObject[] players;

    void Start()
    {
        hatIndex = Random.Range(0, hats.Length);
        hats[hatIndex].SetActive(true);

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        
        foreach (GameObject player in players)
        {
            if (player.transform == transform) continue;

            if ((player.transform.position - transform.position).magnitude <= closestPlayer)
            {
                targetedPlayer = player.transform;
            }
        }
    }

    void FixedUpdate()
    {
        
    }
}
