using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    private string currentCardData;
    public Dictionary<string, CardPlayerData> cardDatabase = new Dictionary<string, CardPlayerData>();
    public int hatListLength;
    public CardPlayerData[] players = new CardPlayerData[4];
    public int playersIndex = 0;

    public class CardPlayerData
    {
        public string name;
        public float color1;
        public float color2;
        public float color3;
        public int hatIndex;
        public CardPlayerData(string _name, float _color1, float _color2, float _color3, int _hatIndex)
        {
            name = _name;
            _color1 = _color1;
            _color2 = _color2;
            _color3 = _color3;
            _hatIndex = _hatIndex;
        }
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
        
        if (cardDatabase.ContainsKey(currentCardData))
        {
            Debug.Log("CARD EXISTS IN DATABASE");
        }
        else if(playersIndex<players.Length)
        {
            Debug.Log("CARD NOT FOUND IN DATABASE. ADDING");
            CardPlayerData temp = new CardPlayerData("NAME HERE", Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0, hatListLength));
            cardDatabase.Add(currentCardData, temp);
            players[playersIndex] = temp;
            playersIndex++;
            Debug.Log(cardDatabase);
        }

        currentCardData = "";
    }
}
