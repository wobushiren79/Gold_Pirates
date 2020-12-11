using System;
using UnityEditor;
using UnityEngine;

public class GameManager : BaseManager, IGameLevelView
{
    public GameLevelController gameLevelController;

    protected ICallBack callBack;
    private void Awake()
    {
        gameLevelController = new GameLevelController(this, this);

    }

    public void SetCallBack(ICallBack callBack)
    {
        this.callBack = callBack;
    }

    public void GetGameLevelDataByLevel(int level, Action<GameLevelBean> callBack)
    {
        gameLevelController.GetGameLevelDataByLevel(level, callBack);
    }

    #region 数据回调
    public void GetGameLevelDataSuccess(GameLevelBean gameLevelData, Action<GameLevelBean> action)
    {
        if (callBack != null)
            callBack.GetGameLevelDataSuccess(gameLevelData, action);
    }

    public void GetGameLevelDataFail(string failMsg)
    {

    }
    #endregion

    public interface ICallBack
    {
        void GetGameLevelDataSuccess(GameLevelBean gameLevelData, Action<GameLevelBean> action);
    }
}