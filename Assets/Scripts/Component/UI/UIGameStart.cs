using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIGameStart : BaseUIComponent, IBaseObserver
{
    public Button ui_BtSetting;
    public Button ui_BtFire;
    public Button ui_BtAdvertisement;

    public Text ui_TvGold;

    public UIChildForAttributeAdd ui_ChildAttributeAdd;
    public UIViewForGoldProgress ui_GoldProgress;

    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;
    public GameHandler handler_Game;
    public ShipHandler handler_Ship;
    public GoldHandler handler_Gold;

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
    }

    public override void RefreshUI()
    {
        base.RefreshUI();
    }

    private void Update()
    {
        UserDataBean userData = handler_GameData.GetUserData();
        if (userData != null)
            SetGold(userData.gold);
        handler_Game.GetScore(out long playerGold, out long enmeyGold);
        SetScore(playerGold, enmeyGold);
        SetGoldProgress(handler_Gold.GetGoldMaxNumber(), handler_Gold.GetGoldNumber());
    }

    public void SetGold(long gold)
    {
        if (ui_TvGold)
            ui_TvGold.text = gold + "";
    }

    public void SetScore(long playerScore, long enemyScore)
    {
        
    }

    public void SetGoldProgress(int maxGold,int currentGold)
    {
        ui_GoldProgress.SetData(maxGold, currentGold);
    }

    public void SetFireCD(int time)
    {
        LogUtil.Log("Fire CD：" + time);
    }

    public void OnClickForFire()
    {
        handler_Ship.ShipFire(CharacterTypeEnum.Player);
    }

    public void OnClickForSetting()
    {

    }

    public void OnClickForAdvertisement()
    {

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