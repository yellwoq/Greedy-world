using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] open = GameObject.FindGameObjectsWithTag("Open");
        if (collision.tag == "Hero" || collision.tag == "Hero2" || collision.tag == "Enemy")
        {
            collision.transform.position = open[Random.Range(0, open.Length)].transform.position;
        }
    }
}
