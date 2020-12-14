using UnityEditor;
using UnityEngine;

public class GameDataManager : BaseManager, IUserDataView
{
    protected UserDataController userDataController;
    protected BaseDataController baseDataController;

    protected UserDataBean userData = new UserDataBean();

    private void Awake()
    {
        userDataController = new UserDataController(this, this);
        baseDataController = new BaseDataController(this, null);
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

    public int GetLevelMax(BaseDataEnum baseDataType)
    {
        BaseDataBean baseData =  baseDataController.GetBaseData(baseDataType);
        return int.Parse(baseData.content);
    }

    public int GetLevelAddForGoldPrice()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Level_Add_GoldPrice);
        return int.Parse(baseData.content);
    }
    public float GetLevelAddForSpeed()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Level_Add_Speed);
        return float.Parse(baseData.content);
    }
    public int GetLevelAddForNumber()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Level_Add_Number);
        return int.Parse(baseData.content);
    }

    public long GetLevelMoney(BaseDataEnum baseDataType, int level)
    {
        BaseDataBean baseData = baseDataController.GetBaseData(baseDataType);
        long[] listData = StringUtil.SplitBySubstringForArrayLong(baseData.content,',');
        if (level > listData.Length)
        {
            return listData[listData.Length - 1];
        }
        else
        {
            return listData[level - 1];
        }
    }

    public float GetLevelSceneExp()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Level_Scene_Exp);
        return float.Parse(baseData.content);
    }

    public long GetLevelSceneMoney()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Level_Scene_Money);
        return long.Parse(baseData.content);
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