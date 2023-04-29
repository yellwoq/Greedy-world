using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    //属性值
    //玩家1，玩家2的生命值
    public int[] lifeValue = new int[2] {6,6};
    //玩家的分数
    public int playerScore = 0;
    public int currentKillEnemys = 0;
    public int targetScore = 0;
    public int targetKillEnemys = 0;
    public bool isplayer1Dead;
    public bool isplayer2Dead;
    //玩家是否被打败
    public bool isDefeat;
    //当前所处的游戏场景索引
    public int sceneIndex;
    //引用
    //出生的特效
    public GameObject born;
    //玩家分数的显示
    public Text playerScoreText;
    //玩家生命值的显示
    public Text player1LifeValueText;
    public Text player2LifeValueText;
    //当前杀敌数的显示
    public Text currentKillEnemysText;
    //目标显示
    //目标分数
    public Text targetScoreValueText;
    //目标杀敌数
    public Text targetKillEnemysText;
    //产生敌人数量
    MapCreation mapCreation;
    private Vector3 state;
    public bool isOver = true;
    private int choice;
    //玩家选择的英雄
    public int player1Select, player2Select;
    public Image player1CurrentImage, player1TargetImage;
    public Sprite[] playersprite;
    //传送阵
    public GameObject sendgameObject;
    //老王
    public GameObject bossGameObject;
    //玩家2的信息显示
    public Image player2Image, player2TargetImage, player2LifeMassage;
    //设置
    public Button SetBotton;
    public SetGame set;
    //单例
    private static PlayerManager instance;
    public AudioClip clickClip;
    public static PlayerManager Instance
    {
        get => instance;
        set => instance = value;
    }

    private void Awake()
    {
        Instance = this;
        sceneIndex = gameObject.scene.buildIndex;
        mapCreation = GameObject.Find("MapCreation").GetComponent<MapCreation>();
        player1Select = PlayerPrefs.GetInt(Const.player1HeroSelect, 1);
        player2Select = PlayerPrefs.GetInt(Const.player2HeroSelect, 1);
        choice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        if (sceneIndex == 3)
        {
            //将玩家的信息储存起来
            PlayerPrefs.SetInt(Const.player1Life, lifeValue[0]);
            PlayerPrefs.SetInt(Const.player2Life, lifeValue[1]);
        }
        lifeValue[0] = PlayerPrefs.GetInt(Const.player1Life, 0);
        lifeValue[1] = PlayerPrefs.GetInt(Const.player2Life, 0);
        SetBotton.onClick.AddListener(Pause);
    }
    private void Start()
    {
        PlayerSelect();
    }

    void Update()
    {
        JudegeTarget();
        Judege();
    }
    //判断游戏
    private void Judege()
    {
        if (sceneIndex == 3) { playerScore = PlayerPrefs.GetInt(Const.playerScoreScene1, 0); }
        else if (sceneIndex == 4) { playerScore = PlayerPrefs.GetInt(Const.playerScoreScene2, 0); }
        else if (sceneIndex == 5) { playerScore = PlayerPrefs.GetInt(Const.playerScoreScene3, 0); }
        else if (sceneIndex == 6) { playerScore = PlayerPrefs.GetInt(Const.playerScoreScene4, 0); bossGameObject = GameObject.FindGameObjectWithTag("Boss"); }
        playerScoreText.text = playerScore.ToString();
        player1LifeValueText.text = lifeValue[0].ToString();
        player2LifeValueText.text = lifeValue[1].ToString();
        currentKillEnemysText.text = currentKillEnemys.ToString();
        targetScoreValueText.text = targetScore.ToString();
        targetKillEnemysText.text = targetKillEnemys.ToString();
        if (isDefeat)
        {
            return;
        }
        if (playerScore >= targetScore && currentKillEnemys >= targetKillEnemys)
        {
            if (isOver)
            {
                if (sceneIndex == 6)
                {
                    
                    if (bossGameObject != null)
                    {
                        isOver = false;
                        return;
                    }
                    else
                    {
                        isOver = true;
                    }
                }
                //将玩家的信息储存起来
                PlayerPrefs.SetInt(Const.player1Life, lifeValue[0]);
                PlayerPrefs.SetInt(Const.player2Life, lifeValue[1]);
                state = new Vector3(Random.Range(-6.5f, 7f), Random.Range(-3f, 3.5f), 0);
                Instantiate(sendgameObject, state, Quaternion.identity);
                isOver = false;
            }

        }
        if (isplayer1Dead)
        {
            OneRecover();
        }
        if (choice == 2)
        {
            //将玩家2的相关信息显示出来
            player2Image.gameObject.SetActive(true);
            player2LifeMassage.gameObject.SetActive(true);
            player2TargetImage.gameObject.SetActive(true);
            if (isplayer2Dead)
            {
                TwoRecover();
            }
            if (isplayer1Dead && isplayer2Dead)
            {
                //游戏失败，返回主界面
                isDefeat = true;
                Invoke("LoseShow", 2);
                return;
            }
        }
    }
    //判断目标
    private void JudegeTarget()
    {
        //单人模式
        if (sceneIndex == 3)
        {
            PlayerPrefs.SetInt(Const.playerScoreScene1, playerScore);
            if (choice == 1)
            {
                targetScore = 20;
                targetKillEnemys = 10;
            }
            else
            {
                targetScore = 30;
                targetKillEnemys = 20;
            }
        }
        else if (sceneIndex == 4)
        {
            PlayerPrefs.SetInt(Const.playerScoreScene2, playerScore);
            if (choice == 1)
            {
                targetScore = 30;
                targetKillEnemys = 20;
            }
            else
            {
                targetScore = 40;
                targetKillEnemys = 30;
            }
        }
        else if (sceneIndex == 5)
        {
            PlayerPrefs.SetInt(Const.playerScoreScene3, playerScore);
            if (choice == 1)
            {
                targetScore = 70;
                targetKillEnemys = 30;
            }
            else
            {
                targetScore = 80;
                targetKillEnemys = 40;
            }
        }
        else if (sceneIndex == 6)
        {
            PlayerPrefs.SetInt(Const.playerScoreScene4, playerScore);
            if (choice == 1)
            {
                targetScore = 70;
                targetKillEnemys = 30;
            }
            else
            {
                targetScore = 80;
                targetKillEnemys = 40;
            }
        }
       
    }
    //玩家英雄选择
    public void PlayerSelect()
    {
        if (player1Select == 1) { player1CurrentImage.sprite = playersprite[0]; player1TargetImage.sprite = playersprite[0]; }
        else if (player1Select == 2) { player1CurrentImage.sprite = playersprite[1]; player1TargetImage.sprite = playersprite[1]; }
        else if (player1Select == 3) { player1CurrentImage.sprite = playersprite[2]; player1TargetImage.sprite = playersprite[2]; }
        else if (player1Select == 4) { player1CurrentImage.sprite = playersprite[3]; player1TargetImage.sprite = playersprite[3]; }
        if (player2Select == 1) { player2Image.sprite = playersprite[0]; player2TargetImage.sprite = playersprite[0]; }
        else if (player2Select == 2) { player2Image.sprite = playersprite[1]; player2TargetImage.sprite = playersprite[1]; }
        else if (player2Select == 3) { player2Image.sprite = playersprite[2]; player2TargetImage.sprite = playersprite[2]; }
        else if (player2Select == 4) { player2Image.sprite = playersprite[3]; player2TargetImage.sprite = playersprite[3]; }
    }
    //1号玩家
    private void OneRecover()
    {
        
        if (choice == 2)
        {
            if (lifeValue[0] <=0)
            {
                return;
            }
        }
        else
        {//如果玩家一死完
            if (lifeValue[0] <=0)
            {
                //游戏失败，跳转失败界面
                isDefeat = true;
                Invoke("LoseShow", 3);
                return;
            }
        }
        lifeValue[0]--;
        //在出生点产生玩家
        GameObject go;
        go = Instantiate(born, OneBorn(), Quaternion.identity);
        go.GetComponent<Born>().iscreatePlayer1 = true;
        isplayer1Dead = false;
    }
    //玩家1出生点
    public Vector3 OneBorn()
    {
        if (sceneIndex == 3) { return new Vector3(-7, 0, 0); }
        else if (sceneIndex == 4) { return new Vector3(-7, 3, 0); }
        else if (sceneIndex == 5) { return new Vector3(-7, 0.5f, 0); }
        else if (sceneIndex == 6) { return new Vector3(-7, -3.5f, 0); }
        else if (sceneIndex == 7) { return new Vector3(-7, -3.5f, 0); }
        else { return new Vector3(-7, 0.5f, 0); }


    }
    //2号玩家
    private void TwoRecover()
    {
        
        if (lifeValue[1] <= 0)
        {
            return;
        }
        lifeValue[1]--;
        GameObject go;
        go = Instantiate(born, TwoBorn(), Quaternion.identity);
        go.GetComponent<Born>().iscreatePlayer2 = true;
        isplayer2Dead = false;
    }
    //玩家2出生点
    public Vector3 TwoBorn()
    {
        if (sceneIndex == 3) { return new Vector3(-7, -0.5f, 0); }
        else if (sceneIndex == 4) { return new Vector3(-7, -2.8f, 0); }
        else if (sceneIndex == 5) { return new Vector3(-7, 0, 0); }
        else if (sceneIndex == 6) { return new Vector3(-6.5f, -3.5f, 0); }
        else { return new Vector3(-7, -2.8f, 0); }
    }

    private void LoseShow()
    {
        SceneManager.LoadSceneAsync(8);
    }

    public void Pause()
    {
        AudioManager._instance.PlaySound(clickClip);
        Time.timeScale = 0;
        set.Show();
    }
}
