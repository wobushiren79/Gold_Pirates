using System;
using UnityEditor;
using UnityEngine;

public class GameManager : BaseManager, IGameLevelView
{
    public GameLevelController gameLevelController;

    protected GameBean gameData  = new GameBean();
    protected GameLevelBean gameLevelData;
    protected ICallBack callBack;
    private void Awake()
    {
        gameLevelController = new GameLevelController(this, this);
    }
    public GameBean GetGameData()
    {
        return gameData;
    }

    public void SetGameData(GameBean gameData)
    {
        this.gameData = gameData;
    }

    public GameLevelBean GetGameLevelData()
    {
        return gameLevelData;
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
        this.gameLevelData = gameLevelData;
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