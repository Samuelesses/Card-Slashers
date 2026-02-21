using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    private string currentCardData;
    public Dictionary<string, CardPlayerData> cardDatabase = new Dictionary<string, CardPlayerData>();

    public class CardPlayerData
    {
        public string name;
        public string hexColor;

        public CardPlayerData(string _name, string _hexColor)
        {
            name = _name;
            hexColor = _hexColor;
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
        else
        {
            Debug.Log("CARD NOT FOUND IN DATABASE. ADDING");

            cardDatabase.Add(currentCardData, new CardPlayerData("NAME HERE", "#000000"));
            Debug.Log(cardDatabase[currentCardData].name + cardDatabase[currentCardData].hexColor);
        }

        currentCardData = "";
    }
}
