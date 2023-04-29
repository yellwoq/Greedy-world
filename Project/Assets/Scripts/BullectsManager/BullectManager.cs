using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullectManager : MonoBehaviour
{
    //子弹移动速度
    private float moveSpeed = 3f;
    //是不是玩家子弹
    public bool isPlayerBullect;
    //是不是3号玩家子弹
    public bool isPlayerBullect3;
    //是不是2号敌人子弹
    public bool isEnemyBullect2;
    //是不是3号敌人子弹
    public bool isEnemyBullect3;
    //是不是5号敌人子弹
    public bool isEnemyBullect5;
    //是不是6号敌人子弹
    public bool isEnemyBullect6;
    public bool isBossBullect;
    //打击总次数
    public int hitCount = 0;
    public bool hasChange;
    private float v, h;
    //移动的间隔时间
    private float timeValueChangeDirection = 0;
    private Vector3 bullectDirector;
    //转向时间
    private float turnTime = 0;
    //缩放时间
    private float scaleTime = 0;
    private float scaleBackTime = 0;
    //当前子弹的动画控制
    private Animator bullectAnim;
    //动画改变冷却时间
    private float changeAnim = 0;
    private Vector3 bullectEulerAngles;
    //获得动画控制器
    private Animator anim;
    private MapCreation mapCreation;
    public GameObject dieEffect;
    public AudioClip hitClip;
    private void Start()
    {
        bullectAnim = GetComponent<Animator>();
        //寻找游戏物体名为MapCreation的MapCreation组件
        mapCreation = GameObject.Find("MapCreation").GetComponent<MapCreation>();
    }

    void FixedUpdate()
    {
        BullectMove();
    }
    //子弹的移动
    private void BullectMove()
    {
        if (isBossBullect)
        {
            //随机向四个方向快速移动
            moveSpeed = 4f;
            int num = Random.Range(0, 4);
            if (num == 0) { transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World); }
            else if (num == 1) { transform.Translate(-transform.right * moveSpeed * Time.deltaTime, Space.World); }
            else if (num == 2) { transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World); }
            else if (num == 3) { transform.Translate(-transform.up * moveSpeed * Time.deltaTime, Space.World); }
        }
        //敌人子弹2：移动速度加快
        if (isEnemyBullect2)
        {
            moveSpeed = 3.2f;
            transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
        //敌人子弹3：可以打障碍
        //敌人子弹5：可改变方向
        if (isEnemyBullect5)
        {
            if (timeValueChangeDirection >= 2f)
            {
                int num = Random.Range(0, 10);
                //向左
                if (num >= 6)
                {
                    v = 0;
                    h = -1;
                }
                //向右
                else if (num == 4 || num == 5)
                {
                    v = 0;
                    h = -1;
                }
                //向上
                else if (num == 0 || num == 1)
                {
                    h = 0;
                    v = 1;
                }
                //向下
                else if (num == 2 || num == 3)
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
            turnTime = turnTime + Time.fixedDeltaTime * 0.5f;
            bullectEulerAngles = transform.eulerAngles;
            //播放转向动画
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(bullectEulerAngles), Quaternion.Euler(0, 0, 180), turnTime);
            //垂直方向移动
            transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
            //水平方向移动
            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
        //敌人子弹6：可转换形态，放大
        else if (isEnemyBullect6)
        {
            scaleTime += Time.fixedDeltaTime;

            if (scaleTime <= 1f)
            {
                //产生放大动画
                transform.localScale = Vector3.Lerp(new Vector3(0.3f, 0.3f, 1), new Vector3(0.9f, 0.9f, 0.9f), scaleTime);
            }
            else if (scaleTime > 1f)
            {
                scaleBackTime += Time.deltaTime;
                //缩小
                transform.localScale = Vector3.Lerp(new Vector3(0.9f, 0.9f, 0.9f), new Vector3(0.5f, 0.5f, 1), scaleBackTime);
            }
            else
            {
                scaleTime += Time.fixedDeltaTime;
            }
            if (changeAnim >= 1f)
            {
                int value = Random.Range(0, 10);
                //转换动画
                if (value <= 3)
                {
                    bullectAnim.SetTrigger("IsEnemy3");
                }
                else if (value > 3 && value <= 6)
                {
                    bullectAnim.SetTrigger("IsEnemy4");
                }
                else
                {
                    bullectAnim.SetTrigger("IsEnemy5");
                }
            }
            else
            {
                changeAnim += Time.deltaTime;
            }
            transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim = collision.GetComponent<Animator>();
        if (isBossBullect)
        {
            if (collision.tag != "Enemy" && collision.tag != "MapEnemy" &&
           collision.tag != "CityWall" && collision.tag != "AirBarriar" && collision.tag != "Open"
           && collision.tag != "Stairs" && collision.tag != "Boss")
            {
                if (collision.tag == "Hero" || collision.tag == "Hero2") { collision.transform.SendMessage("HeroDie"); }
                Destroy(collision.gameObject);
            }
            Destroy(gameObject, 3f);
        }
        switch (collision.tag)
        {
            case "TreeOrWall":
            case "Immortal":
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(collision.gameObject);
                break;
            case "AirBarriar": Destroy(gameObject); break;
            case "Barriar":
                AudioManager._instance.PlaySound(hitClip);
                if (isEnemyBullect3 || isPlayerBullect3||isBossBullect)
                {
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject); break;
            case "Shovel":
            case "Food":
                AudioManager._instance.PlaySound(hitClip);
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                if (collision.tag == "Food")
                {
                    anim.SetTrigger("Attack");
                    collision.SendMessage("MissTime");
                }
                else
                {
                    collision.SendMessage("ShovelDie");
                }
                Destroy(gameObject);
                break;
            case "Hero":
            case "Hero2":
                AudioManager._instance.PlaySound(hitClip);
                if (!isPlayerBullect)
                {
                    collision.SendMessage("HeroDie");
                    Destroy(gameObject);
                }
                break;
            case "MapEnemy":
            case "Enemy":
                if (isPlayerBullect)
                {
                    if (collision.tag == "Enemy")
                    {
                        collision.SendMessage("EnemyDie");
                    }
                    else if (collision.tag == "MapEnemy")
                    {
                        PlayerManager.Instance.playerScore += 2;
                        collision.SendMessage("MapEnemyDie");
                    }
                    Destroy(gameObject);
                }
                break;
            case "Move Battery":
                if (!isPlayerBullect)
                {
                    int num = Random.Range(0, 8);
                    if (num >= 5 && num <= 7)
                    {
                        Instantiate(mapCreation.bonusItem[Random.Range(0, 3)],
                            collision.transform.position, Quaternion.identity);
                    }
                    collision.SendMessage("MapHelperDie");
                    Destroy(gameObject);
                }
                break;
            case "Castle":
            case "Castle2":
                collision.GetComponent<Castle>().shotTime = 0;
                collision.GetComponent<EdgeCollider2D>().isTrigger = true;
                break;
            case "Boss":
                if (isPlayerBullect)
                {
                    AudioManager._instance.PlaySound(hitClip);
                    Instantiate(dieEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    collision.SendMessage("BossDie");
                }
                break;
        }
    }
}
