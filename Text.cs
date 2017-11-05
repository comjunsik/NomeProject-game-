using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour {
    public GameObject Stage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(TextOut());
    }

    IEnumerator TextOut()
    {
        yield return new WaitForSeconds(3);
        Stage.SetActive(false);
    }
}
