using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHandler : BaseHandler<GameDataManager>
{
    public enum NotifyForGameData
    {

    }

    protected UIManager manager_UI;

    private void Start()
    {
        manager.InitUserData();
    }

    private void Update()
    {

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
