using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoPersonLogin : MonoBehaviour
{
    //玩家1账号
    public InputField userNameInput1;
    //玩家1密码
    public InputField passwordInput1;
    //玩家2账号
    public InputField userNameInput2;
    //玩家2密码
    public InputField passwordInput2;
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
    //登录按钮
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
        //自连接,再把查询结果当成一个表，在这个表中验证信息
        DataSet ds = mysql.Select("(select user1.account 账号1,user1.password 密码1,user2.account 账号2,user2.password 密码2 from user as" +
        " user1 inner join user as user2 on user1.account!=user2.account) as userCombine", new string[] { "账号1", "账号2" }, new string[] { "账号1",
            "密码1","账号2","密码2" }, new string[] { "=", "=", "=", "=" }, new string[] { userNameInput1.text, passwordInput1.text,
                userNameInput2.text,passwordInput2.text });
        if (ds != null)
        {
            DataTable table = ds.Tables[0];
            if (table.Rows.Count > 0)
            {
                loginMsg = "登录成功！";
                PlayerPrefs.SetString(Const.user1Name, userNameInput1.text.ToString());
                PlayerPrefs.SetString(Const.user2Name, userNameInput2.text.ToString());
                SceneManager.LoadScene(2);
                PlayerPrefs.SetInt(Const.playerChoice, 2);
                tip.color = Color.green;
                Debug.Log("用户1账号：" + table.Rows[0][0]);
                Debug.Log("用户2账号：" + table.Rows[0][1]);
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
