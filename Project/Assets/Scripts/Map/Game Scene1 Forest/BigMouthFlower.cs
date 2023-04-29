using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMouthFlower : MonoBehaviour
{
    public GameObject flowerBullect;
    //冷却时间
    private float attackTime=0;
    //缩小过程时间
    private float scaleTime = 1f;
    //爆炸特效预制体
    public GameObject explosionPrefab;
    private void Update()
    {
        if (attackTime >= 4f)
        {
            //产生六个随机方向的子弹
            Instantiate(flowerBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            Instantiate(flowerBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            Instantiate(flowerBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            Instantiate(flowerBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            Instantiate(flowerBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            Instantiate(flowerBullect, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 361))));
            attackTime = 0;
        }
        else
        {
            attackTime+= Time.deltaTime;
            scaleTime -= Time.deltaTime;
        }
    }
    //敌人的死亡
    private void MapEnemyDie()
    {
        //播放缩小动画
        transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, transform.localScale, scaleTime);
        //播放爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
