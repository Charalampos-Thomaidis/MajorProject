using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject platform;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.GetAudioManager();
        platform.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            platform.SetActive(true);
            audioManager.PlayPuzzleSound();
        }
    }
}