using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalTree : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Hero" || collision.transform.tag == "Hero2")
        {
            collision.transform.SendMessage("HeroDie");
        }
    }
}
