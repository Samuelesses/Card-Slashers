using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    private AbilitySpawner abilitySpawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilitySpawner = FindObjectOfType<AbilitySpawner>();
        
        if (abilitySpawner == null)
        {
            Debug.LogWarning("GameEnder: No AbilitySpawner found in scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (abilitySpawner != null && abilitySpawner.gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("GameEnder: Returning to Main Menu");
                Time.timeScale = 1f; // Reset time scale before changing scenes
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}
