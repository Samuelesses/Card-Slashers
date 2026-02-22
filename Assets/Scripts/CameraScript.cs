using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cooldown;
    public Vector3 pos;
    public float amp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown>0)
        {
            transform.position = new Vector3(pos.x+Random.Range(0f, amp), pos.y+Random.Range(0f, amp), pos.z);
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 0f;
            transform.position = pos;
        }
    }

    public void shake(float time, float ampl)
    {
        pos = transform.position;
        cooldown = time;
        amp = ampl;
    }
}
