using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CardReader : MonoBehaviour
{
    private string currentCardData;
    public Dictionary<int, CardPlayerData> cardDatabase = new Dictionary<int, CardPlayerData>();
    public int hatListLength;
    public CardPlayerData[] players = new CardPlayerData[4];
    public GameObject[] realPlayers;
    public int playersIndex = 0;

    [SerializeField] Animator transAni;

    public class CardPlayerData
    {
        public string name;
        public string cardId;
        public float color1;
        public float color2;
        public float color3;
        public int hatIndex;

        public CardPlayerData(string _name, string _cardId, float _color1, float _color2, float _color3, int _hatIndex)
        {
            name = _name;
            cardId = _cardId;
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
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Main Menu")
        {
            Debug.Log("if");
            foreach (KeyValuePair<int, CardPlayerData> player in cardDatabase)
            {
                Debug.Log(player.Value.cardId + currentCardData);
                if (player.Value.cardId == currentCardData)
                {
                    transAni.SetTrigger("go");
                    
                    return;
                }
            }
            int tempIndex = cardDatabase.Count;
            cardDatabase.Add(tempIndex, new CardPlayerData("NAME HERE", currentCardData, Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0, hatListLength)));
            realPlayers[tempIndex].GetComponent<menuPlayerScript>().updatePlayer(cardDatabase[tempIndex].name,cardDatabase[tempIndex].color1,cardDatabase[tempIndex].color2,cardDatabase[tempIndex].color3,cardDatabase[tempIndex].hatIndex);
            currentCardData = "";
        }
        else
        {
            
        }
    }
}
