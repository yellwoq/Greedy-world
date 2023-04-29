using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusOut : MonoBehaviour
{
    public Sprite[] bonusSprites;
    private SpriteRenderer sr;
    private void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero"||collision.tag == "Hero2")
        {
            int num = Random.Range(0, 16);
            if (num<=5)
            {
                sr.sprite = bonusSprites[0];
                PlayerManager.Instance.playerScore +=2;
               
            }
            else if (num > 5&& num <= 10)
            {
                sr.sprite = bonusSprites[1];
                PlayerManager.Instance.playerScore +=4;

            }
            else if (num > 10 && num <= 13)
            {
                sr.sprite = bonusSprites[2];
                PlayerManager.Instance.playerScore += 6;
            }
            else if (num == 14)
            {
                sr.sprite = bonusSprites[3];
                PlayerManager.Instance.playerScore += 8;
            }
            else if (num == 15)
            {
                sr.sprite = bonusSprites[4];
                PlayerManager.Instance.playerScore += 10;
            }
            Destroy(gameObject, 0.2f);
        }
    }
}
