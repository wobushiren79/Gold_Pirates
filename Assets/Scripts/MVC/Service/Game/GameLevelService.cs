using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLevelService : BaseMVCService
{
    public GameLevelService() : base("game_level", "game_level_details_" + GameCommonInfo.GameConfig.language)
    {

    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<GameLevelBean> QueryDataById(long id)
    {
        return base.BaseQueryData<GameLevelBean>("id", id + "");
    }

    /// <summary>
    /// 通过关卡等级查询数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<GameLevelBean> QueryDataByLevel(int level)
    {
        return base.BaseQueryData<GameLevelBean>("level", level + "");
    }

    /// <summary>
    /// 根据等级插入数据
    /// </summary>
    /// <param name="gameLevelData"></param>
    public void InsertDataByLevel(GameLevelBean gameLevelData)
    {
        if (base.BaseDeleteData("level", gameLevelData.level + "")) 
        {
            base.BaseInsertData(tableNameForMain, gameLevelData);
        } 
    }
}