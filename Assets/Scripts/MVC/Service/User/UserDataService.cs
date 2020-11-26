using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataService : BaseDataStorageImpl<UserDataBean>
{
    private readonly string mSaveFileName;

    public UserDataService()
    {
        mSaveFileName = "UserData";
    }

    /// <summary>
    /// 查询用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean QueryData()
    {
        return BaseLoadData(mSaveFileName);
    }

    /// <summary>
    /// 更新用户数据
    /// </summary>
    /// <param name="gameConfig"></param>
    public void UpdateData(UserDataBean userData)
    {
        BaseSaveData(mSaveFileName, userData);
    }
}
