using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class CardReader : MonoBehaviour
{
    private string currentCardData;
    public Dictionary<int, CardPlayerData> cardDatabase = new Dictionary<int, CardPlayerData>();
    public int hatListLength;
    public CardPlayerData[] players = new CardPlayerData[4];
    public GameObject[] realPlayers;
    public int playersIndex = 0;
    public List<string> usedNames;

    public string[] randomNames;

    [SerializeField] Animator transAni;
    [SerializeField] PlayerManager pm;



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
    public class NameList
    {
        public string[] names;
    }

    void Awake() 
    {
        GenerateNames();
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

        void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Main Menu")
        {
            pm = GameObject.Find("Players").GetComponent<PlayerManager>();
        }
    }

    void GenerateNames()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Names.json");
        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            NameList nameList = JsonUtility.FromJson<NameList>(jsonText);
            randomNames = nameList.names;
        }  
    }

    private void HandleCard()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Main Menu")
        {
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
            string pickedName = randomNames[Random.Range(0, randomNames.Length)];

            while (usedNames.Contains(pickedName))
            {
                pickedName = randomNames[Random.Range(0, randomNames.Length)];
            }
            
            usedNames.Add(pickedName);
            cardDatabase.Add(tempIndex, new CardPlayerData(pickedName, currentCardData, Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0, hatListLength)));
            realPlayers[tempIndex].GetComponent<menuPlayerScript>().updatePlayer(cardDatabase[tempIndex].name,cardDatabase[tempIndex].color1,cardDatabase[tempIndex].color2,cardDatabase[tempIndex].color3,cardDatabase[tempIndex].hatIndex);
            currentCardData = "";
        }
        else
        {
            pm = GameObject.Find("Players").GetComponent<PlayerManager>();
            for (int x=0; x<cardDatabase.Count; x++)
            {
                if (cardDatabase[x].cardId == currentCardData)
                {
                    Debug.Log("realreal");
                    pm.abilityPlayer(x);
                }
            }
            currentCardData = "";
        }
    }
    
}
