using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }
        playSound("main_theme");
        if (PlayerPrefs.GetInt("Music") == 0)
            PauseSound("main_theme");

        if (PlayerPrefs.GetInt("SoundEffects") == 0)
            PauseSound("all");
    }
    private void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
        }
    }
    public void playSound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.Play();
            }
        }
    }
    public void PauseSound(string name)
    {
        if(name == "all")
        {
            foreach (Sound s in sounds)
            {
                if (s.name != "main_theme")
                {
                    s.source.mute = !s.source.mute;
                }
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                if (s.name == name)
                {
                    s.source.mute = !s.source.mute;
                }
            }
        }
    }
}