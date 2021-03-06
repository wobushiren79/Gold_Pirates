﻿using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameManager>, GameManager.ICallBack
{
    public UIManager manager_UI;
    public GoldHandler handler_Gold;
    public ShipHandler handler_Ship;
    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;

    protected override void Awake()
    {
        base.Awake();
        manager.SetCallBack(this);
    }


    private void Start()
    {
        Action<GameLevelBean> callBack = (gameLevelData) =>
        {
            ChangeGameStatus(GameStatusEnum.GamePre);
            ChangeGameStatus(GameStatusEnum.GameIng);
        };
        InitGameLevelData(1, callBack);
    }

    /// <summary>
    /// 升级
    /// </summary>
    public void LevelUpScene(int addMoneyRate,float deleyAddGoldTime)
    {
        GameBean gameData = manager.GetGameData();
        if (gameData.levelProgressForScene < 1)
            return;
        int totalLevel = gameData.levelForSpeed + gameData.levelForGoldPrice + gameData.levelForPirateNumber;
        long addMoney = handler_GameData.GetLevelSceneMoney(totalLevel);
        gameData.LevelUpForScene();
        StartCoroutine(CoroutineForDelayAddGold(deleyAddGoldTime, addMoneyRate * addMoney));
    }

    public void AddUserGold(long number)
    {
        GameBean gameData=  manager.GetGameData();
        gameData.AddGold(number);
    }

    public long AddGoldNumber(CharacterTypeEnum characterType, long goldPrice,int goldNumber)
    {
        long addGold = 0;
        switch (characterType)
        {
            case CharacterTypeEnum.Player:
                GetGameData().AddPlayerGoldNumber(goldNumber);
                addGold = goldNumber * (goldPrice + GetGameData().goldPrice);
                AddUserGold(addGold);
                break;
            case CharacterTypeEnum.Enemy:
                GetGameData().AddEnemyGoldNumber(goldNumber);
                break;
        }
        return addGold;
    }

    public void InitGameLevelData(int level, Action<GameLevelBean> action)
    {
        manager.GetGameLevelDataByLevel(level, action);
    }

    public void ChangeGameStatus(GameStatusEnum gameStatus)
    {
        GetGameData().gameStatus = gameStatus;
        switch (gameStatus)
        {
            case GameStatusEnum.GamePre:
                //扫描地形
                //AstarPath.active.ScanAsync();
                //初始化数据
                handler_GameData.GetPlayerInitData(out float playerSpeed, out int playerLife);
                GameBean gameData = new GameBean(playerSpeed, playerLife);
                SetGameData(gameData);
                manager_UI.RefreshAllUI();
                //打开UI
                manager_UI.OpenUIAndCloseOther<UIGameStart>(UIEnum.GameStart);
                //创建金币
                GameLevelBean gameLevelData = GetGameLevelData();
                handler_Gold.CreateGold(gameLevelData.gold_pile,gameLevelData.gold_number, gameLevelData.gold_id);
                break;
            case GameStatusEnum.GameIng:
                //开启角色创建
                UserDataBean userData= handler_GameData.GetUserData();
                CharacterDataBean playerCharacterData = new CharacterDataBean(CharacterTypeEnum.Player)
                {
                    life = userData.life + GetGameData().playerForLife,
                    maxLife = userData.life + GetGameData().playerForLife,
                    moveSpeed = userData.speed + GetGameData().GetPlayerSpeed()
                };
                CharacterDataBean enemyCharacterData = new CharacterDataBean(CharacterTypeEnum.Enemy)
                {
                    life = GetGameLevelData().enemy_life,
                    maxLife = GetGameLevelData().enemy_life,
                    moveSpeed = GetGameLevelData().enemy_speed
                };
                StartCoroutine(handler_Character.InitCreateCharacter(playerCharacterData, enemyCharacterData, GetGameLevelData().enemy_number));
                //创建船
                Action enemyShipCallBack = () =>
                {
                    //开启敌舰自动攻击
                    handler_Ship.OpenShipFireAutoForEnemy();
                };
                handler_Ship.CreateShip(CharacterTypeEnum.Player, 1, null);
                handler_Ship.CreateShip(CharacterTypeEnum.Enemy, GetGameLevelData().enemy_ship_id, enemyShipCallBack);
                //开启经验计算
                StartCoroutine(CoroutineForLevelProgress());
                break;
            case GameStatusEnum.GameEnd:
                //打开UI
                manager_UI.OpenUIAndCloseOther<UIGameEnd>(UIEnum.GameEnd);
                CleanGameData();
                break;
        }
    }

    /// <summary>
    /// 强制结束游戏
    /// </summary>
    public void ForcedEndGame()
    {
        CleanGameData();
    }

    public void CheckGameOver()
    {
        int goldNumber = handler_Gold.GetGoldNumber();
        if (goldNumber == 0)
        {
            ChangeGameStatus(GameStatusEnum.GameEnd);
        }
    }

    /// <summary>
    /// 获取分数
    /// </summary>
    /// <param name="playerGoldNumber"></param>
    /// <param name="enemyGoldNumber"></param>
    public void GetScore(out long playerGoldNumber, out long enemyGoldNumber)
    {
        playerGoldNumber = 0;
        enemyGoldNumber = 0;
        if (GetGameData() != null)
        {
            playerGoldNumber = GetGameData().playerGoldNumber;
            enemyGoldNumber = GetGameData().enemyGoldNumber;
        }
    }

    /// <summary>
    /// 获取游戏场景等级
    /// </summary>
    /// <returns></returns>
    public void GetGameLevelScene(out float levelPro ,out int level)
    {
        GameBean gameData =  GetGameData();
        levelPro = gameData.levelProgressForScene;
        level = gameData.levelForScene;
    }

    public GameBean GetGameData()
    {
        return manager.GetGameData();
    }

    public void SetGameData(GameBean gameData)
    {
        manager.SetGameData(gameData);
    }

    public GameLevelBean GetGameLevelData()
    {
        return manager.GetGameLevelData();
    }

    public void CleanGameData()
    {
        handler_Character.StopCreateCharacter();
        handler_Character.CleanAllCharacter();
        handler_Ship.CloseShipFireAutoForEnemy();
    }

    public IEnumerator CoroutineForLevelProgress()
    {
        GameBean gameData = GetGameData(); 
        while (GetGameData().gameStatus == GameStatusEnum.GameIng)
        {
            yield return new WaitForSeconds(0.1f);
            float exp = handler_GameData.GetLevelSceneExp(gameData.levelForScene);
            gameData.AddLevelProgressForScene(exp);
        }
    }

    /// <summary>
    /// 延迟加钱
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoroutineForDelayAddGold(float time,long gold)
    {
        yield return new WaitForSeconds(time);
        AddUserGold(gold);
        manager_UI.RefreshAllUI();
    }

    #region 数据回调
    public void GetGameLevelDataSuccess(GameLevelBean gameLevelData, Action<GameLevelBean> action)
    {
        action?.Invoke(gameLevelData);
    }
    #endregion
}