using System;
using UnityEditor;
using UnityEngine;


[Serializable]
public class GameBean 
{
    public GameStatusEnum gameStatus;
    public long playerGold;
    public long enemyGold;

    //金币价值加成
    public int goldPrice;
    //玩家初始速度
    public float player_speed = 1;
    //玩家初始生命值
    public int player_life = 1;
    //玩家初始伤害
    public int player_damage = 1;

    //初始海盗数量
    public int initPirateNumber = 1;

    public int AddGoldPrice(int addGoldPrice)
    {
        goldPrice += addGoldPrice;
        return goldPrice;
    }

    public float AddPlayerSpeed(float speed)
    {
        player_speed += speed;
        return player_speed;
    }

    public int AddPlayerLife(int life)
    {
        player_life += life;
        return player_life;
    }

    public int AddPlayerDamage(int damage)
    {
        player_damage += damage;
        return player_damage;
    }

    public void AddPlayerGold(long gold)
    {
        playerGold += gold;
        if (playerGold < 0)
        {
            playerGold = 0;
        }
    }
    public void AddEnemyGold(long gold)
    {
        enemyGold += gold;
        if (enemyGold < 0)
        {
            enemyGold = 0;
        }
    }
}