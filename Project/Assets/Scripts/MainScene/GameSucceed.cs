using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSucceed : MonoBehaviour
{
    //IP地址
    private string host;
    //端口号
    private string port;
    //用户名
    private string userName;
    //密码
    private string password;
    //数据库名称
    private string databaseName;
    //音效
    public AudioClip succeedClip, clickClip;

    private void Start()
    {
        host = PlayerPrefs.GetString(Const.host);
        port = PlayerPrefs.GetString(Const.port);
        userName = PlayerPrefs.GetString(Const.userName);
        password = PlayerPrefs.GetString(Const.password);
        databaseName = PlayerPrefs.GetString(Const.databaseName);
        AudioManager._instance.PlaySound(succeedClip);
        UpdateAsset.Instance.playerChoice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        UpdateAsset.Instance.ChangeData(host, port, userName, password, databaseName);
    }
    public void OnYesPlayer() { AudioManager._instance.PlaySound(clickClip); Invoke("LoadVedioPlayerScene", 1.5f);
    }
    public void OnNoPlayer() {
        AudioManager._instance.PlaySound(clickClip);
        PlayerPrefs.DeleteKey(Const.playerScoreScene1);
        PlayerPrefs.DeleteKey(Const.playerScoreScene2);
        PlayerPrefs.DeleteKey(Const.playerScoreScene3);
        PlayerPrefs.DeleteKey(Const.playerScoreScene4);
        SceneManager.LoadScene(0); }
    public void LoadVedioPlayerScene() { SceneManager.LoadScene(11); }
}
