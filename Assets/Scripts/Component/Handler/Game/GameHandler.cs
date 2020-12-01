using System;
using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameManager>
{
    public UIManager manager_UI;
    public GoldHandler handler_Gold;
    public ShipHandler handler_Ship;
    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;

    protected GameBean gameData;

    public void AddGold(CharacterTypeEnum characterType,long gold)
    {
        switch (characterType)
        {
            case CharacterTypeEnum.Player:
                gameData.AddPlayerGold(gold);
                handler_GameData.AddUserGold(gold);
                break;
            case CharacterTypeEnum.Enemy:
                gameData.AddEnemyGold(gold);
                break;
        }
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
                handler_Gold.CreateGold(1000, 1);
                break;
            case GameStatusEnum.GameIng:
                //开启角色创建
                StartCoroutine(handler_Character.InitCreateCharacter());
                //创建船
                Action enemyShipCallBack = () =>
                {
                    //开启敌舰自动攻击
                    //handler_Ship.OpenShipFireAutoForEnemy();
                };
                handler_Ship.CreateShip(CharacterTypeEnum.Player, 1, null);
                handler_Ship.CreateShip(CharacterTypeEnum.Enemy, 1, enemyShipCallBack);
                break;
            case GameStatusEnum.GameEnd:
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameEnd);
                handler_Character.StopCreateCharacter();
                handler_Character.CleanAllCharacter();
                break;
        }
        gameData.gameStatus = gameStatus;
    }

    public void CheckGameOver()
    {
        int goldNumber = handler_Gold.GetGoldNumber();
        if (goldNumber == 0)
        {
            ChangeGameStatus(GameStatusEnum.GameEnd);
        }
    }

    public void GetScore(out long playerGold,out long enemyGold)
    {
        playerGold = 0;
        enemyGold = 0;
        if (gameData != null)
        {
            playerGold = gameData.playerGold;
            enemyGold = gameData.enemyGold;
        }
    }

}