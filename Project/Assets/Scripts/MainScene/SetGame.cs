using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetGame : View
{
    public Sprite pauseSprite;
    public Image countinueButton;
    public int choice;
    public AudioClip clickClip;
    private void Start()
    {
        choice = PlayerPrefs.GetInt(Const.playerChoice, 1);
    }
    public void OnClickCountinue()
    {
        AudioManager._instance.PlaySound(clickClip);
        Time.timeScale = 1;
        countinueButton.sprite = pauseSprite;
        gameObject.SetActive(false);
    }
    public void OnClickExit()
    {
        Time.timeScale = 1;
        AudioManager._instance.PlaySound(clickClip);
        PlayerPrefs.DeleteKey(Const.playerScoreScene1);
        PlayerPrefs.DeleteKey(Const.playerScoreScene2);
        PlayerPrefs.DeleteKey(Const.playerScoreScene3);
        PlayerPrefs.DeleteKey(Const.playerScoreScene4);
        SceneManager.LoadScene(2); 
    }

}
