using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Animator anim;//动画控制器
    public RuntimeAnimatorController[] anims;
    public bool isPlayer1;
    private float moveSpeed = 1.2f;//移动速度
    private Vector3 bullectEulerAngles;//子弹朝向
    private float timeValue;//冷却时间
    public GameObject bullectPrefab;//子弹
    public float attackBetweenSpeed = 2f;
    //爆炸特效预制体
    public GameObject explosionPrefab;
    //强化子弹
    public GameObject[] bullectStrong;
    public float stopTime = 0;
    //是否处于机器人状态
    public bool isRobot = false;
    public float roBotTimeVal = 0;
    //玩家选择的英雄代号
    public int player1Choice, player2Choice;
    //玩家的图片
    public SpriteRenderer heroSprite;
    public Sprite[] playerSprite;
    //玩家的音效
    public AudioClip attackClip, moveClip;
    public AudioClip[] dieClip;
    //上一关的子弹
    private int bullectStore;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        heroSprite = GetComponent<SpriteRenderer>();
        if (transform.gameObject.scene.buildIndex == 3)
        {
            PlayerPrefs.SetInt(Const.player1Bullect, 0);
            PlayerPrefs.SetInt(Const.player2Bullect, 0);
        }
        player1Choice = PlayerPrefs.GetInt(Const.player1HeroSelect, 1);
        player2Choice = PlayerPrefs.GetInt(Const.player2HeroSelect, 1);
    }
    private void Start()
    {
        ChangeHero();
        //子弹转换
        switch (bullectStore)
        {
            case 0: attackBetweenSpeed = 2f; break;
            case 1: bullectPrefab = bullectStrong[0]; attackBetweenSpeed = 0.8f; break;
            case 2: bullectPrefab = bullectStrong[1]; attackBetweenSpeed = 2f; break;
            case 3: bullectPrefab = bullectStrong[2];attackBetweenSpeed = 1.5f;break;
        }
    }

    private void FixedUpdate()
    {

        roBotTimeVal += Time.fixedDeltaTime;
        Debug.Log(bullectStore);
        //处于机器人状态
        if (roBotTimeVal <= 7f)
        {
            isRobot = true;
            anim.SetTrigger("TurnRobot");
            StopCoroutine("Reback");
        }
        else
        {
            StartCoroutine("Reback");
        }
        if (timeValue > attackBetweenSpeed)
        {
            HeroAttack();
        }
        else
        {
            timeValue += Time.deltaTime;
        }
        HeroMove();
    }

    public void ChangeHero()
    {
        if (isPlayer1)
        {
            bullectStore = PlayerPrefs.GetInt(Const.player1Bullect, 0);
            if (player1Choice == 1) { heroSprite.sprite = playerSprite[0]; anim.runtimeAnimatorController = anims[0]; }
            else if (player1Choice == 2) { heroSprite.sprite = playerSprite[1]; anim.runtimeAnimatorController = anims[1]; }
            else if (player1Choice == 3) { heroSprite.sprite = playerSprite[2]; anim.runtimeAnimatorController = anims[2]; }
            else if (player1Choice == 4) { heroSprite.sprite = playerSprite[3]; anim.runtimeAnimatorController = anims[3]; }
        }
        else
        {
            bullectStore = PlayerPrefs.GetInt(Const.player2Bullect, 0);
            if (player2Choice == 1) { heroSprite.sprite = playerSprite[0]; anim.runtimeAnimatorController = anims[0]; }
            else if (player2Choice == 2) { heroSprite.sprite = playerSprite[1]; anim.runtimeAnimatorController = anims[1]; }
            else if (player2Choice == 3) { heroSprite.sprite = playerSprite[2]; anim.runtimeAnimatorController = anims[2]; }
            else if (player2Choice == 4) { heroSprite.sprite = playerSprite[3]; anim.runtimeAnimatorController = anims[3]; }
        }
    }

    IEnumerator Reback()
    {
        isRobot = false;
        anim.SetTrigger("TurnPeople");
        //变小
        transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1.5f, 1.5f, 1), 0);
        yield return 1;
    }
    //角色的攻击
    private void HeroAttack()
    {
        //子弹旋转的角度：当前角色的角度+子弹应该旋转的角度
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager._instance.PlaySound(attackClip);
                if (bullectPrefab == bullectStrong[2])
                {
                    Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, 10)));
                    Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, -10)));
                }
                Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
                timeValue = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                AudioManager._instance.PlaySound(attackClip);
                if (bullectPrefab ==bullectStrong[2])
                {
                    Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles + new Vector3(0, 0, 10)));
                    Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles +new Vector3(0, 0, -10)));
                }
                Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
                timeValue = 0;
            }
        }
    }
    //移动角色
    private void HeroMove()
    {

        float v, h;
        if (isPlayer1)
        {
            v = Input.GetAxisRaw("Player1_Vertical");
            h = Input.GetAxisRaw("Player1_Horizontal");
        }
        else
        {
            v = Input.GetAxisRaw("Player2_Vertical");
            h = Input.GetAxisRaw("Player2_Horizontal");
        }
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            { AudioManager._instance.PlaySound(moveClip); }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            { AudioManager._instance.PlaySound(moveClip); }
        }
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
    //玩家1的死亡
    private void HeroDie()
    {
        if (isRobot)
        {
            return;
        }
        //播放死亡音效
        if (!isPlayer1)
        {
            if (player2Choice == 1 || player2Choice == 4) { AudioManager._instance.PlaySound(dieClip[1]); }
            else if (player2Choice == 2 || player2Choice == 3)
            { AudioManager._instance.PlaySound(dieClip[0]); }
            PlayerPrefs.SetInt(Const.player2Bullect, 0);
            PlayerManager.Instance.isplayer2Dead = true;
        }
        else
        {
            if (player1Choice == 1 || player1Choice == 4) { AudioManager._instance.PlaySound(dieClip[1]); }
            else if (player1Choice == 2 || player1Choice == 3) { AudioManager._instance.PlaySound(dieClip[0]); }
            PlayerPrefs.SetInt(Const.player1Bullect, 0);
            PlayerManager.Instance.isplayer1Dead = true;
        }
        //播放爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (isRobot)
            {
                collision.transform.SendMessage("EnemyDie");
            }
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
