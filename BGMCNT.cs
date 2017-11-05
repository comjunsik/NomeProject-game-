using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCNT : MonoBehaviour {
    public static bool BGMOn = true;
    public static bool SFXOn = true;
    public GameObject GameBGMOn;
    public GameObject GameBGMOff;
    public GameObject GameSFXOn;
    public GameObject GameSFXOff;

    void Update () {
        if (!BGMOn)
        {
            GameBGMOn.SetActive(false);
            GameBGMOff.SetActive(true);
        }
        else
        {
            GameBGMOn.SetActive(true);
            GameBGMOff.SetActive(false);
        }

        if (!SFXOn)
        {
            GameSFXOn.SetActive(false);
            GameSFXOff.SetActive(true);
        }
        else
        {
            GameSFXOn.SetActive(true);
            GameSFXOff.SetActive(false);
        }
    }

    public void BGM_OnOFF()
    {
        if (BGMOn)
        {
            BGMOn = false;
        }
        else
        {
            BGMOn = true;
        }
    }

    public void SFX_OnOFF()
    {
        if (SFXOn)
        {
            SFXOn = false;
        }
        else
        {
            SFXOn = true;
        }
    }
}
