using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    // === 외부 파라미터 =============
    public GameObject PopupGameOver;
    public bool borderCheck = false;
    public GameObject borderLeft;
    public GameObject borderUp;
    public GameObject borderRight;
    public GameObject borderDown;
    [System.NonSerialized] public float     groundY         = 0.0f;
    [System.NonSerialized] public Animator	animator;
	public float     speed           = 1.0f;
	[System.NonSerialized] public bool 		jumped 			= false;
	[System.NonSerialized] public bool 		grounded 		= false;
	[System.NonSerialized] public bool 		groundedPrev 	= false;
    [System.NonSerialized] public bool 		BlockRight  	= false;
    [System.NonSerialized] public int       jumpPos         = 0;
    [System.NonSerialized] public bool      activeSts       = true;
    [System.NonSerialized] public bool      Bounce          = false;
    [System.NonSerialized] public bool      bBounce         = true;
    [System.NonSerialized] public GameObject HpGroup;
    [System.NonSerialized] public GameObject[] HP           = new GameObject[3];
    [System.NonSerialized] public GameObject SpriteGameOver;
    [System.NonSerialized] public int       HpCnt           = 2;

    // === 내부 파라미터 ==============

    protected float         invincibleTime              = 0;
    protected bool          isInvincible                = false;
    protected bool          pause                       = false;
    protected int           jumpCount                   = 0;
    protected float         gravityScale                = 10.0f;
    protected float         jumpStartTime               = 0.0f;
    protected GameObject    groundCheck_OnRoadObject;
    protected GameObject    BlockCheck_OnRightObject;
    
    // === 캐시 ==============
    protected Transform		groundCheck_L;
	protected Transform 	groundCheck_C;
	protected Transform 	groundCheck_R;
    protected Transform     BlockCheck_R;
    protected Transform     playerTrfm;

    //애니메이션 해시 이름
    public readonly static int ANISTS_RUN =
        Animator.StringToHash("Base Layer.Player_Run");

    // === 1회실행 ==============
    protected virtual void Awake()
    {
        System.GC.Collect();
		Time.timeScale = 1;
		Physics2D.gravity = 9 * Vector2.down;
		speed = 1;
        jumpPos = 0;
        animator = GetComponent<Animator>();
        groundCheck_L = transform.Find("GroundCheck_L");
        groundCheck_C = transform.Find("GroundCheck_C");
        groundCheck_R = transform.Find("GroundCheck_R");
        BlockCheck_R = transform.Find("BlockCheck_R");
        HpGroup = GameObject.Find("HP");
        HP[0] = GameObject.Find("하트1");
        HP[1] = GameObject.Find("하트2");
        HP[2] = GameObject.Find("하트3");
        SpriteGameOver = GameObject.Find("GAMEOVER");
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        playerTrfm = transform;
    }

    // === 판단을 한다. ============
    protected void FixedUpdate()
    {
        //무엇과 충돌하는지 판별한다.
        CheckCollider();
        //죽었는지 판별한다.
        DeadIdentify();
        //점프 중인지 판별한다.
        JumpIdentify();
        //무적인지 판별한다.
        isInvincibleIdentify();
        // 캐릭터 개별 처리
        FixedUpdateCharacter();
    }

    // === 판단 후 움직인다. Player2스크립트에서 =============
    protected virtual void FixedUpdateCharacter()
    {
       
    }
    // === 카메라 이동 Player2스크립트에서 ============
    protected virtual void LateUpdate()
    {

    }

    // === 카메라 이동 Player2스크립트에서 ============
    protected virtual void GameOver()
    {

    }

    //무적판별
    void isInvincibleIdentify()
    {
        if (isInvincible)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime < 0)
            {
                isInvincible = false;
            }
        }
    }

    // 죽었을 시 활성화
    public void DeadIdentify() 
    {
        if (transform.position.y < -30.0f)
        {
            GameOver();
        }      
    }

    void JumpIdentify()
    {
        // 1회 점프시
        if (jumped)
        {
            animator.SetTrigger("Jump");
            // 1회 점프 후 착지
            if ((grounded && !groundedPrev) || (grounded && Time.fixedTime > jumpStartTime + 1.0f))
            {
                animator.SetTrigger("Run");
                jumped = false;
                jumpCount = 0;
            }
        }
        // 점프 하지 않은 상태 == 달리는 상태
        if (!jumped)
        {
            jumpCount = 0;
            animator.SetTrigger("Run");
        }
    }

    //무엇과 충돌했는지 판별한다.
    void CheckCollider()
    {
        // 지면에 닿았는지 검사
        groundedPrev = grounded;
        grounded = false;

        groundCheck_OnRoadObject = null;
        BlockCheck_OnRightObject = null;

        Collider2D[][] groundCheckCollider = new Collider2D[3][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(groundCheck_L.position);
        groundCheckCollider[1] = Physics2D.OverlapPointAll(groundCheck_C.position);
        groundCheckCollider[2] = Physics2D.OverlapPointAll(groundCheck_R.position);

        foreach (Collider2D[] groundCheckList in groundCheckCollider)
        {
            foreach (Collider2D groundCheck in groundCheckList)
            {
                if (groundCheck != null)
                {
                    if (!groundCheck.isTrigger)
                    {
                        grounded = true;
                        if (groundCheck.tag == "Down")
                        {
                            groundCheck_OnRoadObject = groundCheck.gameObject;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                            jumpPos = 1;
                        }
                        else if (groundCheck.tag == "Right")
                        {
                            groundCheck_OnRoadObject = groundCheck.gameObject;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
                            jumpPos = 2;
                        }
                        else if (groundCheck.tag == "Up")
                        {
                            groundCheck_OnRoadObject = groundCheck.gameObject;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                            jumpPos = 3;
                        }
                        else if (groundCheck.tag == "Left")
                        {
                            groundCheck_OnRoadObject = groundCheck.gameObject;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
                            jumpPos = 4;
                        }
                    }
                }
            }
        }

        // 맨 앞 빈오브젝트로 벽을 먼저 접촉해보고 어떤 벽인지 판별 후 회전함
        Collider2D[][] BlockRightCollider = new Collider2D[1][];
        BlockRightCollider[0] = Physics2D.OverlapPointAll(BlockCheck_R.position);
        foreach (Collider2D[] BlockRightCheckList in BlockRightCollider)
        {
            foreach (Collider2D BlockCheck in BlockRightCheckList)
            {
                if (BlockCheck != null)
                {
                    if (!BlockCheck.isTrigger)
                    {
                        if (BlockCheck.tag == "Right" && jumped)
                        {
                            BlockCheck_OnRightObject = BlockCheck.gameObject;
                            BlockRight = true;
                            Physics2D.gravity = 9 * Vector2.right;
                        }
                        else if (BlockCheck.tag == "Up" && jumped)
                        {
                            BlockCheck_OnRightObject = BlockCheck.gameObject;
                            BlockRight = true;
                            Physics2D.gravity = 9 * Vector2.up;
                        }
                        else if (BlockCheck.tag == "Left" && jumped)
                        {
                            BlockCheck_OnRightObject = BlockCheck.gameObject;
                            BlockRight = true;
                            Physics2D.gravity = 9 * Vector2.left;
                        }
                        else if (BlockCheck.tag == "Down" && jumped)
                        {
                            BlockCheck_OnRightObject = BlockCheck.gameObject;
                            BlockRight = true;
                            Physics2D.gravity = 9 * Vector2.down;
                        }
                        else if(BlockCheck.tag == "Enemy")
                        {
                            Bounce = true;
                            bBounce = false;
                        }
						else if(BlockCheck.tag == "Right" || BlockCheck.tag == "Up" ||
							BlockCheck.tag == "Left" || BlockCheck.tag == "Down" && !jumped)
						{
							Bounce = true;
							bBounce = false;
						}
                    }
                }
            }
        }
    }
}
