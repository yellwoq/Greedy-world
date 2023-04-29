using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysCreate : MonoBehaviour
{
    public GameObject[] enemys;
    void Awake()
    {
        InvokeRepeating("CreateItem", 5, 15);
    }
    public void CreateItem()
    {
        GameObject itemGo=Instantiate(enemys[Random.Range(0,6)],transform.position, Quaternion.identity);
        itemGo.transform.SetParent(gameObject.transform);
    }
}
