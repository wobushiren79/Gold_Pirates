using System;
using UnityEditor;
using UnityEngine;

public class GameLevelController : BaseMVCController<GameLevelModel, IGameLevelView>
{
    public GameLevelController(BaseMonoBehaviour content, IGameLevelView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    public void GetGameLevelDataById(long id, Action<GameLevelBean> action)
    {
        GameLevelBean gameLevelData = GetModel().GetGameLevelDataById(id);
        if (gameLevelData == null)
        {
            GetView().GetGameLevelDataFail("没有数据");
        }
        else
        {
            GetView().GetGameLevelDataSuccess(gameLevelData, action);
        }
    }

    public void GetGameLevelDataByLevel(int level, Action<GameLevelBean> action)
    {
        GameLevelBean gameLevelData = GetModel().GetGameLevelDataByLevel(level);
        if (gameLevelData == null)
        {
            GetView().GetGameLevelDataFail("没有数据");
        }
        else
        {
            GetView().GetGameLevelDataSuccess(gameLevelData, action);
        }
    }
}