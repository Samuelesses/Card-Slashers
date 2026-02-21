using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("---- Cosmetic Variables ----")]
    public GameObject[] hats;
    public int hatIndex;
    [SerializeField] Animator srAni;
    [SerializeField] GameObject hitParticle;

    [Header ("---- Combat Variables ----")]
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

    void Start()
    {
        hatIndex = Random.Range(0, hats.Length);
        hats[hatIndex].SetActive(true);

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
            enemyHealth.takeDamage();
            knockbackDuration = 0.5f;
            Debug.Log("hit");
            hit = true;
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            rigidBody.linearVelocity = Vector2.zero;
            rigidBody.AddForce(direction * knockback, ForceMode2D.Impulse);
            srAni.SetTrigger("hit");
            Instantiate(hitParticle, transform.position, transform.rotation);
        }
    }
}
