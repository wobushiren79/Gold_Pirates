using System;
using UnityEditor;
using UnityEngine;

public class GameDataManager : BaseManager, IUserDataView
{
    protected UserDataController userDataController;
    protected BaseDataController baseDataController;
    protected LevelUpController levelUpController;

    protected UserDataBean userData = new UserDataBean();

    private void Awake()
    {
        userDataController = new UserDataController(this, this);
        baseDataController = new BaseDataController(this, null);
        levelUpController = new LevelUpController(this, null);
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

    public LevelUpBean GetLevelUpDataForGoldPrice(int level)
    {
        LevelUpBean levelUpData = levelUpController.GetLevelUpForPrice(level);
        return levelUpData;
    }
    public LevelUpBean GetLevelUpDataForSpeed(int level)
    {
        LevelUpBean levelUpData = levelUpController.GetLevelUpForSpeed(level);
        return levelUpData;
    }
    public LevelUpBean GetLevelUpDataForNumber(int level)
    {
        LevelUpBean levelUpData = levelUpController.GetLevelUpForNumber(level);
        return levelUpData;
    }

    public float GetLevelSceneExp(int level)
    {
        LevelUpBean levelUpData= levelUpController.GetLevelUpForScene(level);
        return levelUpData.add_exp;
    }

    public long GetLevelSceneMoney(int level)
    {
        LevelUpBean levelUpData = levelUpController.GetLevelUpForScene(level);
        return levelUpData.data_int;
    }

    public float GetSpeedUpAddSpeed()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.SpeedUp_AddSpeed);
        return float.Parse(baseData.content);
    }

    public float GetSpeedUpTime()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.SpeedUp_Time);
        return float.Parse(baseData.content);
    }

    public float GetFireCD()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Fire_CD);
        return float.Parse(baseData.content);
    }

    public float GetBulletHight()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Bullet_Hight);
        return float.Parse(baseData.content);
    }
    public float GetBulletSpeed()
    {
        BaseDataBean baseData = baseDataController.GetBaseData(BaseDataEnum.Bullet_Speed);
        return float.Parse(baseData.content);
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