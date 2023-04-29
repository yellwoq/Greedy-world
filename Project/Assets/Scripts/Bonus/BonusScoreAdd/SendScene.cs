using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendScene : MonoBehaviour
{
    private int playerChoice;
    private int gameIndex;
    public AudioClip sendClip;
    private float sendwaitTime;
    public Hero hero1, hero2;
    private void Awake()
    {
        playerChoice = PlayerPrefs.GetInt(Const.playerChoice, 1);
        gameIndex = gameObject.scene.buildIndex;
    }
    private void Start()
    {
        AudioManager._instance.PlaySound(sendClip);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero" || collision.tag == "Hero2")
        {
            if (gameIndex >= 3 && gameIndex <= 5)
            {
                try
                {
                    hero1 = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
                    if (playerChoice == 2)
                    {
                        hero2 = GameObject.FindGameObjectWithTag("Hero2").GetComponent<Hero>();
                    }
                    if (hero1.bullectPrefab == hero1.bullectStrong[0])
                    { PlayerPrefs.SetInt(Const.player1Bullect, 1); }
                    else if (hero1.bullectPrefab == hero1.bullectStrong[1])
                    { PlayerPrefs.SetInt(Const.player1Bullect, 2); }
                    else if(hero1.bullectPrefab == hero1.bullectStrong[2])
                    { PlayerPrefs.SetInt(Const.player1Bullect, 3);}
                    else{ PlayerPrefs.SetInt(Const.player1Bullect, 0); }
                    if (playerChoice == 2)
                    {
                        if (hero2.bullectPrefab == hero2.bullectStrong[0])
                        { PlayerPrefs.SetInt(Const.player2Bullect, 1); }
                        else if (hero2.bullectPrefab == hero2.bullectStrong[1])
                        { PlayerPrefs.SetInt(Const.player2Bullect, 2); }
                        else if (hero2.bullectPrefab == hero2.bullectStrong[2])
                        { PlayerPrefs.SetInt(Const.player2Bullect, 3); }
                        else { PlayerPrefs.SetInt(Const.player2Bullect, 0); }
                    }
                }
                catch
                {
                    if (hero1 == null)
                    { Debug.Log("有玩家已经阵亡,阵亡玩家：1"); }
                    else
                    {Debug.Log("有玩家已经阵亡,阵亡玩家：2"); }
                }
                finally
                {
                    SceneManager.LoadSceneAsync(gameObject.scene.buildIndex + 1);
                }
            }
            else
            {
                if (playerChoice == 1)
                {
                    SceneManager.LoadSceneAsync(gameObject.scene.buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadSceneAsync(10);
                }
            }
            Destroy(gameObject, 5f);
        }
    }
}
