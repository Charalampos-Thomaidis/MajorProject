using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    public void Start()
    {
        audioManager = AudioManager.GetAudioManager();
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
        Click();
    }

    public void Menu ()
    {
        SceneManager.LoadScene(0);
        Click();
    }

    public void Quit()
    {
        Application.Quit();
        Click();
    }

    private void Click()
    {
        audioManager.PlayClickMenuSound();
    }
}