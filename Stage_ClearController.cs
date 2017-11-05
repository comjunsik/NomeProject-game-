using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_ClearController : MonoBehaviour {
    public static int StageClear = 0;
    public GameObject[] Button;
    public GameObject[] Village;

	// Update is called once per frame
	void Update () {
        if (StageClear == 0)
        {
            Button[0].SetActive(true);
        }

        else if (StageClear == 1)
        {
            Button[1].SetActive(true);
            Button[0].SetActive(false);
            Village[0].SetActive(true);
        }

        else if (StageClear == 2)
        {
            Button[2].SetActive(true);
            Button[1].SetActive(false);
            Village[1].SetActive(true);
        }

        else if (StageClear == 3)
        {
            Button[3].SetActive(true);
            Button[2].SetActive(false);
            Village[2].SetActive(true);
        }

        else if (StageClear == 4)
        {
            Button[4].SetActive(true);
            Button[3].SetActive(false);
            Village[3].SetActive(true);
        }

        else if (StageClear == 5)
        {
            Button[5].SetActive(true);
            Button[4].SetActive(false);
            Village[4].SetActive(true);
        }

        else if (StageClear == 6)
        {
            Button[6].SetActive(true);
            Button[5].SetActive(false);
            Village[5].SetActive(true);
        }
        else if(StageClear == 7)
        {
            Button[6].SetActive(false);
            Village[6].SetActive(true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StageClear++;
        }
    }
}
