using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //攻击木桶次数
    public int attackedCount = 0;
    private void MissTime()
    {
        if (attackedCount >= 1)
        {
            Destroy(gameObject);
            attackedCount = 0;
        }
            attackedCount++;
        
    }

}
