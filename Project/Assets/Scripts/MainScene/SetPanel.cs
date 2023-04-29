using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : View
{
    public Slider slider_sound;
    public Slider slider_music;
    public AudioClip clickClip;
    //音效
    public void OnSoundValueChange(float f)
    {
        //修改音效的大小
        AudioManager._instance.OnSoundVolumeChange(f);
        //保存当前的修改
        PlayerPrefs.SetFloat(Const.Sound, f);
        Debug.Log("音效大小是:"+f.ToString());
    }
    //音乐
    public void OnMusicValueChange(float f)
    {
        //修改音乐的大小
        AudioManager._instance.OnMusicVolumeChange(f);
        //保存当前的修改
        PlayerPrefs.SetFloat(Const.Music, f);
        Debug.Log("音乐大小是:"+f.ToString());
    }
    //重写Show()方法
    public override void Show()
    {
        base.Show();
        //对界面进行初始化
        slider_music.value = PlayerPrefs.GetFloat(Const.Music, 0);
        slider_sound.value = PlayerPrefs.GetFloat(Const.Sound, 0);
    }
    public void OnClickHide()
    {
        AudioManager._instance.PlaySound(clickClip);
        this.Hide();
    }

    public void OnClickExitGame()
    {
        AudioManager._instance.PlaySound(clickClip);
        //退出游戏
        Application.Quit();
    }
}
