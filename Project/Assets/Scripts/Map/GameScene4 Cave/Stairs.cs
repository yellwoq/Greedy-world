using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public GameObject wall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        wall = GameObject.FindGameObjectWithTag("CityWall");
        if (collision.tag=="Hero"|| collision.tag == "Hero2"||collision.tag=="Enemy")
        {
            wall.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }
}
