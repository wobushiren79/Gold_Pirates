using System;
using UnityEditor;
using UnityEngine;

public interface IGameLevelView 
{
     void GetGameLevelDataSuccess(GameLevelBean gameLevelData,Action<GameLevelBean> action);

     void GetGameLevelDataFail(string failMsg);
}