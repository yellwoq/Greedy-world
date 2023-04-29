using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    //音效
    public AudioClip bgClip, clickClip;
    private void Start()
    {
        AudioManager._instance.PlaySound(bgClip);
    }
    public void OnClickToCountinue() { AudioManager._instance.PlaySound(clickClip); SceneManager.LoadScene(3); }
}
