using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    private string cardData;
    public Dictionary<string, string> cardDatabase = new Dictionary<string, string>();

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\n' || c == '\r')
            {
                Debug.Log("CARD SCANNED");
                cardDatabase.Add(cardData, "N/A");
                Debug.Log(cardData);
                cardData = "";
            }
            else
            {
                cardData += c;
            }
        }
    }
}
