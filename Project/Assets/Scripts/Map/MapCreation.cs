using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreation : MonoBehaviour
{
    //当前游戏所在的场景序号
    private int SceneNum;
    //用来装饰初始化地图所需的数组
    #region 从林场景
    /*0.石头路  1、草  2、蘑菇1  3、蘑菇2  4、石子群 5、树根 6、死树 7.小型树群
8、中型树群 9、食物桶 10、稻谷桶 11、水桶 12、稻谷堆 13、花盆堆 14、大嘴花
15、河流 16、出生效果 17、花*/
    #endregion
    #region 沙漠场景
    /*0.绿草板 1、躲藏草  2、强力花  3、西瓜 4、攻击火架 5、仙人树  6、沙漠枯树 7、火架 
    8、小型仙人树 9、大型仙人树 10.死树11、石头群  12、死鱼地板 13、死树根 14、沙漠城堡 
    15、活树 16、桌子 17、出生效果*/
    #endregion
    #region 军事基地场景
    /*0.小炮塔 1、I站 2、四灯站  3、高塔 4、大战台 5、接收站  6、大炮台 7、A站
        8、移动友军 9、蓝蓝地板 10.紫蓝地板11、黄地板 12、铲子 13、攻击炮台 14、位置传送装置
        15、四炮站 16、接收地板 17、出生效果*/
    #endregion
    #region 洞穴场景
    /*0.蓝花 1、大城池2、长木椅  3、雕塑 4、石长椅 5、砖头碎片  6、树藤 7、水晶
    8、金矿 9、移动制造敌人机 10.黄金塑像11、死龙地板 12、出生效果 */
    #endregion
    public GameObject[] mapItem;
    public GameObject[] bonusItem;
    //玩家1，玩家2
    private GameObject player1, player2, boss;
    private List<Vector3> itemPositionList = new List<Vector3>();//已经有东西的位置列表
    public List<Vector3> sendList = new List<Vector3>();
    //产生敌人总次数
    private List<int> createEnemysCount = new List<int>();
    //敌人的集合
    private List<GameObject> enemysCollection;
    public bool isDestroy = false;
    public int choice;
    //产生的随机位置
    Vector3 createPosition;
    public AudioClip mapClip;
    private void Awake()
    {
        //获取当前游戏所在的场景序号
        SceneNum = gameObject.scene.buildIndex - 2;
        InitMap();
    }
    private void Start()
    {
        AudioManager._instance.PlayMusic(mapClip);
    }
    //初始化地图
    private void InitMap()
    {
        choice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        //实例化地图
        //当前场景为丛林
        if (SceneNum == 1)
        {
            #region  初始化玩家
            //产生玩家1
            player1 = Instantiate(mapItem[16], new Vector3(-7, 0, 0), Quaternion.identity);
            player1.GetComponent<Born>().iscreatePlayer1 = true;
            if (choice == 2)
            {
                //产生玩家2
                player2 = Instantiate(mapItem[16], new Vector3(-7, -0.5f, 0), Quaternion.identity);
                player2.GetComponent<Born>().iscreatePlayer2 = true;
            }
            #endregion
            #region  初始化敌人
            createEnemysCount.Clear();
            //产生敌人
            CreateItem(mapItem[16], new Vector3(6.5f, 3, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[16], new Vector3(6.5f, 0, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[16], new Vector3(6.5f, -3, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            InvokeRepeating("JustGame", 4, 5);
            #endregion
            // 0.石头路  1、草  2、蘑菇1  3、蘑菇2  4、石子群 5、树根 6、死树 7.小型树群
            //8、中型树群 9、食物桶 10、稻谷桶 11、水桶 12、稻谷堆 13、花盆堆 14、大嘴花
            //15、河流 16、出生效果 17、花
            #region 丛林
            // 0.石头路 
            for (int i = 0; i < 100; i++)
            {
                CreateItem(mapItem[0], CreateRandomPosition(), Quaternion.identity);
            }
            //1、草
            for (int i = 0; i < 50; i++)
            {
                CreateItem(mapItem[1], CreateRandomPosition(), Quaternion.identity);
            }
            //2、蘑菇1
            for (int i = 0; i < 30; i++)
            {
                CreateItem(mapItem[2], CreateRandomPosition(), Quaternion.identity);
            }
            //3、蘑菇2
            for (int i = 0; i < 30; i++)
            {
                CreateItem(mapItem[3], CreateRandomPosition(), Quaternion.identity);
            }
            //4、石子群
            for (int i = 0; i < 30; i++)
            {
                CreateItem(mapItem[4], CreateRandomPosition(), Quaternion.identity);
            }
            //5、树根
            for (int i = 0; i < 30; i++)
            {
                CreateItem(mapItem[5], CreateRandomPosition(), Quaternion.identity);
            }
            //6、死树
            for (int i = 0; i < 30; i++)
            {
                CreateItem(mapItem[6], CreateRandomPosition(), Quaternion.identity);
            }
            //7.小型树群
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[7], CreateRandomPosition(), Quaternion.identity);
            }
            //8、中型树群    
            for (int i = 0; i < 35; i++)
            {
                CreateItem(mapItem[8], CreateRandomPosition(), Quaternion.identity);
            }
            //9、食物桶
            for (int i = 0; i < 35; i++)
            {
                CreateItem(mapItem[9], CreateRandomPosition(), Quaternion.identity);
            }
            //10、稻谷桶
            for (int i = 0; i < 25; i++)
            {
                CreateItem(mapItem[10], CreateRandomPosition(), Quaternion.identity);
            }
            //11、水桶
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[11], CreateRandomPosition(), Quaternion.identity);
            }
            //12、稻谷堆
            for (int i = 0; i < 5; i++)
            {
                CreateItem(mapItem[12], CreateRandomPosition(), Quaternion.identity);
            }
            //13、花盆堆
            for (int i = 0; i < 5; i++)
            {
                CreateItem(mapItem[13], CreateRandomPosition(), Quaternion.identity);
            }
            //14、大嘴花
            for (int i = 0; i < 2; i++)
            {
                CreateItem(mapItem[14], CreateRandomPosition(), Quaternion.identity);
            }
            //15、河流  
            for (int i = 0; i < 1; i++)
            {
                CreateItem(mapItem[15], new Vector3(Random.Range(-4, 4), Random.Range(-1, 1), 0), Quaternion.identity);
            }
            //17、花
            for (int i = 0; i < 30; i++)
            {
                CreateItem(mapItem[17], CreateRandomPosition(), Quaternion.identity);
            }
            #endregion
        }
        //当前场景为沙漠
        else if (SceneNum == 2)
        {
            /*0.绿草板 1、躲藏草  2、强力花  3、西瓜 4、攻击火架 5、仙人树  6、沙漠枯树 7、火架 
            8、小型仙人树 9、大型仙人树 10.死树11、石头群  12、死鱼地板 13、死树根 14、沙漠城堡 
            15、活树 16、桌子17、出生效果*/
            #region  初始化玩家
            //产生玩家1
            player1 = Instantiate(mapItem[17], new Vector3(-7, 3, 0), Quaternion.identity);
            player1.GetComponent<Born>().iscreatePlayer1 = true;
            if (choice == 2)
            {
                //产生玩家2
                player2 = Instantiate(mapItem[17], new Vector3(-7, -2.8f, 0), Quaternion.identity);
                player2.GetComponent<Born>().iscreatePlayer2 = true;
            }
            #endregion
            #region  初始化敌人
            createEnemysCount.Clear();
            //产生敌人
            CreateItem(mapItem[17], new Vector3(0, 4.5f, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[17], new Vector3(0, -4.5f, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[17], new Vector3(7, 3, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[17], new Vector3(7, -3, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            InvokeRepeating("JustGame", 4, 5);
            #endregion
            #region 沙漠   
            // 0.绿草板 
            for (int i = 0; i < 50; i++)
            {
                CreateItem(mapItem[0], CreateRandomPosition(), Quaternion.identity);
            }
            //1、躲藏草
            for (int i = 0; i < 50; i++)
            {
                CreateItem(mapItem[1], CreateRandomPosition(), Quaternion.identity);
            }
            //2、强力花
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[2], CreateRandomPosition(), Quaternion.identity);
            }
            //3、西瓜
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[3], CreateRandomPosition(), Quaternion.identity);
            }
            //4、攻击火架
            for (int i = 0; i < 3; i++)
            {
                CreateItem(mapItem[4], CreateRandomPosition(), Quaternion.identity);
            }
            //5、仙人树
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[5], CreateRandomPosition(), Quaternion.identity);
            }
            //6、沙漠枯树
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[6], CreateRandomPosition(), Quaternion.identity);
            }
            //7、火架
            for (int i = 0; i < 3; i++)
            {
                CreateItem(mapItem[7], CreateRandomPosition(), Quaternion.identity);
            }
            // 8、小型仙人树    
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[8], CreateRandomPosition(), Quaternion.identity);
            }
            //9、大型仙人树
            for (int i = 0; i < 10; i++)
            {
                CreateItem(mapItem[9], CreateRandomPosition(), Quaternion.identity);
            }
            //10.死树
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[10], CreateRandomPosition(), Quaternion.identity);
            }
            //11、石头群
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[11], CreateRandomPosition(), Quaternion.identity);
            }
            //12、死鱼地板  
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[12], CreateRandomPosition(), Quaternion.identity);
            }
            //13、死树根
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[13], CreateRandomPosition(), Quaternion.identity);
            }
            //14、沙漠城堡
            for (int i = 0; i < 1; i++)
            {
                CreateItem(mapItem[14], new Vector3(Random.Range(-4, 4), Random.Range(-1, 1), 0), Quaternion.identity);
            }
            //15、活树  
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[15], CreateRandomPosition(), Quaternion.identity);
            }
            //16、桌子
            for (int i = 0; i < 25; i++)
            {
                CreateItem(mapItem[16], CreateRandomPosition(), Quaternion.identity);
            }
            #endregion
        }
        //当前场景为军事基地
        else if (SceneNum == 3)
        {
            /*0.小炮塔 1、I站 2、四灯站  3、高塔 4、大战台 5、接收站  6、大炮台 7、A站
            8、移动友军 9、蓝蓝地板 10.紫蓝地板11、黄地板 12、铲子 13、攻击炮台 14、位置传送装置
            15、四炮站 16、接收地板 17、出生效果*/
            #region  初始化玩家
            //产生玩家1
            player1 = Instantiate(mapItem[17], new Vector3(-7, 0.5f, 0), Quaternion.identity);
            player1.GetComponent<Born>().iscreatePlayer1 = true;
            if (choice == 2)
            {
                //产生玩家2
                player2 = Instantiate(mapItem[17], new Vector3(-7, 0, 0), Quaternion.identity);
                player2.GetComponent<Born>().iscreatePlayer2 = true;
            }
            #endregion
            #region  初始化敌人
            createEnemysCount.Clear();
            //产生敌人
            CreateItem(mapItem[17], new Vector3(0, 4, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[17], new Vector3(0, -4, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[17], new Vector3(7, 0, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            InvokeRepeating("JustGame", 3, 4);
            #endregion
            #region 军事基地  
            // 0.小炮塔
            for (int i = 0; i < 50; i++)
            {
                CreateItem(mapItem[0], CreateRandomPosition(), Quaternion.identity);
            }
            //1、I站
            for (int i = 0; i < 50; i++)
            {
                CreateItem(mapItem[1], CreateRandomPosition(), Quaternion.identity);
            }
            //2、四灯站
            for (int i = 0; i < 35; i++)
            {
                CreateItem(mapItem[2], CreateRandomPosition(), Quaternion.identity);
            }
            //3、高塔
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[3], CreateRandomPosition(), Quaternion.identity);
            }
            //4、大战台
            for (int i = 0; i < 20; i++)
            {
                CreateItem(mapItem[4], CreateRandomPosition(), Quaternion.identity);
            }
            //5、接收站
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[5], CreateRandomPosition(), Quaternion.identity);
            }
            //6、大炮台
            for (int i = 0; i < 35; i++)
            {
                CreateItem(mapItem[6], CreateRandomPosition(), Quaternion.identity);
            }
            //7、A站
            for (int i = 0; i < 25; i++)
            {
                CreateItem(mapItem[7], CreateRandomPosition(), Quaternion.identity);
            }
            // 8、移动友军   
            for (int i = 0; i < 3; i++)
            {
                CreateItem(mapItem[8], CreateRandomPosition(), Quaternion.identity);
            }
            // 9、蓝蓝地板
            for (int i = 0; i < 25; i++)
            {
                CreateItem(mapItem[9], CreateRandomPosition(), Quaternion.identity);
            }
            //10.紫蓝地板
            for (int i = 0; i < 45; i++)
            {
                CreateItem(mapItem[10], CreateRandomPosition(), Quaternion.identity);
            }
            //11、黄地板
            for (int i = 0; i < 5; i++)
            {
                CreateItem(mapItem[11], CreateRandomPosition(), Quaternion.identity);
            }
            // 12、铲子  
            for (int i = 0; i < 10; i++)
            {
                CreateItem(mapItem[12], CreateRandomPosition(), Quaternion.identity);
            }
            //13、攻击炮台
            for (int i = 0; i < 3; i++)
            {
                CreateItem(mapItem[13], CreateRandomPosition(), Quaternion.identity);
            }
            //14、位置传送装置
            for (int i = 0; i < 1; i++)
            {
                CreateItem(mapItem[14], new Vector3(Random.Range(-4, 4), Random.Range(-1, 1), 0), Quaternion.identity);
            }
            //15、四炮站 
            for (int i = 0; i < 40; i++)
            {
                CreateItem(mapItem[15], CreateRandomPosition(), Quaternion.identity);
            }
            //16、接收地板
            for (int i = 0; i < 5; i++)
            {
                CreateItem(mapItem[16], CreateRandomPosition(), Quaternion.identity);
            }
            #endregion
        }
        //当前场景为洞穴
        else if (SceneNum == 4)
        {
            /*0.蓝花 1、大城池2、长木椅  3、雕塑 4、石长椅 5、砖头碎片  6、树藤 7、水晶
            8、金矿 9、移动制造敌人机 10.黄金塑像11、死龙地板 12、出生效果 13、Boss */
            #region  初始化玩家
            //产生玩家1
            player1 = Instantiate(mapItem[12], new Vector3(-7, -3.5f, 0), Quaternion.identity);
            player1.GetComponent<Born>().iscreatePlayer1 = true;
            if (choice == 2)
            {
                //产生玩家2
                player2 = Instantiate(mapItem[12], new Vector3(-6.5f, -3.5f, 0), Quaternion.identity);
                player2.GetComponent<Born>().iscreatePlayer2 = true;
            }
            if (SceneNum == 12)
            {
                //产生玩家2
                player2 = Instantiate(mapItem[12], new Vector3(-6.5f, -3.5f, 0), Quaternion.identity);
                player2.GetComponent<Born>().iscreatePlayer2 = true;
            }
            #endregion
            #region  初始化敌人
            createEnemysCount.Clear();
            //产生敌人
            boss = Instantiate(mapItem[12], new Vector3(6.5f, 0, 0), Quaternion.identity);
            boss.GetComponent<Born>().iscreateBoss = true;
            CreateItem(mapItem[12], new Vector3(-3.5f, 4, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[12], new Vector3(0.2f, 4, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            CreateItem(mapItem[12], new Vector3(4, 4, 0), Quaternion.identity);
            createEnemysCount.Add(1);
            InvokeRepeating("JustGame", 3, 3);
            #endregion
            #region 洞穴  
            if (SceneNum == 4)
            {
                // 0.蓝花
                for (int i = 0; i < 50; i++)
                {
                    CreateItem(mapItem[0], CreateRandomPosition(), Quaternion.identity);
                }
                //1、大城池
                for (int i = 0; i < 1; i++)
                {
                    CreateItem(mapItem[1], new Vector3(0, 0, 0), Quaternion.identity);
                }
                //2、长木椅
                for (int i = 0; i < 35; i++)
                {
                    CreateItem(mapItem[2], CreateRandomPosition(), Quaternion.identity);
                }
                //3、雕塑
                for (int i = 0; i < 20; i++)
                {
                    CreateItem(mapItem[3], CreateRandomPosition(), Quaternion.identity);
                }
                //4、石长椅
                for (int i = 0; i < 30; i++)
                {
                    CreateItem(mapItem[4], CreateRandomPosition(), Quaternion.identity);
                }
                // 5、砖头碎片
                for (int i = 0; i < 40; i++)
                {
                    CreateItem(mapItem[5], CreateRandomPosition(), Quaternion.identity);
                }
                //6、树藤
                for (int i = 0; i < 35; i++)
                {
                    CreateItem(mapItem[6], CreateRandomPosition(), Quaternion.identity);
                }
                //7、水晶
                for (int i = 0; i < 35; i++)
                {
                    CreateItem(mapItem[7], CreateRandomPosition(), Quaternion.identity);
                }
                // 8、金矿    
                for (int i = 0; i < 10; i++)
                {
                    CreateItem(mapItem[8], CreateRandomPosition(), Quaternion.identity);
                }
                // 9、移动制造敌人机
                for (int i = 0; i < 3; i++)
                {
                    CreateItem(mapItem[9], CreateRandomPosition(), Quaternion.identity);
                }
                //10.黄金塑像
                for (int i = 0; i < 10; i++)
                {
                    CreateItem(mapItem[10], CreateRandomPosition(), Quaternion.identity);
                }
                //11、死龙地板
                for (int i = 0; i < 60; i++)
                {
                    CreateItem(mapItem[11], CreateRandomPosition(), Quaternion.identity);
                }
            }
            else if (SceneNum == 5)
            {
                // 0.蓝花
                for (int i = 0; i < 10; i++)
                {
                    CreateItem(mapItem[0], CreateRandomPosition(), Quaternion.identity);
                }
                //1、开关
                for (int i = 0; i < 10; i++)
                {
                    CreateItem(mapItem[1], new Vector3(0, 0, 0), Quaternion.identity);
                }
                //2、长木椅
                for (int i = 0; i < 15; i++)
                {
                    CreateItem(mapItem[2], CreateRandomPosition(), Quaternion.identity);
                }
                //3、雕塑
                for (int i = 0; i < 10; i++)
                {
                    CreateItem(mapItem[3], CreateRandomPosition(), Quaternion.identity);
                }
                //4、石长椅
                for (int i = 0; i < 20; i++)
                {
                    CreateItem(mapItem[4], CreateRandomPosition(), Quaternion.identity);
                }
                // 5、砖头碎片
                for (int i = 0; i < 20; i++)
                {
                    CreateItem(mapItem[5], CreateRandomPosition(), Quaternion.identity);
                }
                //6、树藤
                for (int i = 0; i < 25; i++)
                {
                    CreateItem(mapItem[6], CreateRandomPosition(), Quaternion.identity);
                }
                //7、水晶
                for (int i = 0; i < 25; i++)
                {
                    CreateItem(mapItem[7], CreateRandomPosition(), Quaternion.identity);
                }
                // 8、金矿    
                for (int i = 0; i < 10; i++)
                {
                    CreateItem(mapItem[8], CreateRandomPosition(), Quaternion.identity);
                }
                // 9、移动制造敌人机
                for (int i = 0; i < 4; i++)
                {
                    CreateItem(mapItem[9], CreateRandomPosition(), Quaternion.identity);
                }
                //10.黄金塑像
                for (int i = 0; i < 15; i++)
                {
                    CreateItem(mapItem[10], CreateRandomPosition(), Quaternion.identity);
                }
                //11、死龙地板
                for (int i = 0; i < 60; i++)
                {
                    CreateItem(mapItem[11], CreateRandomPosition(), Quaternion.identity);
                }
            }
            #endregion
        }
    }
    //实例化
    public void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    //地图在随机位置产生
    public Vector3 CreateRandomPosition()
    {
        while (true)
        {
            //丛林场景//沙漠场景
            //不生成x=-6~-7.5,6~7.5的两列，y=-3~-5,3~5的这两行位置
            if (SceneNum == 1 || SceneNum == 2 || SceneNum == 4)
            {
                createPosition = new Vector3(Random.Range(-5.5f, 6f), Random.Range(-2.5f, 3f), 0);
            }
            //军事基地场景
            //不生成x=-6.5~-7.5,6~7.5的两列，y=-3~-5,3~5的这两行位置
            else if (SceneNum == 3)
            {
                createPosition = new Vector3(Random.Range(-6f, 5.5f), Random.Range(-2.5f, 2.5f), 0);
            }
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }

    }
    //用来判断位置列表中是否有这个位置
    public bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }
    //判断产生敌人数量
    private void JustGame()
    {
        if (createEnemysCount.Count <= EnemysRemand())
        {
            CreateEnemy();
        }
        else
        {
            return;
        }
    }
    //要求的敌人总数
    private int EnemysRemand()
    {
        if (SceneNum == 1)
        {
            if (choice == 2) { return 25; }
            return 15;
        }
        else if (SceneNum == 2)
        {
            if (choice == 2) { return 35; }
            return 25;
        }
        else if (SceneNum == 3)
        {
            if (choice == 2) { return 45; }
            return 35;
        }
        else if (SceneNum == 4)
        {
            if (choice == 2) { return 45; }
            return 35;
        }
        return 0;
    }
    //产生敌人的方法
    private void CreateEnemy()
    {
        if (SceneNum == 1)
        {
            int num = Random.Range(0, 3);
            Vector3 EnemyPos = new Vector3();
            if (num == 0)
            {
                EnemyPos = new Vector3(6.5f, 0, 0);
            }
            else if (num == 1)
            {
                EnemyPos = new Vector3(6.5f, 3, 0);
            }
            else
            {
                EnemyPos = new Vector3(6.5f, -3, 0);
            }
            CreateItem(mapItem[16], EnemyPos, Quaternion.identity);
            createEnemysCount.Add(1);
        }
        else if (SceneNum == 2)
        {
            int num = Random.Range(0, 4);
            Vector3 EnemyPos = new Vector3();
            if (num == 0)
            {
                EnemyPos = new Vector3(0, 4.5f, 0);
            }
            else if (num == 1)
            {
                EnemyPos = new Vector3(7, 3, 0);
            }
            else if (num == 2)
            {
                EnemyPos = new Vector3(7, -3, 0);
            }
            else
            {
                EnemyPos = new Vector3(0, -4.5f, 0);
            }
            CreateItem(mapItem[17], EnemyPos, Quaternion.identity);
            createEnemysCount.Add(1);
        }
        else if (SceneNum == 3)
        {
            int num = Random.Range(0, 3);
            Vector3 EnemyPos = new Vector3();
            if (num == 0)
            {
                EnemyPos = new Vector3(7, 0, 0);
            }
            else if (num == 1)
            {
                EnemyPos = new Vector3(0, 4, 0);
            }
            else
            {
                EnemyPos = new Vector3(0, -4, 0);
            }
            CreateItem(mapItem[17], EnemyPos, Quaternion.identity);
            createEnemysCount.Add(1);
        }
        else if (SceneNum == 4)
        {
            int num = Random.Range(0, 3);
            Vector3 EnemyPos = new Vector3();
            if (num == 0)
            {
                EnemyPos = new Vector3(-3.5f, 4, 0);
            }
            else if (num == 1)
            {
                EnemyPos = new Vector3(0.2f, 4, 0);
            }
            else
            {
                EnemyPos = new Vector3(4, 4, 0);
            }
            CreateItem(mapItem[12], EnemyPos, Quaternion.identity);
            createEnemysCount.Add(1);
        }

    }
}