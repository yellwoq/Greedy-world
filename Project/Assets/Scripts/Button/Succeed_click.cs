using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Succeed_click : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float playTime = 0;
    private int clickTime = 0;
    private Animator animator;
    private AudioSource audiosource;
    public AudioClip audioClip;
    public RuntimeAnimatorController runtimeAnim;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = audioClip;
    }
    private void Update()
    {
        playTime += Time.deltaTime;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (playTime >=3f)
        {
            animator.runtimeAnimatorController = null;
            playTime = 0;
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.runtimeAnimatorController = runtimeAnim;
        clickTime++;
        audiosource.Play();
    }
}
