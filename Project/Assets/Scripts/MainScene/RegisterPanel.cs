using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : View
{
    public InputField userNameInput;

    public InputField passwordInput;
    //提示用户注册信息
    private Text registerMessage;
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
    //注册按钮
    public Button registerButton;
    //表名
    public string dataTable;
    public Text tip;
    public AudioClip clickClip, succceedClip;
    public void OnClickHide()
    {
        AudioManager._instance.PlaySound(clickClip);
        tip.text = "";
        this.Hide();
    }
    private void Start()
    {
        //将注册点击事件绑定到按钮registerButton上
        registerButton.onClick.AddListener(OnClickedRegisterButton);

    }

    public override void Show()
    {
        base.Show();
        host = PlayerPrefs.GetString(Const.host);
        port = PlayerPrefs.GetString(Const.port);
        userName = PlayerPrefs.GetString(Const.userName);
        password = PlayerPrefs.GetString(Const.password);
        databaseName = PlayerPrefs.GetString(Const.databaseName);
        //将mysql实例化
        mysql = new MySqlAsset(host, port, userName, password, databaseName);
    }

    private void OnClickedRegisterButton()
    {
        AudioManager._instance.PlaySound(clickClip);
        DataSet oneResult = mysql.Select("user", new string[] { "*" }, new string[] { "account" }, new string[] { "=" }, new string[] { userNameInput.text});
        if (oneResult != null)
        {
            DataTable table = oneResult.Tables[0];
            if (table.Rows.Count > 0)
            {
                tip.text = "注册失败！";
                tip.color = Color.red;
                Debug.Log("注册失败");
            }
            else
            {
                if (userNameInput.text == ""|| passwordInput.text == "")
                {
                    tip.text = "用户名或密码不能为空！";
                    tip.color = Color.red;
                    Debug.Log("注册失败");
                    return;
                }
                //带参数的Sql插入语句
                string sql = "insert into user(account,password) values(@at,@pwd)";
                //给变量赋值
                MySqlParameter[] pms = new MySqlParameter[] {
            new MySqlParameter ("@at",MySqlDbType.VarChar,45){Value=userNameInput.text.Trim() },
            new  MySqlParameter ("@pwd",MySqlDbType.VarChar,45){Value=passwordInput.text.Trim() } };
                mysql.ExecuteNonQuery(sql, pms);
                AudioManager._instance.PlaySound(succceedClip);
                tip.text = "注册成功！";
                tip.color = Color.green;
                Debug.Log("注册成功");
            }
        }
    else
        {
            Debug.Log("数据为空"); 
        }
        userNameInput.text = null;
        passwordInput.text = null;
    }
}
