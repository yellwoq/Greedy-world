using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpdateAsset
{
    public static readonly UpdateAsset Instance = new UpdateAsset();
    public int playerChoice;
    //封装好的数据库类
    public MySqlAsset mysql;
    List<Dictionary<string, object>> rankList;
    //更改信息
    public void ChangeData(string _host, string _port, string _userName, string _password, string _databaseName)
    {
        mysql = new MySqlAsset(_host, _port, _userName, _password, _databaseName);
        string user1Name = PlayerPrefs.GetString(Const.user1Name, "");
        string user2Name = PlayerPrefs.GetString(Const.user2Name, "");
        int? userScore = PlayerPrefs.GetInt(Const.playerScoreScene1, 0) + PlayerPrefs.GetInt(Const.playerScoreScene2, 0) +
        PlayerPrefs.GetInt(Const.playerScoreScene3, 0) + PlayerPrefs.GetInt(Const.playerScoreScene4, 0);
        Debug.Log(userScore.ToString());
        //将以上信息保存到数据库
        //带参数的Sql插入语句
        string sql;
        //单人模式
        if (playerChoice == 1)
        {
            //给变量赋值
            MySqlParameter[] pms = new MySqlParameter[]
            {
                 new MySqlParameter ("@at",MySqlDbType.VarChar,45){Value=user1Name.Trim() },
                new  MySqlParameter ("@scores",MySqlDbType.VarChar,45){Value=userScore }
            };
            DataSet oneResult = mysql.Select("oneplayerrank", new string[] { "*" }, new string[] { "account" }, new string[] { "=" }, new string[] { user1Name });
            if (oneResult != null && userScore != null)
            {
                DataTable table = oneResult.Tables[0];
                if (table.Rows.Count > 0)
                {
                   
                    sql = "update oneplayerrank set scores=@scores where account=@at";
                    rankList = ReloadData();
                    foreach (var item in rankList)
                    {
                        if (item["account"].ToString() == user1Name && (int)item["scores"] <= userScore)
                        {
                            mysql.ExecuteNonQuery(sql, pms);
                        }

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
                Debug.Log("保存成功");
            }
        }
        //双人模式
        else
        {
            //给变量赋值
            MySqlParameter[] pms = new MySqlParameter[]
            {
                 new MySqlParameter ("@account1",MySqlDbType.VarChar,45){Value=user1Name.Trim() },
                 new MySqlParameter ("@account2",MySqlDbType.VarChar,45){Value=user2Name.Trim() },
                new  MySqlParameter ("@score",MySqlDbType.VarChar,45){Value=userScore }
            };
            DataSet twoResult = mysql.Select("twoplayerrank", new string[] { "*" }, new string[] { "account1", "account2" }, new string[] { "=", "=" }, new string[] { user1Name, user2Name });
            if (twoResult != null && userScore != null)
            {
                DataTable table = twoResult.Tables[0];
                if (table.Rows.Count > 0)
                {
                    rankList = ReloadData();
                    sql = "update twoplayerrank set score=@score where account1=@account1 and account2=@account2";
                    foreach (var item in rankList)
                    {
                        if (item["account1"].ToString() == user1Name && item["account2"].ToString() == user2Name && (int)item["score"] <= userScore)
                        {
                            mysql.ExecuteNonQuery(sql, pms);
                        }
                    }
                }
                else
                {
                    sql = "insert into twoplayerrank(account1,account2,score) values(@account1,@account2,@score)";
                    mysql.ExecuteNonQuery(sql, pms);
                }
            }
            else { Debug.Log("保存成功"); }
        }
        mysql.CloseSql();
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
