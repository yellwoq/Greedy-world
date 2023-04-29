using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MySqlAsset
{
    private static MySqlAsset _instance;
    //连接类对象
    private static MySqlConnection mySqlConnection;
    //IP地址
    private static string host;
    //端口号
    private static string port;
    //用户名
    private static string user1Name;
    //密码
    private static string password;
    //数据库名称
    private static string databaseName;
    //连接信息提示
    public string conMessage;

    public static MySqlAsset Instance { get => _instance; set => _instance = value; }
    public MySqlAsset() { }
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_host">IP地址</param>
    /// <param name="_port">端口</param>
    /// <param name="_userName">用户名</param>
    /// <param name="_password">密码</param>
    /// <param name="_databaseName">数据库名称</param>
    public MySqlAsset(string _host, string _port, string _userName, string _password, string _databaseName)
    {
        host = _host;
        port = _port;
        user1Name = _userName;
        password = _password;
        databaseName = _databaseName;
        Instance = this;
        OpenSql();
    }
    /// <summary>
    /// 打开数据库
    /// </summary>
    public void OpenSql()
    {
        try
        {
            string mySqlstring = string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4};",
               databaseName, host, user1Name, password, port);
            mySqlConnection = new MySqlConnection(mySqlstring);
            mySqlConnection.Open();
            if (mySqlConnection.State == ConnectionState.Open)
            { conMessage = "数据库连接成功"; }
        }
        catch (Exception e)
        {
            conMessage = string.Format("数据库连接失败：{0}", e.Message.ToString());
            Debug.LogErrorFormat("服务器连接失败，请重新检查MySql服务是否打开。错误原因：{0}", e.Message.ToString());
        }
    }
    /// <summary>
    /// 执行查询操作
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <returns>查询的结果</returns>
    public List<Dictionary<string, object>> ExecuteQuery(string sql)
    {

        // 实例化一个集合, 用来存储最终的结果
        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
        using (MySqlConnection con = new MySqlConnection(string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4};",
           databaseName, host, user1Name, password, port)))
        {
            
            using (MySqlCommand command = new MySqlCommand(sql, con))
            {
                con.Open();
                // 执行查询操作
                command.CommandText = sql;
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        { // 循环读取查询到的每一行的内容
                            while (reader.Read())
                            {
                                // 实例化一个Dictionary<string, object>，用来存储查询到每一行的键值对
                                Dictionary<string, object> item = new Dictionary<string, object>();
                                // 遍历查询到的一行的内容中所有的键
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    // 将查询到的键和值存储到item中
                                    item[reader.GetName(i)] = reader.GetValue(i);
                                }
                                // 在将这个存储了所有键值信息的Dictionary存储到result中
                                result.Add(item);
                            }
                        }
                        else
                        {
                            return null;
                        }

                    }

                }
                catch (MySqlException e)
                {
                    Debug.LogWarning("查询异常！" + e.ToString());
                }

                return result;
            }
        }
    }
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseSql()
    {
        if (mySqlConnection != null)
        {
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            mySqlConnection = null;

        }
    }
    /// <summary>
    /// 增删改
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="pms">参数集合</param>
    /// <returns></returns>
    public int ExecuteNonQuery(string sql, params MySqlParameter[] pms)
    {
        using (MySqlConnection con = new MySqlConnection(string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4};",
            databaseName, host, user1Name, password, port)))
        {
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
    /// <summary>
    /// 拼接查询语句，查询数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">要查询的列</param>
    /// <param name="whereColumnName">查询的条件列</param>
    /// <param name="operation">条件操作符</param>
    /// <param name="value">条件值</param>
    /// <returns></returns>
    public DataSet Select(string tableName, string[] items, string[] whereColumnName,
        string[] operation, string[] value)
    {
        if (whereColumnName.Length != operation.Length || operation.Length != value.Length)
        {
            throw new Exception("输入不正确:" + "要查询的条件，条件操作符、条件值的数量不一致！");
        }
        string query = "Select " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            query += "," + items[i];
        }
        query += " from " + tableName + " where " + whereColumnName[0] + " " + operation[0] + " '" + value[0] + "'";
        for (int i = 1; i < whereColumnName.Length; i++)
        {
            query += " and " + whereColumnName[i] + " " + operation[i] + " '" + value[i] + "'";
        }
        return QuerySet(query);
    }
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName">要插入的表名</param>
    /// <param name="items">要插入的列</param>
    /// <param name="value">插入的值</param>
    /// <returns></returns>
    public DataSet Insert(string tableName, string[] items, string[] value)
    {
        if (items.Length != value.Length)
        {
            throw new Exception("输入不正确:" + "要插入的列与值的列的数量不一致！");
        }
        string query = "Insert into " + tableName + "(" + items[0];
        for (int i = 1; i < items.Length - 1; i++)
        {
            query += "," + items[i];
        }
        query += "," + items[items.Length - 1] + ")" + " Value(" + "'" + value[0] + "'";
        for (int i = 1; i < value.Length - 1; i++)
        {
            query += "," + "'" + value[i] + "'";
        }
        query += "," + "'" + value[value.Length - 1] + "')";
        return QuerySet(query);
    }
    /// <summary>
    /// 执行语句
    /// </summary>
    /// <param name="sqlString">sql语句</param>
    /// <returns></returns>
    private DataSet QuerySet(string sqlString)
    {
        if (mySqlConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                mySqlAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
            finally
            {

            }
            return ds;
        }
        return null;
    }
}
