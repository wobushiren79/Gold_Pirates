using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelUpController : BaseMVCController<LevelUpModel, ILevelUpView>
{
    public Dictionary<int, LevelUpBean> mapSpeedData = new Dictionary<int, LevelUpBean>();
    public int levelMaxSpeed;
    public Dictionary<int, LevelUpBean> mapNumberData = new Dictionary<int, LevelUpBean>();
    public int levelMaxNumber;
    public Dictionary<int, LevelUpBean> mapPriceData = new Dictionary<int, LevelUpBean>();
    public int levelMaxPrice;
    public Dictionary<int, LevelUpBean> mapSceneData = new Dictionary<int, LevelUpBean>();
    public int levelMaxScene;

    public LevelUpController(BaseMonoBehaviour content, ILevelUpView view) : base(content, view)
    {
       InitLevelData();
    }
    public override void InitData()
    {

    }

    public void InitLevelData()
    {
        List<LevelUpBean> listSpeedData = GetModel().GetLevelUpDataForSpeed();
        SetListData(mapSpeedData, listSpeedData, out levelMaxSpeed);
        List<LevelUpBean> listNumberData = GetModel().GetLevelUpDataForNumber();
        SetListData(mapNumberData, listNumberData, out levelMaxNumber);
        List<LevelUpBean> listPriceData = GetModel().GetLevelUpDataForPrice();
        SetListData(mapPriceData, listPriceData, out levelMaxPrice);
        List<LevelUpBean> listSceneData = GetModel().GetLevelUpDataForScene();
        SetListData(mapSceneData, listSceneData, out levelMaxScene);
    }

    public LevelUpBean GetLevelUpForSpeed(int level)
    {
        if (mapSpeedData.TryGetValue(level, out LevelUpBean value))
        {
            return value;
        }
        else
        {
            return mapSpeedData[levelMaxSpeed];
        }
    }

    public LevelUpBean GetLevelUpForNumber(int level)
    {
        if (mapNumberData.TryGetValue(level, out LevelUpBean value))
        {
            return value;
        }
        else
        {
            return mapNumberData[levelMaxNumber];
        }
    }


    public LevelUpBean GetLevelUpForPrice(int level)
    {
        if (mapPriceData.TryGetValue(level, out LevelUpBean value))
        {
            return value;
        }
        else
        {
            return mapPriceData[levelMaxPrice];
        }
    }


    public LevelUpBean GetLevelUpForScene(int level)
    {
        if (mapSceneData.TryGetValue(level, out LevelUpBean value))
        {
            return value;
        }
        else
        {
            return mapSceneData[levelMaxScene];
        }
    }


    protected void SetListData(Dictionary<int, LevelUpBean> mapData, List<LevelUpBean> listData, out int levelMax)
    {
        mapData.Clear();
        levelMax = 0;
        for (int i = 0; i < listData.Count; i++)
        {
            LevelUpBean levelUpData = listData[i];
            if (!mapData.ContainsKey(levelUpData.level))
            {
                mapData.Add(levelUpData.level, levelUpData);
            }
            if (levelUpData.level > levelMax)
            {
                levelMax = levelUpData.level;
            }
        }
    }

}