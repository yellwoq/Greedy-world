using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Data;

public class Rank : MonoBehaviour
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
    //封装好的数据库类
    public MySqlAsset mysql;
    private int playerChoice;
    List<Dictionary<string, object>> rankList;
    public Text[] onePerSonName, onePerSonScore;
    public Text[] twoPerSonName1, twoPerSonName2, twoPerSonScore;
    //存放读取到的数据信息
    public List<object> accountMsg, scoreMsg;
    public GameObject oneRankPanel, twoRankPanel;
    public AudioClip bgClip, clickClip;
    int i = 0;
    int amount;
    public void OnBeginGame()
    {
        AudioManager._instance.PlaySound(clickClip);
        SceneManager.LoadScene(12);
    }
    public void OnExitLogin()
    {
        AudioManager._instance.PlaySound(clickClip);
        PlayerPrefs.DeleteKey(Const.playerScoreScene1);
        PlayerPrefs.DeleteKey(Const.playerScoreScene2);
        PlayerPrefs.DeleteKey(Const.playerScoreScene3);
        PlayerPrefs.DeleteKey(Const.playerScoreScene4);
        if (playerChoice == 1)
        {
            SceneManager.LoadScene(gameObject.scene.buildIndex - 1);
        }
        else
        { SceneManager.LoadScene(9); }
    }
    public void OnRefreshLogin() { AudioManager._instance.PlaySound(clickClip); i = 0; Refresh(); }
    void Start()
    {
        AudioManager._instance.PlayMusic(bgClip);
        host = PlayerPrefs.GetString(Const.host);
        port = PlayerPrefs.GetString(Const.port);
        userName = PlayerPrefs.GetString(Const.userName);
        password = PlayerPrefs.GetString(Const.password);
        databaseName = PlayerPrefs.GetString(Const.databaseName);
        mysql = new MySqlAsset(host, port, userName, password, databaseName);
        playerChoice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        ChangeAction();
    }
    /// <summary>
    /// 刷新数据
    /// </summary>
    public void Refresh()
    {
        mysql.OpenSql();
        string userName = PlayerPrefs.GetString(Const.user1Name, "");
        Debug.Log(userName);
        string user2Name = PlayerPrefs.GetString(Const.user2Name, "");
        Debug.Log(user2Name);
        int? userScore = PlayerPrefs.GetInt(Const.playerScoreScene1, 0) + PlayerPrefs.GetInt(Const.playerScoreScene2, 0) +
        PlayerPrefs.GetInt(Const.playerScoreScene3, 0) + PlayerPrefs.GetInt(Const.playerScoreScene4, 0);
        rankList = ReloadData();
        foreach (var item in rankList)
        {
            if (playerChoice == 1)
            {
                onePerSonName[i].text = item["account"].ToString();
                onePerSonScore[i].text = item["scores"].ToString();
                onePerSonScore[i].transform.parent.parent.gameObject.SetActive(true);
            }
            else
            {
                twoPerSonName1[i].text = item["account1"].ToString();
                twoPerSonName2[i].text = item["account2"].ToString();
                twoPerSonScore[i].text = item["score"].ToString();
                twoPerSonScore[i].transform.parent.parent.gameObject.SetActive(true);
            }

            if (i < rankList.Count) { i++; } else { i = 0; }
        }
        Debug.Log(userScore.ToString());
        mysql.CloseSql();
    }
    /// <summary>
    /// 改变信息
    /// </summary>
    public void ChangeAction()
    {

        mysql.OpenSql();
        string userName = PlayerPrefs.GetString(Const.user1Name, "");
        Debug.Log(userName);
        string user2Name = PlayerPrefs.GetString(Const.user2Name, "");
        Debug.Log(user2Name);
        int? userScore = PlayerPrefs.GetInt(Const.playerScoreScene1, 0) + PlayerPrefs.GetInt(Const.playerScoreScene2, 0) +
        PlayerPrefs.GetInt(Const.playerScoreScene3, 0) + PlayerPrefs.GetInt(Const.playerScoreScene4, 0);
        Debug.Log(userScore.ToString());
        //将以上信息保存到数据库
        //带参数的Sql插入语句
        string sql;
        //单人模式
        if (playerChoice == 1)
        {
            oneRankPanel.SetActive(true);
            //给变量赋值
            MySqlParameter[] pms = new MySqlParameter[]
            {
                 new MySqlParameter ("@at",MySqlDbType.VarChar,45){Value=userName.Trim() },
                new  MySqlParameter ("@scores",MySqlDbType.VarChar,45){Value=userScore }
            };
            DataSet oneResult = mysql.Select("oneplayerrank", new string[] { "*" }, new string[] { "account" }, new string[] { "=" }, new string[] { userName });
            if (oneResult != null && userScore != null)
            {
                DataTable table = oneResult.Tables[0];
                if (table.Rows.Count > 0)
                {
                    rankList = ReloadData();
                    sql = "update oneplayerrank set scores=@scores where account=@at";
                    foreach (var item in rankList)
                    {
                        Debug.Log(item["scores"].ToString());
                        if (item["account"].ToString()==userName && (int)item["scores"]<=userScore)
                        {
                            mysql.ExecuteNonQuery(sql, pms);
                        }
                        onePerSonName[i].text = item["account"].ToString();
                        onePerSonScore[i].text = item["scores"].ToString();
                        onePerSonScore[i].transform.parent.parent.gameObject.SetActive(true);
                        if (i < rankList.Count) { i++; } else { i = 0; }
                    }
                }
                else
                {
                    sql = "insert into oneplayerrank(account,scores) values(@at,@scores)";
                    mysql.ExecuteNonQuery(sql, pms);
                }
            }
            else
            {
                Debug.Log("请开始游戏");
            } }
        //双人模式
        else
        {
            twoRankPanel.SetActive(true);
            //给变量赋值
            MySqlParameter[] pms = new MySqlParameter[]
            {
                 new MySqlParameter ("@account1",MySqlDbType.VarChar,45){Value=userName.Trim() },
                 new MySqlParameter ("@account2",MySqlDbType.VarChar,45){Value=user2Name.Trim() },
                new  MySqlParameter ("@score",MySqlDbType.Int32){Value=userScore }
            };
            DataSet twoResult = mysql.Select("twoplayerrank", new string[] { "*" }, new string[] { "account1", "account2" }, new string[] { "=", "=" }, new string[] { userName, user2Name });
            if (twoResult != null && userScore != null)
            {
                DataTable table = twoResult.Tables[0];
                if (table.Rows.Count > 0)
                {
                    rankList = ReloadData();
                    sql = "update twoplayerrank set score=@score where account1=@account1 and account2=@account2";
                    foreach (var item in rankList)
                    {
                        Debug.Log(item["score"].ToString());
                        if (item["account1"].ToString() == userName&& item["account2"].ToString() == user2Name&& (int)item["score"]<=userScore)
                        {
                            mysql.ExecuteNonQuery(sql, pms);
                        }
                        twoPerSonName1[i].text = item["account1"].ToString();
                        twoPerSonName2[i].text = item["account2"].ToString();
                        twoPerSonScore[i].text = item["score"].ToString();
                        twoPerSonScore[i].transform.parent.parent.gameObject.SetActive(true);
                        if (i < rankList.Count) { i++; } else { i = 0; }
                    }
                }
                else
                {
                    sql = "insert into twoplayerrank(account1,account2,score) values(@account1,@account2,@score)";
                    mysql.ExecuteNonQuery(sql, pms);
                }
            }
            else { Debug.Log("请开始游戏"); }
        }
        mysql.CloseSql();
    }
    public List<Dictionary<string, object>> Read(string pms)
    {
        List<Dictionary<string, object>> result;
        result = mysql.ExecuteQuery("select * from oneplayerrank where account=pms");
        return result;
    }
    /// <summary>
    /// 刷新界面
    /// </summary>
    public List<Dictionary<string, object>> ReloadData()
    {
        List<Dictionary<string, object>> result;
        if (playerChoice == 1)
        { result = mysql.ExecuteQuery("select * from oneplayerrank order by scores desc limit 5"); }
        else { result = mysql.ExecuteQuery("select * from twoplayerrank order by score desc limit 5"); }
        return result;
    }

}