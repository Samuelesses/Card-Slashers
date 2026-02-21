using UnityEngine;

public class hRendererScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite upSr;
    [SerializeField] Sprite downSr;
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
        sr.sprite = upSr;
    }

    public void down()
    {
        sr.sprite = downSr;
    }
}
