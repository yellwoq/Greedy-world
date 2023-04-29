using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownStairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] downStairs = GameObject.FindGameObjectsWithTag("Stairs");
        if (collision.tag == "Hero" || collision.tag == "Hero2"||collision.tag == "Enemy")
        {
            collision.transform.position = downStairs[Random.Range(0, downStairs.Length)].transform.position;
        }
    }
}
