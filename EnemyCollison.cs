using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollison : MonoBehaviour {

    //GameObject enemyCtrl;
    GameObject Player;
    Animator PlayerAnim;
    int attackHash = 0;

    void Awake()
    {
        Player = GameObject.Find("Player (1)");
        PlayerAnim = Player.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name);
        if (other.name == "Collider_Attack")
        {
            Debug.Log("1단계 작동!");
            AnimatorStateInfo stateInfo = PlayerAnim.GetCurrentAnimatorStateInfo(0);
            if(attackHash!=stateInfo.nameHash)
            {
                attackHash = stateInfo.nameHash;
                ActionDamage();
            }
        }
    }


    public void ActionDamage()
    {
        Debug.Log("2단계 작동!");
        //GetComponent<Rigidbody2D>().AddForce(new Vector3(10f, 20.0f,0));
        //GetComponent<Rigidbody2D>().velocity = new Vector2(10, 5);
        StartCoroutine(EnemyTime());
        
    }
    IEnumerator EnemyTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update () {
        AnimatorStateInfo stateInfo = PlayerAnim.GetCurrentAnimatorStateInfo(0);
        if(attackHash!=0&& stateInfo.nameHash==PlayerState.ANISTS_RUN)
        {
            attackHash = 0;
        }
    }
}
