using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{ 
    //重写Show(),Hide()方法
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
