using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
    public float _speed;
    public GameObject Camera;
	// Use this for initialization
	void Start () {
        // 스피드는 플레이어의 속도에 따라 배경도 빠르게 달려야해
        _speed = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-_speed, 0f, 0f);
        //transform.position = new Vector3(Camera.transform.position.x-0.1f, -1.5f, 0);
       // transform.Translate(Camera.transform.position.x, 0, 0);
        /*
        if (transform.localPosition.x - Camera.transform.position.x < -3f)
            transform.localPosition = new Vector3(-40f, 0.11f, 0);*/
    }
}
