using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSfx : MonoBehaviour {
    AudioSource myAudioSfx;
    public AudioClip MainSfx;
    public bool DontDestroyEnabled = true;

    private void Awake()
    {
        if (DontDestroyEnabled)
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        myAudioSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "01.LoGo" ||
            Application.loadedLevelName == "02-1Fade" ||
            Application.loadedLevelName == "02.Menu" ||
            Application.loadedLevelName == "01.Stage")
        {
            if (BGMCNT.BGMOn)
            {
                MainPlay();
            }
            else
            {
                myAudioSfx.Stop();
            }
        }
        else
        {
            myAudioSfx.Pause();
        }
    }

    public void MainPlay()
    {
        if (!myAudioSfx.isPlaying)
        {
            myAudioSfx.PlayOneShot(MainSfx);
        }
    }

}
