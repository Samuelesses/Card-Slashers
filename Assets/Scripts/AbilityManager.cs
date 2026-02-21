using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum Ability { None, Speed, Power }
    public Ability currentAbility = Ability.None;
    private PlayerController pc;

    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentAbility != Ability.None)
        {
            GiveRandomAbility();
            Debug.Log(currentAbility);
            UseAbility();
            Debug.Log("Used " + currentAbility);
        }
    }

    public void GiveRandomAbility()
    {
        if (currentAbility != Ability.None) return;
        currentAbility = (Ability)Random.Range(1, 3);
        Debug.Log(currentAbility);
    }

    void UseAbility()
    {
        if (currentAbility == Ability.Speed)
        {
            pc.speed *= 2f;
            Invoke("ResetStats", 5f);
        }
        else if (currentAbility == Ability.Power)
        {
            pc.knockback *= 2f;
            Invoke("ResetStats", 5f);
        }

        currentAbility = Ability.None;
        pc.abilityIcon.SetActive(false);
    }

    void ResetStats()
    {
        pc.speed = 1f;
        pc.knockback = 5f;
    }
}