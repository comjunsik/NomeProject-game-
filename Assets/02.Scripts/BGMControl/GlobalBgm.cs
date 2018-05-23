using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBgm : MonoBehaviour {
    AudioSource myAudio;
    public AudioClip MainBGM;
    public AudioClip Store_OptionBGM;
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
        myAudio = GetComponent<AudioSource>();
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
                myAudio.Stop();
            }
        }
        else
        {
            myAudio.Pause();
        }
    }

    public void MainPlay()
    {
         if (!myAudio.isPlaying)
         {
             myAudio.loop = true;
             myAudio.PlayOneShot(MainBGM);
         }
    }
}
