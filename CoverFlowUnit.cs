using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverFlowUnit : MonoBehaviour {

    Transform mTrans;
    UIPanel mPanel;
    UIWidget mWidget;
    float cellWidth;
    float downScale;

	// Use this for initialization
	void Start () {
        mTrans = transform;
        mPanel = mTrans.parent.parent.GetComponent<UIPanel>();
        mWidget = GetComponent<UIWidget>();

        cellWidth = 400;        //이걸늘리면 스프라이트 끼리 가까이붙음
        downScale = 0.35f;	
	}

    float pos, dist;

    void FixedUpdate()
    {
        pos = mTrans.localPosition.x - mPanel.clipOffset.x;
        dist = Mathf.Clamp(Mathf.Abs(pos), 0f, cellWidth);
        mWidget.width = System.Convert.ToInt32(((cellWidth - dist * downScale) / cellWidth) * cellWidth);
    }
}
