using UnityEngine;

public class pRendererScript : MonoBehaviour
{
    [SerializeField] GameObject player; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void up()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.hats[pc.hatIndex].GetComponent<hRendererScript>().up();
    }

    public void down()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.hats[pc.hatIndex].GetComponent<hRendererScript>().down();
    }
}
