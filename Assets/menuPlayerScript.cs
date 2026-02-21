using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class menuPlayerScript : MonoBehaviour
{
    [SerializeField] Image self;
    [SerializeField] Image shirt;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject[] hats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePlayer(string _name, float _color1, float _color2, float _color3, int _hatIndex)
    {
        text.text = name;
        self.color = new Color(1, 1, 1, 1);
        shirt.color = new Color(_color1, _color2, _color3, 1);
        hats[_hatIndex].SetActive(true);
    }
}
