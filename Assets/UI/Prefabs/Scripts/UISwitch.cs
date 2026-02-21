using UnityEngine;

public class UISwitch : MonoBehaviour
{
    public GameObject NoPlayers;
    public GameObject Players;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Players.SetActive(true);
            NoPlayers.SetActive(false);

        }
        
    }
}
