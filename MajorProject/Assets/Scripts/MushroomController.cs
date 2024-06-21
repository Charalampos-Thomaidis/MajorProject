using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public Transform pos1, pos2;
    public Transform startPos;
    public float speed;

    private AudioManager audioManager;
    private Animator anim;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;

        anim = GetComponent<Animator>();
        audioManager = AudioManager.GetAudioManager();
    }

    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
            transform.Rotate(0, -180, 0);
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
            transform.Rotate(0, 180, 0);
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("mushroom_grey_attack_anim");
            audioManager.PlayMushroomAttackSound();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
