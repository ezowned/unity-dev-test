using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//класс для отслеживания одинарных и двойных кликов по объекту

public class ClickHandler : MonoBehaviour {

    private int clicks = 0;
    private float lastTime = 0;
    private float maxTime = 0.2f;

    private BallController ball;

    private void Awake()
    {
        ball = GetComponent<BallController>();
    }

    private void Update () {

        if(ball==null)
        {
            return;
        }


        if(clicks!=0)
        {
            var tm = Time.realtimeSinceStartup;
            if(tm-lastTime >= maxTime)
            {
                if(clicks==1)
                {
                    ball.OnClick();
                }
                else
                {
                    ball.OnDoubleClick();
                }

                lastTime = tm;
                clicks = 0;
            }
        }
	}

    private void OnMouseDown()
    {
        clicks++;
        lastTime = Time.realtimeSinceStartup;
    }
}
