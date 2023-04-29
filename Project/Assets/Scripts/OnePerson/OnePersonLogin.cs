using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnePersonLogin : MonoBehaviour
{
    public InputField userNameInput;

    public InputField passwordInput;
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
    //封装好的数据库类
    MySqlAsset mysql;
    public Button login;
    public Text tip;
    //音效
    public AudioClip bgClip, clickClip;
    private void Start()
    {
        host = PlayerPrefs.GetString(Const.host);
        port = PlayerPrefs.GetString(Const.port);
        userName = PlayerPrefs.GetString(Const.userName);
        password = PlayerPrefs.GetString(Const.password);
        databaseName = PlayerPrefs.GetString(Const.databaseName);
        AudioManager._instance.PlayMusic(bgClip);
        login.onClick.AddListener(OnClickedLoginButton);
        mysql = new MySqlAsset(host, port, userName, password, databaseName);
    }
    //主界面
    public void OnMainSceneReBackClick()
    {
        AudioManager._instance.PlaySound(clickClip);
        SceneManager.LoadScene(0);
    }
    private void OnClickedLoginButton()
    {
        AudioManager._instance.PlaySound(clickClip);
        mysql.OpenSql();
        string loginMsg = "";
        DataSet ds = mysql.Select("user", new string[] { "id" }, new string[] {  "account",
            "password" }, new string[] { "=", "=" }, new string[] { userNameInput.text, passwordInput.text });
        if (ds != null)
        {
            DataTable table = ds.Tables[0];
            if (table.Rows.Count > 0)
            {
                loginMsg = "登录成功!";
                Invoke("Loaded", 0.1f);
                PlayerPrefs.SetString(Const.user1Name, userNameInput.text.ToString());
                PlayerPrefs.SetInt(Const.playerChoice, 1);
                SceneManager.LoadScene(2);
                tip.color = Color.green;
                Debug.Log("用户权限等级：" + table.Rows[0][0]);
            }
            else
            {
                loginMsg = "用户名或密码错误！";
                tip.color = Color.red;
            }
            tip.text = loginMsg;
        }
        mysql.CloseSql();
    }
}
