using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {
    public GameObject InGameMenu;

    // 팝업 비활성화
    public void OffPopup()
    {
        Time.timeScale = 1;
        InGameMenu.SetActive(false);
    }

    // 옵션으로!!
    public void Option()
    {
        Application.LoadLevel("05.Option");
    }

    // 메뉴로!!
    public void Quit()
    {
        //Popup.SetActive(true);
        Application.LoadLevel("02.Menu");
    }
}
