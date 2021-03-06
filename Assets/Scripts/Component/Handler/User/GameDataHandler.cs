﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHandler : BaseHandler<GameDataManager>
{
    public enum NotifyForGameData
    {

    }

    private void Start()
    {
        manager.InitUserData();
    }

    private void Update()
    {

    }


    public void AddPirateNumber(int number)
    {
        UserDataBean userData = GetUserData();
        userData.AddPirateNumber(number);
    }

    public float AddSpeed(float addSpeed)
    {
        UserDataBean userData = GetUserData();
        return userData.AddSpeed(addSpeed);
    }

    public int AddLife(int addLife)
    {
        UserDataBean userData = GetUserData();
        return userData.AddLife(addLife);
    }

    public int AddDamage(int addDamage)
    {
        UserDataBean userData = GetUserData();
        return userData.AddDamage(addDamage);
    }

    /// <summary>
    /// 获取用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean GetUserData()
    {
        return manager.GetUserData();
    }

    public int GetLevelMaxForGoldPrice()
    {
        return manager.GetLevelMax(BaseDataEnum.Level_Max_GoldPrice);
    }
    public int GetLevelMaxForSpeed()
    {
        return manager.GetLevelMax(BaseDataEnum.Level_Max_Speed);
    }
    public int GetLevelMaxForNumber()
    {
        return manager.GetLevelMax(BaseDataEnum.Level_Max_Number);
    }

    public void GetLevelUpDataForGoldPrice(int level, out int addPrice,out long preGold)
    {
        LevelUpBean levelUpData= manager.GetLevelUpDataForGoldPrice(level);
        addPrice = levelUpData.data_int;
        preGold = levelUpData.pre_gold;
    }

    public void GetLevelLevelUpDataForSpeed(int level, out float addSpeed, out long preGold)
    {
        LevelUpBean levelUpData = manager.GetLevelUpDataForSpeed(level);
        addSpeed = levelUpData.data_float;
        preGold = levelUpData.pre_gold;
    }

    public void GetLevelLevelUpDataForNumber(int level, out int addNumber, out long preGold)
    {
        LevelUpBean levelUpData = manager.GetLevelUpDataForNumber(level);
        addNumber = levelUpData.data_int;
        preGold = levelUpData.pre_gold;
    }
    
    public float GetLevelSceneExp(int level)
    {
        return manager.GetLevelSceneExp(level);
    }

    public long GetLevelSceneMoney(int level)
    {
        return manager.GetLevelSceneMoney(level);
    }

    public float GetSpeedUpAddSpeed()
    {
        return manager.GetSpeedUpAddSpeed();
    }

    public float GetSpeedUpTime()
    {
        return manager.GetSpeedUpTime();
    }

    public float GetFireCD()
    {
        return manager.GetFireCD();
    }

    public float GetBulletHight()
    {
        return manager.GetBulletHight();
    }

    public float GetBulletSpeed()
    {
        return manager.GetBulletSpeed();
    }

    public void GetPlayerInitData(out float playerSpeed,out int playerLife)
    {
        playerLife = manager.GetPlayerInitLife();
        playerSpeed = manager.GetPlayerInitSpeed();
    }


    public void GetCameraData(out float minFov,out float maxFov,out float scaleSpeed)
    {
        maxFov = manager.GetCameraFovMax();
        minFov = manager.GetCameraFovMin();
        scaleSpeed = manager.GetCameraScaleSpeed();
    }

    public void GetAnglesForShip(out Vector3 anglesPlayer,out Vector3 anglesEnemy)
    {
        anglesPlayer = manager.GetAnglesPlayerShip();
        anglesEnemy = manager.GetAnglesEnemyShip();
    }

    public float GetCharacterCorpseDestoryTime()
    {
       return manager.GetCharacterCorpseDestoryTime();
    }

    public void HandleForGameDataChange()
    {

    }
}
