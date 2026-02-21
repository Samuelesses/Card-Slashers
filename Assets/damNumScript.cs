using UnityEngine;
using TMPro;
public class damNumScript : MonoBehaviour
{
    public TextMeshPro self;
    [SerializeField] Animator selfAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText(string input)
    {
        self.text = input;
        selfAni.SetTrigger("show");
    }

    public void done()
    {
        Destroy(gameObject);
    }
}
