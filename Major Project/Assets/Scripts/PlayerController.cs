using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public LayerMask platformLayerMask;
    [SerializeField] 
    public LayerMask worldLayerMask;

    public float jumpspeed;
    public float speed;
    public GameObject Player;
    public GameObject RealWorld;
    public GameObject FantasyWorld;
    public GameObject deathText;
    public Transform spawn;
    public Sprite blackplayer;
    public Sprite whiteplayer;

    private float timer = 0.5f;
    private float currentPlayerPosY;
    private float playerPositionX;
    private float playerPositionY;
    private float moveHorizontal;
    private string ActivePlayer;
    private bool isOnGround;
    private bool isTiming = false;
    private Rigidbody2D playerRB;
    private SpriteRenderer playerRenderer;
    private BoxCollider2D[] colRealWorld;
    private BoxCollider2D[] colFantasyWorld;
    private BoxCollider2D boxCollider2d;
    private AudioManager audioManager;
    private Animator anim;

    void Start()
    {
        ActivePlayer = "white";
        playerRB = Player.GetComponent<Rigidbody2D>();
        playerRenderer = Player.GetComponent<SpriteRenderer>();
        colRealWorld = RealWorld.GetComponentsInChildren<BoxCollider2D>();
        colFantasyWorld = FantasyWorld.GetComponentsInChildren<BoxCollider2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        EnableDisableColliders(colFantasyWorld, colRealWorld);
        audioManager = AudioManager.GetAudioManager();
        deathText.SetActive(false);
        playerRenderer.sprite = whiteplayer;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Jump();
        AbilityToSwitchWorlds();

        playerPositionX = transform.position.x;
        playerPositionY = transform.position.y;

        if (playerPositionY >= 0)
        {
            currentPlayerPosY = playerPositionY;
        }
        else
        {
            currentPlayerPosY = -playerPositionY;
        }

        if (isTiming == true)
        {
            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;
            }
            else
            {
                timer = 0;
                isTiming = false;
                Player.transform.position = spawn.position;
            }
        }
    }

    void Movement()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(speed * moveHorizontal, playerRB.velocity.y);

        if (ActivePlayer == "black")
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector2(180, 0);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector2(180, 180);
            }
        }
        if (ActivePlayer == "white")
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            audioManager.PlayJumpSound();

            if (ActivePlayer == "black")
            {
                playerRB.velocity = new Vector2(0, -jumpspeed);
            }
            if (ActivePlayer == "white")
            {
                playerRB.velocity = new Vector2(0, jumpspeed);
            }
        }
    }
    
    void AbilityToSwitchWorlds()
    {
        if (Input.GetKeyDown(KeyCode.S) && ActivePlayer == "white" && onGround())
        {
            anim.Play("Furry_Jump_anim");
            audioManager.PlaySwitchSound();
            audioManager.PlayFantasyBackgroundMusic();
            ActivePlayer = "black";
            transform.eulerAngles = new Vector2(180, 0);
            changePlayerVariables(new Vector2(playerPositionX, playerPositionY), -1, playerRenderer.sprite = blackplayer);
            EnableDisableColliders(colRealWorld, colFantasyWorld);
        }
        if (Input.GetKeyDown(KeyCode.W) && ActivePlayer == "black" && onGround())
        {
            anim.Play("Furry_Jump_White_anim");
            audioManager.PlaySwitchSound();
            audioManager.PlayBackgroundMusic();
            ActivePlayer = "white";
            transform.eulerAngles = new Vector2(0, 0);
            changePlayerVariables(new Vector2(playerPositionX, playerPositionY), 1, playerRenderer.sprite = whiteplayer);
            EnableDisableColliders(colFantasyWorld, colRealWorld);
        }
    }

    bool onGround()
    {
        if (ActivePlayer == "white")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 70, worldLayerMask);
            raycastHit(hit);
        }
        if (ActivePlayer == "black")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 70, worldLayerMask);
            raycastHit(hit);
        }
        return isOnGround;
    }

    private bool IsGrounded()
    {
        if (ActivePlayer == "white")
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
            return hit.collider != null;
        }
        if (ActivePlayer == "black")
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, -.1f, platformLayerMask);
            return hit.collider != null;
        }
        return isOnGround;
    }

    void raycastHit(RaycastHit2D hit)
    {
        if (hit == null)
        {
            return;
        }
        if (hit.distance < 0.6)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    void changePlayerVariables(Vector2 newPlayerPos, int newGravity, Sprite newSprite)
    {
        Player.transform.position = newPlayerPos;
        playerRB.gravityScale = newGravity;
        playerRenderer.sprite = newSprite;
    }

    public void EnableDisableColliders(BoxCollider2D[] enableColliders, BoxCollider2D[] disableColliders)
    {
        foreach (BoxCollider2D col in enableColliders)
        {
            col.enabled = true;
        }
        foreach (BoxCollider2D col in disableColliders)
        {
            col.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("spikes"))
        {
            audioManager.PlaySpikeSound();
            Player.transform.position = spawn.position;
            deathText.SetActive(true);
            StartCoroutine("WaitForSec");
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            isTiming = true;
            deathText.SetActive(true);
            StartCoroutine("WaitForSec");
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            Player.transform.position = spawn.position;
            deathText.SetActive(true);
            StartCoroutine("WaitForSec");
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            jumpspeed = 0;
            speed = 0;
            audioManager.PlayDoorSound();
            Invoke("LoadingMenu", 1f);
        }

        if (other.gameObject.CompareTag("TutorialFinish"))
        {
            jumpspeed = 0;
            speed = 0;
            audioManager.PlayDoorSound();
            Invoke("LoadingGame", 1f);
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        deathText.SetActive(false);
    }

    void LoadingMenu()
    {
        SceneManager.LoadScene(0);
    }

    void LoadingGame()
    {
        SceneManager.LoadScene(1);
    }
}