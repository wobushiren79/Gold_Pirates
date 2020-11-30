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

    /// <summary>
    /// 获取用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean GetUserData()
    {
        return manager.GetUserData();
    }

    public void HandleForGameDataChange()
    {

    }
}
