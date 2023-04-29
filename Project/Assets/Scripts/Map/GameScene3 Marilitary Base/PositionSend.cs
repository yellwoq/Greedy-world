using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSend : MonoBehaviour
{
    public GameObject[] receiveFloor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        receiveFloor = GameObject.FindGameObjectsWithTag("Receive Floor");
        MapCreation map = GameObject.Find("MapCreation").GetComponent<MapCreation>();
        if(collision.tag=="Hero"|| collision.tag == "Hero2"|| collision.tag == "Enemy")
        {
            collision.transform.position = receiveFloor[Random.Range(0, receiveFloor.Length)].transform.position;
        }
    }
}
