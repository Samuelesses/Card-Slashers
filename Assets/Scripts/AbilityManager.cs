using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum Ability { None, Speed, Power, Shield, Bomb }
    public Ability currentAbility = Ability.None;
    public GameObject shieldEffect;
    private PlayerController pc;
    private health h;
    void Start()
    {
        pc = GetComponent<PlayerController>();
        h = GetComponent<health>();
    }

    void Update()
    {
        // space is only for testintg
        if (Input.GetKeyDown(KeyCode.Space) && currentAbility != Ability.None)
        {
            Debug.Log("Using: " + currentAbility);
            
            UseAbility();
        }
    }

    public void GiveRandomAbility()
    {
        if (currentAbility != Ability.None) return;
        currentAbility = (Ability)Random.Range(1, 5);
        Debug.Log(currentAbility);
    }

    void UseAbility()
    {
        if (currentAbility == Ability.Speed)
        {
            pc.speed *= 2f;
            Invoke("ResetStats", 4f);
        }
        
        else if (currentAbility == Ability.Power)
        {
            h = GetComponent<health>();
            h.minDamage *= 5;
            h.maxDamage *= 2;
            Invoke("ResetStats", 4f);
        }
        else if (currentAbility == Ability.Shield)
        {
            h = GetComponent<health>();
            shieldEffect.SetActive(true);
            h.isShielded = true;
            Invoke("ResetStats", 4f);
        }
        else if (currentAbility == Ability.Bomb)
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            if (col != null)
                {
                    col.size = new Vector2(3f, 3f); 
                    pc.knockback = 0;
                    Invoke("ResetBombCollider", 4f);
                    Invoke("ResetStats", 4f);
                }
        }

        currentAbility = Ability.None;
        pc.abilityIcon.SetActive(false);
    }

    void ResetStats()
    {
        pc.speed = 1f;
        pc.knockback = 5f;
        h.minDamage = 1;
        h.maxDamage = 5;
        h.isShielded = false;
        shieldEffect.SetActive(false);
    }

    void ResetBombCollider()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(0.5f, 0.5f);
    }
}