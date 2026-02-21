using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CardReader : MonoBehaviour
{
    private string currentCardData;
    public Dictionary<string, CardPlayerData> cardDatabase = new Dictionary<string, CardPlayerData>();
    public int hatListLength;
    public CardPlayerData[] players = new CardPlayerData[4];
    public GameObject[] realPlayers;
    public int playersIndex = 0;

    public class CardPlayerData
    {
        public string name;
        public string hexColor;
        public float color1;
        public float color2;
        public float color3;
        public int hatIndex;

        public CardPlayerData(string _name, float _color1, float _color2, float _color3, int _hatIndex)
        {
            name = _name;
            color1 = _color1;
            color2 = _color2;
            color3 = _color3;
            hatIndex = _hatIndex;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\n' || c == '\r')
            {
                HandleCard();
            }
            else
            {
                currentCardData += c;
            }
        }
    }

    private void HandleCard()
    {
        Debug.Log("CARD SCANNED" + currentCardData);
        Scene currentScene = SceneManager.GetActiveScene();
        
        if (cardDatabase.ContainsKey(currentCardData) && currentScene.name == "Main Menu")
        {
            Debug.Log("CARD EXISTS IN DATABASE. LOADING GAME SCENE");
            SceneManager.LoadScene("Game");
        }
        else if (playersIndex < players.Length && currentScene.name == "Main Menu")
        {
            Debug.Log("CARD NOT FOUND IN DATABASE. ADDING");
            CardPlayerData temp = new CardPlayerData("NAME HERE", Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0, hatListLength));
            cardDatabase.Add(currentCardData, temp);
            players[playersIndex] = temp;
            realPlayers[playersIndex].GetComponent<menuPlayerScript>().updatePlayer(temp.name, temp.color1, temp.color2, temp.color3, temp.hatIndex);
            playersIndex++;
            Debug.Log(cardDatabase);
        }

        currentCardData = "";
    }
}
