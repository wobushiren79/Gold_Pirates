using System;
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
        StartCoroutine(CoroutineForLevelProgress());
    }


    public void AddGold(CharacterTypeEnum characterType, long goldPrice,int goldNumber)
    {
        switch (characterType)
        {
            case CharacterTypeEnum.Player:
                GetGameData().AddPlayerGoldNumber(goldNumber);
                handler_GameData.AddUserGold(goldNumber * (goldPrice + GetGameData().goldPrice));
                break;
            case CharacterTypeEnum.Enemy:
                GetGameData().AddEnemyGoldNumber(goldNumber);
                break;
        }
    }

    public void InitGameLevelData(int level, Action<GameLevelBean> action)
    {
        manager.GetGameLevelDataByLevel(level, action);
    }

    public void ChangeGameStatus(GameStatusEnum gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatusEnum.GamePre:
                //扫描地形
                //AstarPath.active.ScanAsync();
                //初始化数据
                SetGameData(new GameBean());
                manager_UI.RefreshAllUI();
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameStart); 
                //创建金币
                handler_Gold.CreateGold(GetGameLevelData().gold_number, GetGameLevelData().gold_id);
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
                break;
            case GameStatusEnum.GameEnd:
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameEnd);
                CleanGameData();
                break;
        }
        GetGameData().gameStatus = gameStatus;
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

    #region 数据回调
    public void GetGameLevelDataSuccess(GameLevelBean gameLevelData, Action<GameLevelBean> action)
    {
        action?.Invoke(gameLevelData);
    }
    #endregion
}