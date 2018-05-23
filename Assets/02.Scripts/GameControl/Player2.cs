using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerState
{
    // === 코드(MonoBehaviour 기본 기능 구현) =========
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdateCharacter()
    {
        //회전을 준다.
        Rotation();
        //충돌처리와 무적을 작동시킨다.
        ActionisInvicible();
    }

    //카메라 움직임
    protected override void LateUpdate()
    {
        ActionCamera();
    }
    
    // 게임오버 팝업
    protected override void GameOver()
    {
        Time.timeScale = 0;
        PopupGameOver.SetActive(true);
    }

    void ActionCamera()
    {
        //카메라가 캐릭터를 따라다닌다.
        GameObject goCam = GameObject.Find("Main Camera");
        float cX = goCam.transform.position.x;
        float cY = goCam.transform.position.y;

        // 벽이 움직이더라도 자동으로 찾아서 카메라를 잘 보여줌
        if (jumpPos == 1)
        {
            if (borderCheck && cX + 8.085f > borderRight.transform.position.x + 2f) return;  //카메라 고정 Right벽에 충돌시
            goCam.transform.position = new Vector3(transform.position.x + 4f, borderDown.transform.position.y + 3.6f, -10f);  //Down에 따라가는 카메라
            HpGroup.transform.position = new Vector2(-1f, -3f);
        }
        else if (jumpPos == 2)
        {
            if (borderCheck && cY + 3.65f > borderUp.transform.position.y) return;      // 카메라 고정 Up벽에 충돌시
            goCam.transform.position = new Vector3(borderRight.transform.position.x - 6f, transform.position.y + 1f, -10f);  // Right에 따라가는 카메라
            HpGroup.transform.position = new Vector2(-30f, 52f);
        }
        else if (jumpPos == 3)
        {
            if (borderCheck && cX - 8.385f < borderLeft.transform.position.x - 2.35f) return;   // 카메라 고정 Left 벽에 충돌시
            goCam.transform.position = new Vector3(transform.position.x - 5.0f, borderUp.transform.position.y - 3.6f, -10f);  // Up에 따라가는 카메라
            HpGroup.transform.position = new Vector2(-79f, 22.5f);
        }
        else if (jumpPos == 4)
        {
            if (borderCheck && cY - 3.38f < borderDown.transform.position.y + 0.14f) return;  // 카메라 고정 Down 벽에 충돌시
            goCam.transform.position = new Vector3(borderLeft.transform.position.x + 5.86f, transform.position.y - 1.45f, -10f);  // Left에 따라가는 카메라
            HpGroup.transform.position = new Vector2(-49.5f, -32.5f);
        }
    }

    // 충돌 시 HP감소와 무적을 작동시킴, 또한 HP가 0이 되었을 시 게임오버 작동
    void ActionisInvicible()
    {
        if (!isInvincible && Bounce)
        {
            //풀피일때
            if (HpCnt == 2)
            {
                HP[2].SetActive(false);
            }
            else if (HpCnt == 1)
            {
                HP[1].SetActive(false);
            }
            else if (HpCnt == 0)
            {
                HP[0].SetActive(false);
                animator.SetTrigger("Dead");
                StartCoroutine(DeadState());
            }
            HpCnt--;
            animator.SetTrigger("Collision");
            speed = 0;
            SetInvincible(2.5f);
            StartCoroutine(ActionDelay());
            Bounce = false;
            //속도가 천천히 정상궤도로 올라왔으면 좋겠다. 아니면 기울기로 속도가 천천히 올랐으면 좋겠다.
            speed = 10;
        }
        else  Bounce = false;
    }

    // 충돌 후 2초 뒤 이동
    IEnumerator ActionDelay()
    {
        yield return new WaitForSeconds(2);
        if (jumpPos == 1)
        {
            transform.position = new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z);
        }
        else if (jumpPos == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 5.0f, transform.position.z);
        }
        else if (jumpPos == 3)
        {
            transform.position = new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z);
        }
        else if (jumpPos == 4)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
        }
        bBounce = true;
    }

    // HP==0됬을 때 1초 후
    IEnumerator DeadState()
    {
        yield return new WaitForSeconds(1);
        GameOver();
    }

    // 재시작
    public void ReTry()
    {
        PopupGameOver.SetActive(false);
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevelName);
    }

    // 무적 활성화
    void SetInvincible(float time)
    {
        isInvincible = true;
        invincibleTime = time;
    }

    //점프하면서 벽과 충돌한다면 회전하세요.
    void Rotation()
    {
        if (BlockRight && jumped)
        {
            BlockRight = false;
            transform.Rotate(0, 0, 90f);
            HpGroup.transform.Rotate(new Vector3(0, 0, 90f));
        }
    }

    //어택 활성화
    public void ActionAttack()
    {
        animator.SetTrigger("Attack");
    }

    //슬라이드 활성화
    public void ActionSlide()
    {
        animator.SetTrigger("Slide");
    }

    public void ColliderAttack()
    {
        animator.SetTrigger("Collider_Attack");
    }

    //점프 활성화
    public void ActionJump()
    {
        switch (jumpCount)
        {
            case 0:
                animator.SetTrigger("Jump");
                jumpStartTime = Time.fixedTime;
                jumped = true;
                jumpCount++;
                if (grounded && groundCheck_OnRoadObject.tag == "Down")
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * 10.0f;
                }
                else if (grounded && groundCheck_OnRoadObject.tag == "Right")
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.left * 10.0f;
                }
                else if (grounded && groundCheck_OnRoadObject.tag == "Up")
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.down * 10.0f;
                }
                else if (grounded && groundCheck_OnRoadObject.tag == "Left")
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.right * 10.0f;
                }
                break;
            case 1:
                animator.Play("Player_Jump", 0, 0.0f);
                jumped = true;
                jumpCount++;
                if (!grounded && jumpPos == 1)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10f);
                }
                else if (!grounded && jumpPos == 2)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (!grounded && jumpPos == 3)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -10f);
                }
                else if (!grounded && jumpPos == 4)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(10f, GetComponent<Rigidbody2D>().velocity.y);
                }
                break;
        }
    }
}
