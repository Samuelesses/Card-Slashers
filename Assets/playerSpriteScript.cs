using UnityEngine;

public class playerSpriteScript : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0f, 0f, player.rotation.z*-1);
    }
}
