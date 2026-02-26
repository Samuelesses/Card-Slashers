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
        // update basic visuals
        text.text = _name;
        self.color = new Color(1, 1, 1, 1);
        shirt.color = new Color(_color1, _color2, _color3, 1);

        // ensure only the selected hat is active
        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(i == _hatIndex);
        }

        // safety: clamp hat index if out of range
        if (_hatIndex < 0 || _hatIndex >= hats.Length)
        {
            Debug.LogWarning($"hatIndex { _hatIndex } out of range");
        }

        // play intro if it's one of the known names
        if (_name == "Backyard Scientist")
        {
            aSource.PlayOneShot(intros[0]);
        }
        else if (_name == "Code Bullet")
        {
            aSource.PlayOneShot(intros[1]);
        }
        else if (_name == "Polymars")
        {
            aSource.PlayOneShot(intros[3]);
        }
        else if (_name == "Michael Reeves")
        {
            aSource.PlayOneShot(intros[2]);
        }
        else if (_name == "Sondering Emily")
        {
            aSource.PlayOneShot(intros[4]);
        }
        else if (_name == "TechJoyce")
        {
            aSource.PlayOneShot(intros[5]);
        }
        else if (_name == "William Osman")
        {
            aSource.PlayOneShot(intros[6]);
        }
        else
        {
            // no special intro available for this name (random or numeric);
            // do nothing so the console isn't spammed during testing.
        }
    }
}
