using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool testing;
    public int index;
    [Header ("---- Cosmetic Variables ----")]
    public GameObject[] hats;
    public int hatIndex2;
    [SerializeField] Animator srAni;
    [SerializeField] GameObject hitParticle;
    [SerializeField] CameraScript cameraScript;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteRenderer shirtSr;

    [Header ("---- Combat Variables ----")]
    public bool hasAbility = false;
    public GameObject abilityObj;
    public Rigidbody2D rigidBody;
    private Transform targetedPlayer;
    private float closestPlayer = 100f;
    public float speed = 1f;
    public float knockback = 5f;
    public float knockbackDuration;
    public static bool hit;
    public GameObject[] players;
    public GameObject[] abilityOrb;
    public GameObject abilityIcon;
    public CardReader cr;
    public int myAttackMin = 1;
    public int myAttackMax = 5;

    void Start()
    {
        if (!testing)
        {
            cr = GameObject.Find("CardReader").GetComponent<CardReader>();
            cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
            hatIndex2 = cr.cardDatabase[index].hatIndex;
            hats[hatIndex2].SetActive(true);
            shirtSr.color = new Color(cr.cardDatabase[index].color1, cr.cardDatabase[index].color2, cr.cardDatabase[index].color3, 1);
        }
        else
        {
            cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
            hatIndex2 = 0;
            shirtSr.color = new Color(0.5f, 1, 0.7f, 1);
        }
        players = GameObject.FindGameObjectsWithTag("Player");
        abilityOrb = GameObject.FindGameObjectsWithTag("Ability");
    }

    void Update()
    {
        closestPlayer = 100f;
        targetedPlayer = null;

        foreach (GameObject player in players)
        {
            if (player == null) continue;
            if (player.transform == transform) continue;

            if ((player.transform.position - transform.position).magnitude <= closestPlayer)
            {
                closestPlayer = (player.transform.position - transform.position).magnitude; 
                targetedPlayer = player.transform;
            }
        }

        foreach (GameObject orb in abilityOrb)
        {
            if (orb == null) continue;
            float dist = (orb.transform.position - transform.position).magnitude;

            if (dist < closestPlayer)
            {
                closestPlayer = dist;
                targetedPlayer = orb.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (knockbackDuration > 0)
        {
            knockbackDuration -= Time.fixedDeltaTime;
            return;
        }

        if (targetedPlayer)
        {
            Vector2 direction = (targetedPlayer.position - transform.position).normalized;
            rigidBody.linearVelocity = direction * speed;
        
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health enemyHealth = collision.gameObject.GetComponent<health>();
            enemyHealth.takeDamage(myAttackMin,myAttackMax);
            knockbackDuration = 0.5f;
            //Debug.Log("hit");
            hit = true;
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            rigidBody.linearVelocity = Vector2.zero;
            rigidBody.AddForce(direction * knockback, ForceMode2D.Impulse);
            srAni.SetTrigger("hit");
            Instantiate(hitParticle, transform.position, transform.rotation);
            cameraScript.shake(0.1f, 0.04f);
        }
    }
}
