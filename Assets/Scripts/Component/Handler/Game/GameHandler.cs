using System;
using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameManager>, GameManager.ICallBack
{
    public UIManager manager_UI;
    public GoldHandler handler_Gold;
    public ShipHandler handler_Ship;
    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;

    protected GameBean gameData;
    protected GameLevelBean gameLevelData;

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

    public void AddGold(CharacterTypeEnum characterType, long goldPrice,int goldNumber)
    {
        switch (characterType)
        {
            case CharacterTypeEnum.Player:
                gameData.AddPlayerGold(goldNumber);
                handler_GameData.AddUserGold(goldNumber * (goldPrice + gameData.goldPrice));
                break;
            case CharacterTypeEnum.Enemy:
                gameData.AddEnemyGold(goldNumber);
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
                gameData = new GameBean();
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameStart);  
                //创建金币
                handler_Gold.CreateGold(gameLevelData.gold_number, gameLevelData.gold_id);
                break;
            case GameStatusEnum.GameIng:
                //开启角色创建
                UserDataBean userData= handler_GameData.GetUserData();
                CharacterDataBean playerCharacterData = new CharacterDataBean(CharacterTypeEnum.Player)
                {
                    life = userData.life + gameData.player_life,
                    maxLife = userData.life + gameData.player_life,
                    moveSpeed = userData.speed + gameData.player_speed
                };
                CharacterDataBean enemyCharacterData = new CharacterDataBean(CharacterTypeEnum.Enemy)
                {
                    life = gameLevelData.enemy_life,
                    maxLife = gameLevelData.enemy_life,
                    moveSpeed = gameLevelData.enemy_speed
                };
                StartCoroutine(handler_Character.InitCreateCharacter(playerCharacterData, enemyCharacterData, gameLevelData.enemy_number));
                //创建船
                Action enemyShipCallBack = () =>
                {
                    //开启敌舰自动攻击
                    handler_Ship.OpenShipFireAutoForEnemy();
                };
                handler_Ship.CreateShip(CharacterTypeEnum.Player, 1, null);
                handler_Ship.CreateShip(CharacterTypeEnum.Enemy, gameLevelData.enemy_ship_id, enemyShipCallBack);
                break;
            case GameStatusEnum.GameEnd:
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameEnd);
                CleanGameData();
                break;
        }
        gameData.gameStatus = gameStatus;
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

    public void GetScore(out long playerGold, out long enemyGold)
    {
        playerGold = 0;
        enemyGold = 0;
        if (gameData != null)
        {
            playerGold = gameData.playerGold;
            enemyGold = gameData.enemyGold;
        }
    }

    public GameBean GetGameData()
    {
        return gameData;
    }

    public GameLevelBean GetGameLevelData()
    {
        return gameLevelData;
    }

    public void CleanGameData()
    {
        handler_Character.StopCreateCharacter();
        handler_Character.CleanAllCharacter();
        handler_Ship.CloseShipFireAutoForEnemy();
    }

    #region 数据回调
    public void GetGameLevelDataSuccess(GameLevelBean gameLevelData, Action<GameLevelBean> action)
    {
        this.gameLevelData = gameLevelData;
        action?.Invoke(gameLevelData);
    }
    #endregion
}