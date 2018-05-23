using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour {

    // === 내부 파라미터 ===============
    Player2 PlayerController;
    public GameObject InGameMenu;
    
    //터치 및 슬라이드, 공격 구분 짓기 위한 파라미터
    delegate void listener(ArrayList touches);
    event listener touchBegin, touchMove, touchEnd;
    private float beginX, beginY, endX, endY;
    private float dx, dy, distance;
    protected bool bPause = false;

	// Update is called once per frame
	void Update () {/*
		KeyMove ();
        // 홈, 뒤로가기, 메뉴 누를 시
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                //if() 게임화면일땐 메뉴팝업을 활성화 시키고, 
                //상점이나 옵션같은 곳에서 누를 경우는 Back과 같은 역할을 한다.
                OnPopup();
                Time.timeScale = 0;
            }
        }
       
        // 터치 및 슬라이드, 공격 구분 짓기
        int count = Input.touchCount;
        if (count == 0) return;

        bool begin, move, end;
        begin = move = end = false;

        ArrayList result = new ArrayList();

        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);
            result.Add(touch);
            if (touch.phase == TouchPhase.Began && touchBegin != null)
            {
                beginX = touch.position.x;
                beginY = touch.position.y;
                begin = true;
            }
            else if (touch.phase == TouchPhase.Moved && touchMove != null) move = true;
            else if (touch.phase == TouchPhase.Ended && touchEnd != null)
            {
                endX = touch.position.x;
                endY = touch.position.y;
                end = true;
                dx = touch.position.x - beginX;
                dy = touch.position.y - beginY;
            }
        }
        if (begin) touchBegin(result);
        if (end) touchEnd(result);
        if (move) touchMove(result);
       
        //1회 터치시 점프*/
        if (Input.GetButtonDown("Fire1") && distance < 10 && PlayerController.bBounce)
        {
            //Handheld.Vibrate();
            PlayerController.ActionJump();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            PlayerController.ColliderAttack();
        }
    }
    
	//기울었을 때 작동 가중기
	public void KeyMove(){
		Vector2 dir = Vector2.zero;
		if (PlayerController.jumpPos == 1) {
			if (Input.acceleration.x < 0) {
				dir.x = 0.1f;
			} else {
				dir.x = Input.acceleration.x;
			}	
		}
		else if (PlayerController.jumpPos == 2) {
			if (Input.acceleration.y < 0) {
				dir.x = 0.1f;
			} else {
				dir.x = Input.acceleration.y; 
			}
		}
		else if (PlayerController.jumpPos == 3) {
			if (Input.acceleration.x > 0) {
				dir.x = 0.1f;
			} else {
				dir.x = -Input.acceleration.x;  
			}  
		}
		else if (PlayerController.jumpPos == 4) {
			if (Input.acceleration.y > 0) {
				dir.x = 0.1f;
			} else {
				dir.x = -Input.acceleration.y;  
			} 
		}
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		dir *= Time.deltaTime;
		transform.Translate (dir * PlayerController.speed * 10f);
	}

    //팝업창 활성화
    public void OnPopup()
    {
        InGameMenu.SetActive(true);
    }

    //팝업창 비활성화
    public void OffPopup()
    {
        Time.timeScale = 1;
        InGameMenu.SetActive(false);
    }

    // distance와 기울기 측정
    float Gradient(float bx, float by, float ex, float ey)
    {
        distance = Mathf.Sqrt(dx * dx + dy * dy);
        return (ey - by) / (ex - bx);
    }

    void Start()
    {
        PlayerController = GetComponent<Player2>();
        touchBegin += (touches) => { Debug.Log("Begin" + beginX + beginY); };
        touchMove += (touches) => { Debug.Log("Move"); };
        touchEnd += (touches) => {
            Debug.Log("End" + endX + endY + "dx :" + dx + "dy" + dy);
            float grad = Gradient(beginX, beginY, endX, endY);
            if (grad > -0.5 && grad < 0.4 && distance >= 80 && PlayerController.grounded)
            {
                Debug.Log("슬라이드 작동!" + distance);
                gameObject.GetComponent<CircleCollider2D>().radius = 0.2f;
                gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.5f);
                PlayerController.ActionSlide();
                if (PlayerController.jumpPos == 1)
                {
                    GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x + 0.94f, gameObject.transform.position.y - 0.4f);
                }
                else if (PlayerController.jumpPos == 2)
                {
                    GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x + 0.4f, gameObject.transform.position.y + 0.94f);
                }
                else if (PlayerController.jumpPos == 3)
                {
                    GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x - 0.94f, gameObject.transform.position.y + 0.4f);
                }
                else if (PlayerController.jumpPos == 4)
                {
                    GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x - 0.4f, gameObject.transform.position.y - 0.94f);
                }
                StartCoroutine(SliderT());
            }
            else if (grad >= 0.4 && grad < 2 && distance >= 80 && PlayerController.grounded)
            {
                Debug.Log("공격 작동!" + distance);
                PlayerController.ActionAttack();
            }
        };
    }
    IEnumerator SliderT()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.1f);
        if (PlayerController.jumpPos == 1)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.1f);
            GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x + 0.94f, gameObject.transform.position.y - 0.1f);
        }
        else if (PlayerController.jumpPos == 2)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.1f);
            GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y + 0.94f);
        }
        else if (PlayerController.jumpPos == 3)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.1f);
            GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x - 0.94f, gameObject.transform.position.y + 0.1f);
        }
        else if (PlayerController.jumpPos == 4)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.1f);
            GameObject.Find("BlockCheck_R").transform.position = new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y - 0.94f);
        }
        gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.1f);
    }
}
