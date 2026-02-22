using UnityEngine;
using TMPro;

public class health : MonoBehaviour
{
    public int totalHp;

    public bool isShielded = false;
    [SerializeField] GameObject damDisplay;

    public void takeDamage(int incomingMin, int incomingMax)
    {
        if (isShielded)
        {
            return;
        }

        int damage = Random.Range(incomingMin, incomingMax);
        totalHp -= damage;

        if (totalHp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Instantiate(damDisplay, transform.position, transform.rotation)
                .GetComponent<damNumScript>().updateText("-" + damage);
        }
    }
}