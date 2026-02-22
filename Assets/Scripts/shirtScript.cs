using UnityEngine;

public class shirtScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
