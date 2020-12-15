using System;
using UnityEditor;
using UnityEngine;


[Serializable]
public class GameBean
{
    public GameStatusEnum gameStatus;
    public int playerGoldNumber;
    public int enemyGoldNumber;

    //场景等级
    public int levelForScene = 1;
    //场景等级经验
    public float levelProgressForScene = 0;

    //金币价值等级
    public int levelForGoldPrice = 1;
    //金币价值加成
    public int goldPrice;

    //玩家速度等级
    public int levelForSpeed = 1;
    //玩家初始速度
    public float playForSpeed = 2;

    //海盗数量等级
    public int levelForPirateNumber = 1;
    //初始海盗数量
    public int playForPirateNumber = 1;

    //玩家初始生命值
    public int playerForLife = 1;
    //玩家初始伤害
    public int playerForDamage = 1;

    public int GetGoldPrice()
    {
        return goldPrice;
    }
    public int GetGoldPriceLevel()
    {
        return levelForGoldPrice;
    }
    public float GetPlayerSpeed()
    {
        return playForSpeed;
    }
    public float GetPlayerSpeedLevel()
    {
        return levelForSpeed;
    }
    public int GetPlayPirateNumber()
    {
        return playForPirateNumber;
    }
    public int GetPlayPirateNumberLevel()
    {
        return levelForPirateNumber;
    }

    /// <summary>
    /// 金币价值升级
    /// </summary>
    /// <param name="maxLevel"></param>
    /// <param name="addGoldPrice"></param>
    /// <returns></returns>
    public bool LevelUpForGoldPrice(int maxLevel, int addGoldPrice)
    {
        if (levelForGoldPrice >= maxLevel)
        {
            return false;
        }
        else
        {
            levelForGoldPrice++;
            goldPrice += addGoldPrice;
            return true;
        }
    }

    /// <summary>
    /// 角色速度升级
    /// </summary>
    /// <param name="maxLevel"></param>
    /// <param name="addSpeed"></param>
    /// <returns></returns>
    public bool LevelUpForPlayerSpeed(int maxLevel, float addSpeed)
    {
        if (levelForSpeed >= maxLevel)
        {
            return false;
        }
        else
        {
            levelForSpeed++;
            playForSpeed += addSpeed;
            return true;
        }
    }

    /// <summary>
    /// 海盗数量升级
    /// </summary>
    /// <param name="maxLevel"></param>
    /// <param name="addNumer"></param>
    /// <returns></returns>
    public bool LevelUpForPlayerPirateNumber(int maxLevel, int addNumer)
    {
        if (levelForPirateNumber >= maxLevel)
        {
            return false;
        }
        else
        {
            levelForPirateNumber++;
            playForPirateNumber += addNumer;
            return true;
        }
    }

    public void LevelDownForPlayerPirateNumber(int downLevel)
    {
        levelForPirateNumber -= downLevel;
        if (levelForPirateNumber < 1)
            levelForPirateNumber = 1;
    }

    public int AddPlayerLife(int life)
    {
        playerForLife += life;
        return playerForLife;
    }

    public int AddPlayerDamage(int damage)
    {
        playerForDamage += damage;
        return playerForDamage;
    }

    public void AddPlayerGoldNumber(int goldNumber)
    {
        playerGoldNumber += goldNumber;
        if (playerGoldNumber < 0)
        {
            playerGoldNumber = 0;
        }
    }

    public void AddEnemyGoldNumber(int goldNumber)
    {
        enemyGoldNumber += goldNumber;
        if (enemyGoldNumber < 0)
        {
            enemyGoldNumber = 0;
        }
    }

    public void AddLevelProgressForScene(float pro)
    {
        levelProgressForScene += pro;
        if (levelProgressForScene > 1)
        {
            levelProgressForScene = 1;
        }
    }

    public void LevelUpForScene()
    {
        levelProgressForScene = 0;
        levelForScene++;
    }
}