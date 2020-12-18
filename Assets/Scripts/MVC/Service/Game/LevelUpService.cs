using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelUpService : BaseMVCService
{
    public readonly string tableNameForSpeed = "level_speed";
    public readonly string tableNameForNumber = "level_number";
    public readonly string tableNameForPrice = "level_price";
    public readonly string tableNameForScene = "level_up";
    public LevelUpService() : base("", "")
    {
    }

    public List<LevelUpBean> QueryAllDataForSpeed()
    {
        return QueryAllData(tableNameForSpeed);
    }
    public List<LevelUpBean> QueryAllDataForNumber()
    {
        return QueryAllData(tableNameForNumber);
    }
    public List<LevelUpBean> QueryAllDataForPrice()
    {
        return QueryAllData(tableNameForPrice);
    }
    public List<LevelUpBean> QueryAllDataForScene()
    {
        return QueryAllData(tableNameForScene);
    }


    public List<LevelUpBean> QueryDataByLevelForSpeed(int level)
    {
        return QueryDataByLevel(tableNameForSpeed, level);
    }

    public List<LevelUpBean> QueryDataByLevelForNumber(int level)
    {
        return QueryDataByLevel(tableNameForNumber, level);
    }

    public List<LevelUpBean> QueryDataByLevelForPrice(int level)
    {
        return QueryDataByLevel(tableNameForPrice, level);
    }

    public List<LevelUpBean> QueryDataByLevelForScene(int level)
    {
        return QueryDataByLevel(tableNameForScene, level);
    }

    public List<LevelUpBean> QueryDataByLevel(string tableName, int level)
    {
        tableNameForMain = tableName;
        List<LevelUpBean> listData = BaseQueryData<LevelUpBean>("level", level + "");
        return listData;
    }

    public List<LevelUpBean> QueryAllData(string tableName)
    {
        tableNameForMain = tableName;
        return BaseQueryAllData<LevelUpBean>();
    }
}