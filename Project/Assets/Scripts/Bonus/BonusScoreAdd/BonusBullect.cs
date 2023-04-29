using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBullect : MonoBehaviour
{
    public Sprite[] bonusSprites;
    private SpriteRenderer sr;
    private Hero hero;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //获得碰撞物体上相应的组件
        hero = collision.GetComponent<Hero>();
        if (collision.tag == "Hero"|| collision.tag == "Hero2")
        {
            //随机产生奖励
            int num = Random.Range(0, 10);
            Debug.Log(num);

            if (num <= 3)
            {
                sr.sprite = bonusSprites[0];
                //切换子弹
                hero.bullectPrefab = hero.bullectStrong[0];
                //子弹冷却时间缩短
                hero.attackBetweenSpeed = 0.8f;
            }
            else if (num > 3 && num <= 7)
            {
                sr.sprite = bonusSprites[1];
                hero.bullectPrefab = hero.bullectStrong[1];
                hero.attackBetweenSpeed = 2f;
            }
            else if (num >7 && num <= 9)
            {
                sr.sprite = bonusSprites[2];
                hero.bullectPrefab = hero.bullectStrong[2];
                hero.attackBetweenSpeed = 1.5f;
            }
            Destroy(gameObject, 0.3f);
        }
    }
}
