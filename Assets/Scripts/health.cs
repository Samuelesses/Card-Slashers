using UnityEngine;
using TMPro;

public class health : MonoBehaviour
{
    public int totalHp;
    public int minDamage = 1;
    public int maxDamage = 5;
    private TextMeshProUGUI hpText;
    PlayerController playerController;
    [SerializeField] GameObject damDisplay;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //hpText = GetComponentInChildren<TextMeshProUGUI>();
        //hpText.text = totalHp.ToString();
    }

    public void takeDamage()
    {
        int damage = Random.Range(minDamage, maxDamage);
        totalHp -= damage;
        //hpText.text = totalHp.ToString();
        Debug.Log(gameObject.name + " Health: " + totalHp);
        if (totalHp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Instantiate(damDisplay, transform.position, transform.rotation).GetComponent<damNumScript>().updateText("-"+damage);
        }
    }
}
