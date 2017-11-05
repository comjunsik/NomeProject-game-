using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Fade : MonoBehaviour {
    float m_lastTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        if (time - m_lastTime > 12)
        {
            Application.LoadLevel("02.Menu");
        }
    }
}
