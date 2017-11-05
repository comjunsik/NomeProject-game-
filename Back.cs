using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour {

    void Update()
    {
        // 홈, 뒤로가기, 메뉴 누를 시
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {
                //home button
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                Menu();
            }
            else if (Input.GetKey(KeyCode.Menu))
            {
                //menu button
            }
        }



    }

    // 메뉴로!
    public void Menu()
    {
        Application.LoadLevel("02.Menu");
    }

    public void Stage1()
    {
        Application.LoadLevel("02.GrassLand");
    }

    public void Stage2()
    {
        Application.LoadLevel("03.WaterWorld");
    }

    public void Stage3()
    {
        Application.LoadLevel("04.FossilLand");
    }

    public void Stage4()
    {
        Application.LoadLevel("05.Latifundium");
    }

    public void Stage5()
    {
        Application.LoadLevel("06.Metrolpolis");
    }

    public void Stage6()
    {
        Application.LoadLevel("07.Palace");
    }

    public void Stage7()
    {
        Application.LoadLevel("08.AppleVillage");
    }
}
