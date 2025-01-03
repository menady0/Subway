using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//using UnityEngine.UIElements;
public class Events : MonoBehaviour
{
    public TMP_InputField input;
    public GameObject SettingsContainerPanel;
    public GameObject InputFieldPanel;
    public TextMeshProUGUI errorMessage;

    public Image musicBtn;
    public Image soundEffectsBtn;
    public Sprite withSound;
    public Sprite withoutSound;

    bool isMusic;
    bool isSoundEffects;

    bool isSettingsOpen = false;
    bool isNewGame = false;

    private void Start()
    {

        isMusic = true;
        isSoundEffects = true;

        if (musicBtn != null)
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                isMusic = true;
                musicBtn.sprite = withSound;
            }
            else
            {
                isMusic = false;
                musicBtn.sprite = withoutSound;
            }


            if (PlayerPrefs.GetInt("SoundEffects") == 1)
            {
                isSoundEffects = true;
                soundEffectsBtn.sprite = withSound;
            }
            else
            {
                isSoundEffects = false;
                soundEffectsBtn.sprite = withoutSound;
            }
        }

    }

    public void StartGame()
    {
        if(input.text != "")
        {
            PlayerManager.username = input.text.ToLower();

            if (PlayerManager.players.Count != 0)
            {
                for (int i = 0; i < PlayerManager.players.Count; i++)
                {
                    if (PlayerManager.username != PlayerManager.players[i].name && i == PlayerManager.players.Count - 1)
                    {
                        Person player = new Person();
                        player.name = PlayerManager.username;
                        player.highestScore = 0;
                        PlayerManager.players.Add(player);
                        //Debug.Log("Player Created");
                    }
                }
            }
            else
            {
                Person player = new Person();
                player.name = PlayerManager.username;
                player.highestScore = 0;
                PlayerManager.players.Add(player);
                //Debug.Log("First Player Created");
            }
            if (isMusic)
                PlayerPrefs.SetInt("Music", 1);
            else
                PlayerPrefs.SetInt("Music", 0); 

            if (isSoundEffects)
                PlayerPrefs.SetInt("SoundEffects", 1);
            else
                PlayerPrefs.SetInt("SoundEffects", 0);

            SceneManager.LoadScene("Game");
        }
        else
            errorMessage.text = "Can't leave field empty!";
    }
    public void NewGame()
    {
        isNewGame = !isNewGame;
        InputFieldPanel.SetActive(isNewGame);
    }
    public void Settings()
    {
        isSettingsOpen = !isSettingsOpen;
        SettingsContainerPanel.SetActive(isSettingsOpen);
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Music()
    {
        if (isMusic)
        {
            musicBtn.sprite = withoutSound;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            musicBtn.sprite = withSound;
        }
        isMusic = !isMusic;

        FindObjectOfType<AudioManager>().PauseSound("main_theme");
    }
    public void SoundEffects()
    {
        if (isSoundEffects)
        {
            soundEffectsBtn.sprite = withoutSound;
            PlayerPrefs.SetInt("SoundEffects", 0);
        }
        else
        {
            PlayerPrefs.SetInt("SoundEffects", 1);
            soundEffectsBtn.sprite = withSound;
        }
        isSoundEffects = !isSoundEffects;

        FindObjectOfType<AudioManager>().PauseSound("all");
    }


}
