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
        // ensure usedNames is initialized so we can add new players via keyboard
        if (usedNames == null)
            usedNames = new List<string>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        // if realPlayers hasn't been set in inspector, try to find them automatically
        if (realPlayers == null || realPlayers.Length == 0)
        {
            var scripts = FindObjectsOfType<menuPlayerScript>();
            realPlayers = new GameObject[scripts.Length];
            for (int i = 0; i < scripts.Length; i++)
                realPlayers[i] = scripts[i].gameObject;
        }
    }

    void Update()
    {
        // original card reader logic (accumulate characters until return)
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

        // testing support: allow adding players with numeric keys 1-4 on the main menu
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Main Menu")
        {
            // don't allow more than the available slots (usually 4)
            if (realPlayers == null || realPlayers.Length == 0)
            {
                Debug.LogWarning("[CardReader] realPlayers array is empty");
                return;
            }
            for (int i = 1; i <= 4; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    Debug.Log($"[CardReader] numeric key {i} pressed");
                    string dummyId = "NUM" + i;
                    if (CardIdExists(dummyId))
                    {
                        // duplicate key always starts the game
                        StartGame();
                    }
                    else
                    {
                        AddNewPlayer(dummyId);
                    }
                }
            }
        }
        else
        {
            // during actual gameplay, allow numeric keys to trigger abilities
            if (pm != null)
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                    {
                        pm.abilityPlayer(i - 1);
                    }
                }
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

    /// <summary>
    /// Query the database to see if a given cardId is already registered.
    /// </summary>
    private bool CardIdExists(string cardId)
    {
        foreach (KeyValuePair<int, CardPlayerData> player in cardDatabase)
        {
            if (player.Value.cardId == cardId)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Initiates transition to gameplay.  Uses animator if assigned or directly loads the scene.
    /// </summary>
    private void StartGame()
    {
        if (transAni != null)
        {
            transAni.SetTrigger("go");
        }
        else
        {
            Debug.Log("[CardReader] starting game via direct load");
            SceneManager.LoadScene("Game");
        }
    }

    /// <summary>
    /// Centralized logic for adding a new player entry using a card identifier.
    /// This is reused both by the card reader and by debug numeric keys.
    /// </summary>
    /// <param name="cardId">Unique identifier for the player card (or generated number token).</param>
    private void AddNewPlayer(string cardId)
    {
        // do not exceed the number of visible player slots
        if (realPlayers == null || realPlayers.Length == 0)
        {
            Debug.LogError("[CardReader] realPlayers array not assigned or empty when trying to add");
        }
        if (cardDatabase.Count >= realPlayers.Length)
        {
            Debug.LogWarning("Player limit reached, cannot add more.");
            currentCardData = "";
            return;
        }

        // generate a unique random name not yet used
        string pickedName = randomNames[Random.Range(0, randomNames.Length)];
        while (usedNames.Contains(pickedName))
        {
            pickedName = randomNames[Random.Range(0, randomNames.Length)];
        }
        usedNames.Add(pickedName);

        int tempIndex = cardDatabase.Count;
        cardDatabase.Add(tempIndex, new CardPlayerData(pickedName, cardId,
            Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f),
            Random.Range(0, hatListLength)));

        if (tempIndex < realPlayers.Length && realPlayers[tempIndex] != null)
        {
            realPlayers[tempIndex].GetComponent<menuPlayerScript>().updatePlayer(
                cardDatabase[tempIndex].name,
                cardDatabase[tempIndex].color1,
                cardDatabase[tempIndex].color2,
                cardDatabase[tempIndex].color3,
                cardDatabase[tempIndex].hatIndex);
        }

        // ensure the players UI panel is visible when someone joins
        UISwitch ui = FindObjectOfType<UISwitch>();
        if (ui != null)
        {
            ui.Players.SetActive(true);
            if (ui.NoPlayers != null)
                ui.NoPlayers.SetActive(false);
        }

        currentCardData = "";
    }

    /// <summary>
    /// Handles a card read event by checking for existing players or creating a new one.
    /// </summary>
    private void HandleCard()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Main Menu")
        {
            // if card already registered, start the game and exit
            foreach (KeyValuePair<int, CardPlayerData> player in cardDatabase)
            {
                Debug.Log(player.Value.cardId + currentCardData);
                if (player.Value.cardId == currentCardData)
                {
                    StartGame();
                    return;
                }
            }

            // otherwise add new player with the scanned card id
            AddNewPlayer(currentCardData);
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
