using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBattery : MonoBehaviour
{
    public GameObject moverBullect;
    //冷却时间
    private float attackTime = 0;
    //缩小过程时间
    private float scaleTime = 1f;
    //爆炸特效预制体
    public GameObject explosionPrefab;
    float v, h;
    float timeValueChangeDirection;
    private float moveSpeed=0.2f;
    private void Update()
    {
        MapHelperMove();
        if (attackTime >= 5f)
        {
            MapHelperAttack();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }
    //友军的攻击
    private void MapHelperAttack()
    {
        //产生三个随机方向的子弹
        Instantiate(moverBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
        Instantiate(moverBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
        Instantiate(moverBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
    }
    //友军的移动
    private void MapHelperMove()
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
    //友军的死亡
    private void MapHelperDie()
    {
        //播放缩小动画
        transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, transform.localScale, scaleTime);
        //播放爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
