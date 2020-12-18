using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIGameStart : BaseUIComponent, IBaseObserver, UIViewForFireButton.ICallBack
{
    public Button ui_BtSetting;
    public Button ui_BtFire;
    public Button ui_BtAdvertisement;
    public Button ui_BtSpeedUp;

    public Text ui_TvGold;
    public Button ui_BtLevelUp;
    public ProgressView ui_PvLevelUp;

    public UIChildForAttributeAdd ui_ChildAttributeAdd;
    public UIViewForGoldProgress ui_GoldProgress;
    public UIViewForFireButton ui_FireButton;
    public UIViewForBattle ui_Battle;

    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;
    public GameHandler handler_Game;
    public ShipHandler handler_Ship;
    public GoldHandler handler_Gold;

    public MsgManger manager_Msg;

    protected void Start()
    {
        if (handler_GameData)
            handler_GameData.AddObserver(this);

        if (ui_BtFire)
            ui_BtFire.onClick.AddListener(OnClickForFire);
        if (ui_BtSetting)
            ui_BtSetting.onClick.AddListener(OnClickForSetting);
        if (ui_BtAdvertisement)
            ui_BtAdvertisement.onClick.AddListener(OnClickForAdvertisement);
        if (ui_FireButton)
            ui_FireButton.SetCallBack(this);
        if (ui_PvLevelUp)
            ui_PvLevelUp.SetCompleteContent(GameCommonInfo.GetUITextById(4));
        if (ui_BtLevelUp)
            ui_BtLevelUp.onClick.AddListener(OnClickForLevelUp);
        if (ui_BtSpeedUp)
            ui_BtSpeedUp.onClick.AddListener(OnClickForSpeedUp);
        RefreshUI();
    }

    private void Update()
    {
        UserDataBean userData = handler_GameData.GetUserData();
        if (userData != null)
        {
            SetGold(userData.gold);
        }
        if (handler_Game != null)
        {
            handler_Game.GetScore(out long playerGold, out long enmeyGold);
            SetScore(playerGold, enmeyGold);
            SetGoldProgress(handler_Gold.GetGoldMaxNumber(), handler_Gold.GetGoldNumber());
            handler_Game.GetGameLevelScene(out float gameLevelSceneProgress, out int gameLevelScene);
            SetLevelUpPro(gameLevelScene, gameLevelSceneProgress);
        }
    }

    public override void RefreshUI()
    {
        base.RefreshUI();
        ui_ChildAttributeAdd.RefreshUI();
        GameBean gameData = handler_Game.GetGameData();
        ui_Battle.SetData(gameData.playerGoldNumber, gameData.enemyGoldNumber);
    }

    public override void OpenUI()
    {
        base.OpenUI();
        SetFireCD(0f, 0f);
    }

    public void SetGold(long gold)
    {
        if (ui_TvGold)
            ui_TvGold.text = gold + "";
    }

    public void SetLevelUpPro(int level, float pro)
    {
        ui_PvLevelUp.SetData("Lv." + level, pro);
    }

    public void SetScore(long playerScore, long enemyScore)
    {

    }

    public void SetGoldProgress(int maxGold, int currentGold)
    {
        ui_GoldProgress.SetData(maxGold, currentGold);
    }

    public void SetFireCD(float maxTime, float time)
    {
        if (time == 0)
        {
            ui_FireButton.ChangeStatus(false);
        }
        else
        {
            ui_FireButton.ChangeStatus(true);
        }
        ui_FireButton.SetTime(maxTime, time);
    }

    public void ShakeUI()
    {
        //屏幕抖动
        RectTransform rtf = (RectTransform)transform;
        rtf.DOKill();
        rtf.DOShakeAnchorPos(0.3f, 50, 30, 90, false, true);
    }

    public void OnClickForFire()
    {
        handler_Ship.ShipFire(CharacterTypeEnum.Player);
        ShakeUI();     
    }


    public void OnClickForSetting()
    {

    }

    public void OnClickForAdvertisement()
    {

    }

    public void OnClickForLevelUp()
    {
        GameBean gameData = handler_Game.GetGameData();
        UserDataBean userData = handler_GameData.GetUserData();
        if (gameData.levelProgressForScene < 1)
            return;
        gameData.LevelUpForScene();
        long addMoney = handler_GameData.GetLevelSceneMoney(gameData.levelForScene);
        userData.AddGold(addMoney);
    }

    public void OnClickForSpeedUp()
    {
        float addSpeed = handler_GameData.GetSpeedUpAddSpeed();
        float time = handler_GameData.GetSpeedUpTime();
        handler_Character.SetCharacterSpeedUp(CharacterTypeEnum.Player, addSpeed, time);
    }

    #region 通知回调
    public void ObserbableUpdate<T>(T observable, int type, params object[] obj) where T : Object
    {
        if (observable as GameDataHandler)
        {

        }
    }
    #endregion

}