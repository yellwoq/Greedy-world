using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectHero : MonoBehaviour
{
    public int choice;
    public GameObject player2Select,star;
    public Button beginButton;
    //音效
    public AudioClip bgClip, clickClip;
    #region 玩家的选择
    public Toggle player1SelectHero1;
    public Toggle player1SelectHero2;
    public Toggle player1SelectHero3;
    public Toggle player1SelectHero4;
    public Toggle player2SelectHero1;
    public Toggle player2SelectHero2;
    public Toggle player2SelectHero3;
    public Toggle player2SelectHero4;
    #endregion
    private void Awake()
    {
        choice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        AudioManager._instance.PlayMusic(bgClip);
        beginButton.onClick.AddListener(OnClickToCountinue);
    }
    void Judge(bool isOn)
    {
        if(isOn)
        {
            if (choice == 2)
            {
                player2Select.SetActive(true);
            }
            beginButton.gameObject.SetActive(true); return;
        }
        
        
    }
    public void Player1SelectHero1(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player1HeroSelect, 1); Judge(isOn); } }
    public void Player1SelectHero2(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player1HeroSelect, 2); Judge(isOn); } }
    public void Player1SelectHero3(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player1HeroSelect, 3); Judge(isOn); } }
    public void Player1SelectHero4(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player1HeroSelect, 4); Judge(isOn); } }
    public void Player2SelectHero1(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player2HeroSelect, 1); Judge(isOn); } }
    public void Player2SelectHero2(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player2HeroSelect, 2); Judge(isOn); } }
    public void Player2SelectHero3(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player2HeroSelect, 3); Judge(isOn); } }
    public void Player2SelectHero4(bool isOn) { if (isOn) { star.SetActive(true); AudioManager._instance.PlaySound(clickClip); PlayerPrefs.SetInt(Const.player2HeroSelect, 4); Judge(isOn); } }
    public void OnClickToCountinue() { AudioManager._instance.PlaySound(clickClip); SceneManager.LoadScene(13); }
    public void StorySceneLoad() { SceneManager.LoadScene(13); }

}
