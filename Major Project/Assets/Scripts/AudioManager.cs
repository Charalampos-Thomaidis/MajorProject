using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource BackGroundMusic;
    private AudioSource EyeAttackSound;
    private AudioSource JumpSound;
    private AudioSource SwitchSound;
    private AudioSource ClickMenuSound;
    private AudioSource PuzzleSound;
    private AudioSource FantasyBackgroundMusic;
    private AudioSource MushroomAttackSound;
    private AudioSource SpikeSound;
    private AudioSource DoorSound;

    public AudioSource[] audioSources;

    private static AudioManager inst = null;

    public static AudioManager GetAudioManager()
    {
        return inst;
    }
    public void Awake()
    {
        inst = this;

        audioSources = GetComponentsInChildren<AudioSource>();

        BackGroundMusic = audioSources[0];
        BackGroundMusic.loop = true;

        EyeAttackSound = audioSources[1];
        EyeAttackSound.loop = false;

        JumpSound = audioSources[2];
        JumpSound.loop = false;

        SwitchSound = audioSources[3];
        SwitchSound.loop = false;

        ClickMenuSound = audioSources[4];
        ClickMenuSound.loop = false;

        PuzzleSound = audioSources[5];
        PuzzleSound.loop = false;

        FantasyBackgroundMusic = audioSources[6];
        FantasyBackgroundMusic.loop = true;

        MushroomAttackSound = audioSources[7];
        MushroomAttackSound.loop = false;

        SpikeSound = audioSources[8];
        SpikeSound.loop = false;

        DoorSound = audioSources[9];
        DoorSound.loop = false;
    }

    public void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        BackGroundMusic.Play();
        FantasyBackgroundMusic.Stop();
    }
    public void PlayEyeAttackSound()
    {
        EyeAttackSound.Play();
    }
    public void PlayJumpSound()
    {
        JumpSound.Play();
    }
    public void PlaySwitchSound()
    {
        SwitchSound.Play();
    }
    public void PlayClickMenuSound()
    {
        ClickMenuSound.Play();
    }
    public void PlayPuzzleSound()
    {
        PuzzleSound.Play();
    }
    public void PlayFantasyBackgroundMusic()
    {
        FantasyBackgroundMusic.Play();
        BackGroundMusic.Stop();
    }
    public void PlayMushroomAttackSound()
    {
        MushroomAttackSound.Play();
    }
    public void PlaySpikeSound()
    {
        SpikeSound.Play();
    }
    public void PlayDoorSound()
    {
        DoorSound.Play();
    }
}