using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public GameObject[] enemyPrefabList;
    public GameObject boss;
    //当前游戏所在的场景序号
    private int SceneNum;
    public bool iscreatePlayer1;
    public bool iscreatePlayer2;
    public bool iscreateBoss;
    private int num;
    public AudioClip bornClip;
    void Start()
    {
        Invoke("BornHero", 1f);
        Destroy(gameObject, 1f);
    }

    public void BornHero()
    {
        //获取当前游戏所在的场景序号
        SceneNum = gameObject.scene.buildIndex - 2;
        //如果产生的是玩家
        if (iscreatePlayer1)
        {
            AudioManager._instance.PlaySound(bornClip);
            Instantiate(playerPrefab[0], transform.position, Quaternion.identity);
        }
        else if (iscreatePlayer2)
        {
            AudioManager._instance.PlaySound(bornClip);
            Instantiate(playerPrefab[1], transform.position, Quaternion.identity);
        }
        else if (iscreateBoss)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
        }
        else
        {
            if (SceneNum == 1)
            {
                num = Random.Range(0, 3);

            }
            else if (SceneNum == 2)
            {
                num = Random.Range(1, 4);
            }
            else if (SceneNum == 3)
            {
                num = Random.Range(2, 6);
            }
            else if (SceneNum == 4)
            {
                num = Random.Range(0, 7);
            }
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);

        }
    }
}
