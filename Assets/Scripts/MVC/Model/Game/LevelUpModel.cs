using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelUpModel : BaseMVCModel
{

    protected LevelUpService levelUpService;

    public override void InitData()
    {
        levelUpService = new LevelUpService();
    }

    public List<LevelUpBean> GetLevelUpDataForSpeed()
    {
        return levelUpService.QueryAllDataForSpeed();
    }
    public List<LevelUpBean> GetLevelUpDataForNumber()
    {
        return levelUpService.QueryAllDataForNumber();
    }
    public List<LevelUpBean> GetLevelUpDataForPrice()
    {
        return levelUpService.QueryAllDataForPrice();
    }
    public List<LevelUpBean> GetLevelUpDataForScene()
    {
        return levelUpService.QueryAllDataForScene();
    }

    public LevelUpBean GetLevelUpDataForSpeedByLevel(int level)
    {
        List<LevelUpBean> listData = levelUpService.QueryDataByLevelForSpeed(level);
        return GetFirstData(listData);
    }

    public LevelUpBean GetLevelUpDataForNumberByLevel(int level)
    {
        List<LevelUpBean> listData = levelUpService.QueryDataByLevelForSpeed(level);
        return GetFirstData(listData);
    }

    public LevelUpBean GetLevelUpDataForPriceByLevel(int level)
    {
        List<LevelUpBean> listData = levelUpService.QueryDataByLevelForSpeed(level);
        return GetFirstData(listData);
    }

    public LevelUpBean GetLevelUpDataForSceneByLevel(int level)
    {
        List<LevelUpBean> listData = levelUpService.QueryDataByLevelForSpeed(level);
        return GetFirstData(listData);
    }

    private LevelUpBean GetFirstData(List<LevelUpBean> listData)
    {
        if (!CheckUtil.ListIsNull(listData))
        {
            return listData[0];
        }
        return null;
    }

}