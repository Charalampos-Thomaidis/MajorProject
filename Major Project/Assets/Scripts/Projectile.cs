using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private Vector2 target;
    private GameObject player;

    float timer = 2;
    Vector2 Pos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        target = new Vector2(player.transform.position.x, player.transform.position.y);
        Pos = player.transform.position - transform.position;
    }
    void Update()
    {
        Kill();
        transform.position += new Vector3(Pos.x, Pos.y).normalized * speed * Time.deltaTime;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void Kill()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            DestroyProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DestroyProjectile();
    }
}