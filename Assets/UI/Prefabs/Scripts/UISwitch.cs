using UnityEngine;
using UnityEngine.SceneManagement;

public class UISwitch : MonoBehaviour
{
    public GameObject NoPlayers;
    public GameObject Players;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Players.activeSelf)
            {
                // second press when panel already shown should start the game
                SceneManager.LoadScene("Game");
            }
            else
            {
                Players.SetActive(true);
                NoPlayers.SetActive(false);
            }
        }
    }
}
