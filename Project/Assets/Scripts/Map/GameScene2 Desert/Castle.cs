using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float shotTime;
    private void Update()
    {
        shotTime += Time.deltaTime;
        if (shotTime >= 0.1f)
        {
            gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
        }
    }
}
