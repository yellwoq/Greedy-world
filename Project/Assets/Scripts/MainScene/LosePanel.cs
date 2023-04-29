using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
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
    public AudioClip clickClip, loseClip;
    
    private void Start()
    {
        host = PlayerPrefs.GetString(Const.host);
        port = PlayerPrefs.GetString(Const.port);
        userName = PlayerPrefs.GetString(Const.userName);
        password = PlayerPrefs.GetString(Const.password);
        databaseName = PlayerPrefs.GetString(Const.databaseName);
        AudioManager._instance.PlaySound(loseClip);
    }
    public void OnReback()
    {
        AudioManager._instance.PlaySound(clickClip);
        SceneManager.LoadSceneAsync(2);
    }
    public void ReturnToMainPanel()
    {
        AudioManager._instance.PlaySound(clickClip);
        SceneManager.LoadSceneAsync(0);
        UpdateAsset.Instance.playerChoice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        UpdateAsset.Instance.ChangeData(host, port, userName, password, databaseName);
        PlayerPrefs.DeleteKey(Const.playerScoreScene1);
        PlayerPrefs.DeleteKey(Const.playerScoreScene2);
        PlayerPrefs.DeleteKey(Const.playerScoreScene3);
        PlayerPrefs.DeleteKey(Const.playerScoreScene4);
        
    }
}
