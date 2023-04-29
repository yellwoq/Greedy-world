using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysController : MonoBehaviour
{
    Animator anim;//动画控制器
    private float moveSpeed = 0.8f;//移动速度
    private Vector3 bullectEulerAngles;//子弹朝向
    private float timeValue;//冷却时间
    public GameObject bullectPrefab;//子弹
    public bool isEnemy2;
    public bool isEnemy3;
    public bool isEnemy4;
    public bool isEnemy5;
    public bool isEnemy6;
    //攻击次数
    public int attackCount = 0;
    //移动的间隔时间
    private float timeValueChangeDirection = 0;
    private float h = -1, v;
    //是不是小Boss
    public bool isLittleBoss;
    public GameObject[] bossBullectPrefab;
    //爆炸特效预制体
    public GameObject explosionPrefab;
    //是否处于静止
    public bool isStopTime = true;
    //停止时间
    public float stopTime;
    public MapCreation mapCreation;
    private void Start()
    {
        anim = GetComponent<Animator>();
        //寻找游戏物体名为MapCreation的MapCreation组件
        mapCreation = GameObject.Find("MapCreation").GetComponent<MapCreation>();
        stopTime = 10f;
    }
    private void FixedUpdate()
    {
        stopTime += Time.deltaTime;
        if (stopTime <= 5f)
        {
            StopCoroutine("SetMove");
        }
        else
        {
            StartCoroutine("SetMove");
        }

    }
    IEnumerator SetMove()
    {
        EnemysMove();
        if (timeValue > AttackBetweenTime())
        {
            EnemysAttack();
        }
        else
        {
            timeValue += Time.deltaTime;
        }
        yield return 1;
    }
    //攻击间隔
    private float AttackBetweenTime()
    {
        if (isEnemy2) { return 3f; }
        else if (isEnemy3) { return 2.7f; }
        else if (isEnemy4) { return 2.5f; }
        else if (isEnemy5) { return 2.4f; }
        else if (isEnemy6) { return 2.3f; }
        else if (isLittleBoss) { return 2.2f; }
        else { return 3f; }
    }
    //角色的攻击
    private void EnemysAttack()
    {
        if (isLittleBoss)
        {
            int r = Random.Range(0, 6);
            switch (r)
            {
                case 0:
                    Instantiate(bossBullectPrefab[0], transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles+new Vector3(0,0,15)));
                    break;
                case 1:
                    Instantiate(bossBullectPrefab[1], transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles +new Vector3(0, 0, 15)));
                    break;
                case 2:
                    Instantiate(bossBullectPrefab[2], transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, 15)));
                    break;
                case 3:
                    Instantiate(bossBullectPrefab[3], transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, 15)));
                    break;
                case 4:
                    Instantiate(bossBullectPrefab[4], transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, 15)));
                    break;
                case 5:
                    Instantiate(bossBullectPrefab[5], transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, 15)));
                    break;
            }
        }
        //子弹旋转的角度：当前角色的角度+子弹应该旋转的角度
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeValue = 0;
    }
    //移动角色
    private void EnemysMove()
    {
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
        if (isEnemy3) { moveSpeed = 1f; }
        //垂直方向移动
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        //向下
        if (v < 0)
        {
            //播放下移动画
            anim.SetTrigger("Down");
            bullectEulerAngles = new Vector3(0, 0, -90);

        }
        //向上
        else if (v > 0)
        {
            //播放上移动画
            anim.SetTrigger("Up");
            bullectEulerAngles = new Vector3(0, 0, 90);


        }
        if (v != 0)
        {
            return;
        }
        //水平方向移动
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            //播放左移动画
            anim.SetTrigger("Left");
            bullectEulerAngles = new Vector3(0, 0, -180);

        }
        else if (h > 0)
        {
            //播放右移动画
            anim.SetTrigger("Right");
            bullectEulerAngles = new Vector3(0, 0, 0);

        }
    }
    //敌人的死亡
    private void EnemyDie()
    {
        attackCount++;
        //4号敌人
        if (isEnemy4)
        {
            if (attackCount <=1)
            {
                return;
            }
        }
        //5号,6号敌人
        else if (isEnemy5 || isEnemy6)
        {
            
            if (attackCount <=2)
            {
                return;
            }
        }
        else if (isLittleBoss)
        {
            //小Boss
            if (attackCount<=3)
            {
                return;
            }
        }
        int num = Random.Range(0, 8);
        if (num >= 5&& num <= 7)
        {
            int bonusNum = Random.Range(0, 9);
            if (bonusNum >= 0 && bonusNum <= 2) { Instantiate(mapCreation.bonusItem[0], transform.position, Quaternion.identity); }
            else if (bonusNum >= 3 && bonusNum <= 5) { Instantiate(mapCreation.bonusItem[1], transform.position, Quaternion.identity); }
            else { Instantiate(mapCreation.bonusItem[2], transform.position, Quaternion.identity); }
        }
        ScoreJudged();
        //播放爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    //分数与杀敌数的判断
    private void ScoreJudged()
    {
        if (isEnemy2) { PlayerManager.Instance.playerScore += 2; }
        else if (isEnemy3) { PlayerManager.Instance.playerScore += 3; }
        else if (isEnemy4) { PlayerManager.Instance.playerScore += 5; }
        else if (isEnemy5) { PlayerManager.Instance.playerScore += 7; }
        else if (isEnemy6) { PlayerManager.Instance.playerScore += 10; }
        else if (isLittleBoss) { PlayerManager.Instance.playerScore += 15; }
        else
        {
            PlayerManager.Instance.playerScore++;
        }

        PlayerManager.Instance.currentKillEnemys++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Hero" || collision.transform.tag == "Hero2")
        {
            collision.transform.SendMessage("HeroDie");
        }
        else if (collision.transform.tag == "Bridge")
        {
            collision.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (collision.transform.tag == "Castle")
        {
            collision.transform.GetComponent<BoxCollider2D>().isTrigger = true;
            collision.transform.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
}
