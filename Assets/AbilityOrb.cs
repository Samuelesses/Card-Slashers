using UnityEngine;

public class AbilityOrb : MonoBehaviour
{
    private PlayerController playerController;
    public bool hasAbility = false;
    public AbilityManager abilityManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.abilityIcon.SetActive(true);
            abilityManager.GiveRandomAbility();
            Destroy(gameObject);
        }
    }
}
