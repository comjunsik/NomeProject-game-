using UnityEngine;
using System.Collections;

public class Start_LoGo : MonoBehaviour {
    float m_lastTime = 0.0f;

	// Update is called once per frame
	void Update () {
        float time = Time.time;
        if (time - m_lastTime > 9)
        {
            Application.LoadLevel("02-1Fade");
        }
    }
}
