using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 heroPosition1 = GameObject.Find("PlayerController").GetComponent<PlayerManager>().OneBorn();
        Vector3 heroPosition2 = GameObject.Find("PlayerController").GetComponent<PlayerManager>().TwoBorn();
        if (collision.tag == "Hero")
        {
            collision.transform.position = heroPosition1;
        }
        else if(collision.tag == "Hero2")
        {
            collision.transform.position = heroPosition2;
        }
    }
}
