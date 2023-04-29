using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float v, h;
    float timeValueChangeDirection;
    private float moveSpeed = 0.2f;
    void Update()
    {
        EveryBodyMove();
    }
    //移动
    private void EveryBodyMove()
    {
        if (Mathf.Abs(transform.position.x) >= 7.5f || Mathf.Abs(transform.position.y) >= 3.5f)
        {
            transform.position = new Vector3(Random.Range(-3, 4), Random.Range(-2, 2), 0);
        }
        if (timeValueChangeDirection >= 1f)
        {
            int num = Random.Range(0, 8);
            //向左
            if (num >= 5)
            {
                v = 0;
                h = -1;
            }
            //向右
            else if (num == 0)
            {
                v = 0;
                h = 1;
            }
            //向上
            else if (num > 0 && num <= 2)
            {
                h = 0;
                v = 1;
            }
            //向下
            else if (num > 2 && num <= 4)
            {
                h = 0;
                v = -1;
            }
            timeValueChangeDirection = 0;
        }
        else
        {
            timeValueChangeDirection += Time.fixedDeltaTime;
        }
        //垂直方向移动
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        //水平方向移动
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
}
