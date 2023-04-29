using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : View
{
    public SetPanel setPanel;
    public RegisterPanel registerPanel;
    public DataBaseManager databasePanel;
    public AudioClip clickclip,bgclip;

    private void Awake()
    {
        AudioManager._instance.PlayMusic(bgclip);//循环播放背景音乐
    }
    //单人模式
    public void OnOnePerSonModelClick()
    {
        //播放点击音乐
        AudioManager._instance.PlaySound(clickclip);//只播放一次
        SceneManager.LoadScene(1);
    }
    //双人模式
    public void OnTwoPerSonModelClick()
    {
        AudioManager._instance.PlaySound(clickclip);
        PlayerPrefs.SetInt(Const.playerChoice, 2);
        SceneManager.LoadScene(9);
    }

    public void OnClickDataBaseMangagerPanel()
    {
        AudioManager._instance.PlaySound(clickclip);
        //数据库设置界面显示出来
        databasePanel.Show();
    }

    public void OnClickSetPanelShow()
    {
        AudioManager._instance.PlaySound(clickclip);
        //将设置界面显示出来
        setPanel.Show();
    }
    public void OnClickRegisterShow()
    {
        AudioManager._instance.PlaySound(clickclip);
        //将注册账号界面显示出来
        registerPanel.Show();
    }
}
