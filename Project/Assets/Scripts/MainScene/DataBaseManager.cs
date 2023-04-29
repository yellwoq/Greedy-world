using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : View
{
    public AudioClip clickClip;
    public InputField host;
    public InputField port;
    public InputField userName;
    public InputField password;
    public InputField databaseName;
    //封装好的数据库类
     MySqlAsset mysql;
    public Text tips;
    public Text message;

    //重写Show()方法
    public override void Show()
    {
        base.Show();
        //对界面进行初始化
        host.text = PlayerPrefs.GetString(Const.host, "localhost");
        port.text = PlayerPrefs.GetString(Const.port, "3306");
        userName.text = PlayerPrefs.GetString(Const.userName, "root");
        password.text = PlayerPrefs.GetString(Const.password, "root");
        databaseName.text = PlayerPrefs.GetString(Const.databaseName, "usermanager");
    }

    public void OnClickCheck()
    {
        AudioManager._instance.PlaySound(clickClip);
        PlayerPrefs.SetString(Const.host, host.text.Trim());
        PlayerPrefs.SetString(Const.port, port.text.Trim());
        PlayerPrefs.SetString(Const.userName, userName.text.Trim());
        PlayerPrefs.SetString(Const.password, password.text.Trim());
        PlayerPrefs.SetString(Const.databaseName, databaseName.text.Trim());
        //将mysql实例化
        mysql =new MySqlAsset(host.text.Trim(), port.text.Trim(), userName.text.Trim(), password.text.Trim(), databaseName.text.Trim());
        message.text = "设置成功";
        tips.text = MySqlAsset.Instance.conMessage;
    }
    public void OnClickClose()
    {
        AudioManager._instance.PlaySound(clickClip);
        message.text = "";
        tips.text = "";
        mysql.CloseSql();
        this.Hide();
    }
}
