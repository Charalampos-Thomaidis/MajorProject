using UnityEngine;

public class EyeAIController : MonoBehaviour
{
    public float startTimeBtwShots;
    public GameObject projectile;
    public GameObject player;
    public float weaponRange = 5.0f;
    public bool facingRight = false;

    private Vector2 diffBtwPos;
    private float distance;
    private AudioManager audioManager;
    private float timeBtwShots;
    private Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeBtwShots = 0;
    }

    void Start()
    {
        audioManager = AudioManager.GetAudioManager();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        diffBtwPos = player.transform.position - transform.position;

        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
        {
            Flip();
        }
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
        {
            Flip();
        }

        if (distance <= weaponRange)
        {
            if (Physics2D.OverlapCircle(transform.position, distance))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, diffBtwPos.normalized, weaponRange);

                if (hit.collider.tag == "Player" && timeBtwShots <= 0)
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    audioManager.PlayEyeAttackSound();
                    anim.Play("eye_attack_anim");
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }
}