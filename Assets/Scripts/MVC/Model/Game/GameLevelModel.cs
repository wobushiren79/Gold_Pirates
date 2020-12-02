using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameLevelModel : BaseMVCModel
{
    protected GameLevelService gameLevelService;

    public override void InitData()
    {
        gameLevelService = new GameLevelService();
    }

    /// <summary>
    /// 通过ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameLevelBean GetGameLevelDataById(long id)
    {
        List<GameLevelBean> listData = gameLevelService.QueryDataById(id);
        if (listData.Count > 0)
        {
            return listData[0];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 通过等级获取数据
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public GameLevelBean GetGameLevelDataByLevel(int level)
    {
        List<GameLevelBean> listData = gameLevelService.QueryDataByLevel(level);
        if (listData.Count > 0)
        {
            return listData[0];
        }
        else
        {
            return null;
        }
    }

}