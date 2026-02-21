using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] hats;
    public int hatIndex;
    private Transform targetedPlayer;
    private float closestPlayer = 100f;
    public List<Transform> players = new List<Transform>();

    void Start()
    {
        hatIndex = Random.Range(0, hats.Length);
        hats[hatIndex].SetActive(true);
        foreach (Transform child in transform.parent)
        {
            if (child.CompareTag("Player") && child != transform)
            {
                players.Add(child);
            }
        }
    }

    void Update()
    {
       foreach (Transform player in players)
        {
            if ((player.position - transform.position).magnitude < closestPlayer)
            {
                targetedPlayer = player;
            }
        }
    }

    //yo

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetedPlayer.position, 100f);
    }
}
