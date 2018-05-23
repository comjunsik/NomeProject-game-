using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Menu : MonoBehaviour {
    void Update()
    {
        // 홈, 뒤로가기, 메뉴 누를 시
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
            }
            else if (Input.GetKey(KeyCode.Menu))
            {
                //menu button
            }
        }
    }

    public void Stage()
    {
        Application.LoadLevel("01.Stage");
    }

    public void Developer()
    {
        Application.LoadLevel("05.Developer");
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        Application.LoadLevel("02.GameMode");
    }

    public void Store()
    {
        Application.LoadLevel("03.Store");
    }

    public void Option()
    {
        Application.LoadLevel("04.Option");
    }

    public void Quit()
    {
        Application.Quit();
        return;
    }
}
