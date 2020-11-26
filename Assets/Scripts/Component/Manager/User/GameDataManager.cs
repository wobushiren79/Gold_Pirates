using UnityEditor;
using UnityEngine;

public class GameDataManager : BaseManager, IUserDataView
{
    protected UserDataController userDataController;
    protected UserDataBean userData = new UserDataBean();

    private void Awake()
    {
        userDataController = new UserDataController(this, this);
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    public void InitUserData()
    {
        userDataController.GetUserData();
    }

    /// <summary>
    /// 获取用户数据
    /// </summary>
    /// <returns></returns>
    public UserDataBean GetUserData()
    {
        return userData;
    }

    #region 用户数据回调
    public void GetUserDataSuccess(UserDataBean userData)
    {
        if (userData != null)
            this.userData = userData;
    }

    public void GetUserDataFail(string failMsg)
    {

    }
    #endregion
}