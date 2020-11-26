using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataModel : BaseMVCModel
{
    protected UserDataService userDataService = new UserDataService();
    public override void InitData()
    {
        userDataService = new UserDataService();
    }

    /// <summary>
    /// 获取用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean GetUserData()
    {
       return userDataService.QueryData();
    } 

    /// <summary>
    /// 设置用户数据
    /// </summary>
    /// <param name="userData"></param>
    public void SetUserData(UserDataBean userData)
    {
        userDataService.UpdateData(userData);
    }
}
