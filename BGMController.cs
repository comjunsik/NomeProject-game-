using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {
    AudioSource myAudio;

    // Use this for initialization
    void Start () {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (!BGMCNT.BGMOn)
        {
            myAudio.Stop();
        }
	}
}
