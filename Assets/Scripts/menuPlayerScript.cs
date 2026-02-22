using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class menuPlayerScript : MonoBehaviour
{
    [SerializeField] Image self;
    [SerializeField] Image shirt;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject[] hats;
    [SerializeField] AudioClip[] intros;
    [SerializeField] AudioSource aSource;
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
        text.text = _name;
        self.color = new Color(1, 1, 1, 1);
        shirt.color = new Color(_color1, _color2, _color3, 1);
        hats[_hatIndex].SetActive(true);
        if (_name == "Backyard Scientist")
        {
            aSource.PlayOneShot(intros[0]);
        }else if (_name == "Code Bullet")
        {
            aSource.PlayOneShot(intros[1]);
        }else if (_name == "Polymars")
        {
            aSource.PlayOneShot(intros[3]);
        }else if (_name == "Michael Reeves")
        {
            aSource.PlayOneShot(intros[2]);
        }else if (_name == "Sondering Emily")
        {
            aSource.PlayOneShot(intros[4]);
        }else if (_name == "TechJoyce")
        {
            aSource.PlayOneShot(intros[5]);
        }else if (_name == "William Osman")
        {
            aSource.PlayOneShot(intros[6]);
        }
        else
        {
            Debug.Log("else");
        }
    }
}
