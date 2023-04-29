using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator anim;//动画控制器
    private float moveSpeed;//移动速度
    //子弹朝向
    private List<Vector3> bullectEulerAngles = new List<Vector3>();
    private float timeValue;//冷却时间

    //移动方向的改变时间
    private float timeValueChangeDirection = 0;
    //爪攻时间
    private float crapTime = 0;
    //向上的力
    private float Force;
    //跳跃时间
    private float jumpTime = 0;
    private Rigidbody2D rig;
    private float h = -1, v;
    //子弹
    public GameObject bossBullectPrefab1;
    public GameObject[] bossBullectPrefab;
    //攻击次数
    public int AttackCount = 0;
    //出现音效
    public AudioClip bossBornClip,crapClip,dieClip;
    public MapCreation mapCreation;
    private void Start()
    {
        AudioManager._instance.PlaySound(bossBornClip);
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        //寻找游戏物体名为MapCreation的MapCreation组件
        mapCreation = GameObject.Find("MapCreation").GetComponent<MapCreation>();
    }

    private void FixedUpdate()
    {

        BossMove();
        if (timeValue >= 2f)
        {

            BossAttack();
        }
        else
        {
            timeValue += Time.deltaTime;
        }
    }
    //角色的射击
    private void BossAttack()
    {
        //射击
        int num = Random.Range(0, 3);
        if (num == 0)
        {
            Instantiate(bossBullectPrefab1, new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0), Quaternion.Euler(transform.eulerAngles));
        }
        foreach (var item in bullectEulerAngles)
        {
            //子弹旋转的角度：当前角色的角度+子弹应该旋转的角度
            int r = Random.Range(0, 6);
            switch (r)
            {
                case 0:
                    Instantiate(bossBullectPrefab[0], transform.position, Quaternion.Euler(transform.eulerAngles + item));
                    break;
                case 1:
                    Instantiate(bossBullectPrefab[1], transform.position, Quaternion.Euler(transform.eulerAngles + item));
                    break;
                case 2:
                    Instantiate(bossBullectPrefab[2], transform.position, Quaternion.Euler(transform.eulerAngles + item));
                    break;
                case 3:
                    Instantiate(bossBullectPrefab[3], transform.position, Quaternion.Euler(transform.eulerAngles + item));
                    break;
                case 4:
                    Instantiate(bossBullectPrefab[4], transform.position, Quaternion.Euler(transform.eulerAngles + item));
                    break;
                case 5:
                    Instantiate(bossBullectPrefab[5], transform.position, Quaternion.Euler(transform.eulerAngles + item));
                    break;
            }
        }

        timeValue = 0;
    }
    //移动角色
    private void BossMove()
    {
        if (timeValueChangeDirection >= 0.5f)
        {
            int num = Random.Range(0, 8);
            //向左
            if (num >= 6)
            {
                v = 0;
                h = -1;
            }
            //向右
            else if (num == 0 || num == 1)
            {
                v = 0;
                h = 1;
            }
            //向上
            else if (num > 1 && num <= 3)
            {
                h = 0;
                v = 1;
            }
            //向下
            else if (num > 3 && num <= 5)
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
        HorizationBullect();
        //垂直方向移动
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v != 0)
        {
            return;
        }
        //水平方向移动
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        Jump();
    }
    //角色的死亡
    public void BossDie()
    {
        AttackCount++;
        if (AttackCount <= 20)
        {
            return;
        }
        PlayerManager.Instance.playerScore += 50;
        PlayerManager.Instance.isOver = true;
        int num = Random.Range(0, 8);
        if ( num == 7)
        {
            Instantiate(mapCreation.bonusItem[Random.Range(0, 3)], transform.position, Quaternion.identity);
        }
        AudioManager._instance.PlaySound(dieClip);
        if (h < 0) { anim.SetTrigger("IsDieLeftTime"); }
        else if (h > 0) { anim.SetTrigger("IsDieRightTime"); }
        Destroy(gameObject, 1);

    }
    //子弹及角色的水平移动方向
    private void HorizationBullect()
    {
        //向左
        if (h < 0)
        {
            if (crapTime >= 2.5f)
            {
                AudioManager._instance.PlaySound(crapClip);
                //播放抓取动画
                anim.SetBool("IsCrapLeftTime", true);
                crapTime = 0;
            }
            else
            {

                //将抓取动画关闭
                anim.SetBool("IsCrapLeftTime", false);
                //回归原动画
                anim.SetFloat("CrapLeft Time", 1.1f);
                crapTime += Time.fixedDeltaTime;
                //播放左移动画
                anim.SetTrigger("IsWalkLeft");
                //置空
                bullectEulerAngles.Clear();
                //设置子弹方向
                bullectEulerAngles.Add(new Vector3(0, 0, -180));
                bullectEulerAngles.Add(new Vector3(0, 0, -175));
                bullectEulerAngles.Add(new Vector3(0, 0, -185));
                moveSpeed = 1f;
            }
        }
        //向右
        else if (h > 0)
        {
            if (crapTime >= 2.5f)
            {
                AudioManager._instance.PlaySound(crapClip);
                //播放抓取动画
                anim.SetBool("IsCrapRightTime", true);
                crapTime = 0;
                moveSpeed = 0;
            }
            else
            {

                //将抓取动画关闭
                anim.SetBool("IsCrapRightTime", false);
                //回归原动画
                anim.SetFloat("CrapRight Time", 1.1f);
                //播放右移动画
                anim.SetTrigger("IsWalkRight");
                bullectEulerAngles.Clear();
                bullectEulerAngles.Add(new Vector3(0, 0, 0));
                bullectEulerAngles.Add(new Vector3(0, 0, 5));
                bullectEulerAngles.Add(new Vector3(0, 0, -5));
                crapTime += Time.fixedDeltaTime;
                moveSpeed = 1f;

            }
        }

    }
    //角色的跳跃
    private void Jump()
    {
        
        if (h < 0)
        {
            if (jumpTime >= 5f)
            {
                AudioManager._instance.PlaySound(crapClip);
                anim.SetBool("IsJumpLeftTime", true);
                transform.localPosition = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0);
                jumpTime = 0;
            }
            else
            {
                anim.SetBool("IsJumpLeftTime", false);
                //回归原动画
                anim.SetFloat("JumpLeft Time", 1.3f);
                jumpTime += Time.fixedDeltaTime;

            }
        }
        if (h > 0)
        {
            if (jumpTime >= 5f)
            {
                AudioManager._instance.PlaySound(crapClip);
                anim.SetBool("IsJumpRightTime", true);
                transform.localPosition = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0);
                jumpTime = 0;
            }
            else
            {

                anim.SetBool("IsJumpRightTime", false);
                //回归原动画
                anim.SetFloat("JumpRight Time", 1.3f);
                jumpTime += Time.fixedDeltaTime;

            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Enemy" && collision.transform.tag != "MapEnemy" &&
            collision.transform.tag != "CityWall" && collision.transform.tag != "AirBarriar")
        {
            if (collision.transform.tag == "Hero" || collision.transform.tag == "Hero2") { collision.transform.SendMessage("HeroDie"); }
            else { Destroy(collision.gameObject); }
        }
    }
}
