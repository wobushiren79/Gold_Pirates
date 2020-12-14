using System.Collections;
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

    public void AddUserGold(long number)
    {
        UserDataBean userData = GetUserData();
        userData.AddGold(number);
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
        return manager.GetLevelMax( BaseDataEnum.Level_Max_GoldPrice);
    }
    public int GetLevelMaxForSpeed()
    {
        return manager.GetLevelMax(BaseDataEnum.Level_Max_Speed);
    }
    public int GetLevelMaxForNumber()
    {
        return manager.GetLevelMax(BaseDataEnum.Level_Max_Number);
    }
    public int GetLevelAddForGoldPrice()
    {
        return manager.GetLevelAddForGoldPrice();
    }
    public float GetLevelAddForSpeed()
    {
        return manager.GetLevelAddForSpeed();
    }
    public int GetLevelAddForNumber()
    {
        return manager.GetLevelAddForNumber();
    }
    public long GetLevelMoneyForGoldPrice(int level)
    {
        return manager.GetLevelMoney( BaseDataEnum.Level_Money_GoldPrice, level);
    }
    public long GetLevelMoneyForSpeed(int level)
    {
        return manager.GetLevelMoney(BaseDataEnum.Level_Money_Speed, level);
    }
    public long GetLevelMoneyForNumber(int level)
    {
        return manager.GetLevelMoney(BaseDataEnum.Level_Money_Number, level);
    }
    public float GetLevelSceneExp()
    {
        return manager.GetLevelSceneExp();
    }
    public long GetLevelSceneMoney()
    {
        return manager.GetLevelSceneMoney();
    }

    public void HandleForGameDataChange()
    {

    }
}
