using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusSpecial : MonoBehaviour
{
    public Sprite[] bonusSprites;
    private SpriteRenderer sr;
    Animator anim;
    public GameObject[] game;
    private Hero heroBack;
    private void Awake()
    { 
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Destroy(gameObject, 10f);
       
    }
    private void Update()
    {
        //寻找标签为Enemy的所有物体，添加到game中
        game = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim = collision.GetComponent<Animator>();
        heroBack = collision.GetComponent<Hero>();
        if (collision.tag == "Hero"||collision.tag=="Hero2")
        {
            int num = Random.Range(0, 16);
            if (num <= 5)
            {
                sr.sprite = bonusSprites[0];//时间暂停
                try
                {
                    foreach (var item in game)
                    {
                        item.GetComponent<EnemysController>().stopTime = 0;
                    }
                    Destroy(gameObject, 0.167f);
                }
                catch
                {
                    Debug.Log("敌人暂停成功");
                }
            }
            else if (num > 5 && num <= 9)
            {
                sr.sprite = bonusSprites[1];//变身机器人
            heroBack.roBotTimeVal = 0;
            //变大
            collision.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1.5f, 1.5f, 1), 1);
            }
            else if (num > 9 && num <= 12)
            {
                sr.sprite = bonusSprites[2];//爆炸
                try
                {
                    foreach (var item in game)
                    {
                        item.GetComponent<EnemysController>().SendMessage("EnemyDie");
                    }
                }
                catch
                {
                    Debug.Log("敌人消灭完毕");
                }
            }
            else if (num == 14 || num == 15)
            {
                sr.sprite = bonusSprites[3];//生命
                int lifeNum = Random.Range(0, 5);
                if (collision.tag == "Hero")
                {
                    switch (lifeNum) { case 0: PlayerManager.Instance.lifeValue[0]++;break;
                        case 1: PlayerManager.Instance.lifeValue[0]+=2; break;
                        case 2: PlayerManager.Instance.lifeValue[0]+=3; break;
                        case 3: PlayerManager.Instance.lifeValue[0]+=4; break;
                        case 4: PlayerManager.Instance.lifeValue[0]+=5; break;
                    }
                }
                else
                {
                    switch (lifeNum)
                    {
                        case 0: PlayerManager.Instance.lifeValue[1]++; break;
                        case 1: PlayerManager.Instance.lifeValue[1] += 2; break;
                        case 2: PlayerManager.Instance.lifeValue[1] += 3; break;
                        case 3: PlayerManager.Instance.lifeValue[1] += 4; break;
                        case 4: PlayerManager.Instance.lifeValue[1] += 5; break;
                    }
                }
            }
            Destroy(gameObject, 0.3f);
        }
    }
}
