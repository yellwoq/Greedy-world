using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_click: MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public RuntimeAnimatorController runtimeAnim;
    private void Start()
    {
        animator =GetComponent<Animator>();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.runtimeAnimatorController = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.runtimeAnimatorController = runtimeAnim;
    }
}
