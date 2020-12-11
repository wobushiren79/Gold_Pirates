using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIGameStart : BaseUIComponent, IBaseObserver, UIViewForFireButton.ICallBack
{
    public Button ui_BtSetting;
    public Button ui_BtFire;
    public Button ui_BtAdvertisement;

    public Text ui_TvGold;
    public ProgressView ui_PvLevelUp;

    public UIChildForAttributeAdd ui_ChildAttributeAdd;
    public UIViewForGoldProgress ui_GoldProgress;
    public UIViewForFireButton ui_FireButton;

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
        if (ui_FireButton)
            ui_FireButton.SetCallBack(this);
        if (ui_PvLevelUp)
            ui_PvLevelUp.SetCompleteContent("LEVEL UP");
    }

    private void Update()
    {
        UserDataBean userData = handler_GameData.GetUserData();
        if (userData != null)
            SetGold(userData.gold);
        handler_Game.GetScore(out long playerGold, out long enmeyGold);
        SetScore(playerGold, enmeyGold);
        SetGoldProgress(handler_Gold.GetGoldMaxNumber(), handler_Gold.GetGoldNumber());
        SetLevelUpPro();
    }

    public override void OpenUI()
    {
        base.OpenUI();
        SetFireCD(0f,0f);
    }

    public void SetGold(long gold)
    {
        if (ui_TvGold)
            ui_TvGold.text = gold + "";
    }

    public void SetLevelUpPro()
    {
        ui_PvLevelUp.SetData(1);
    }

    public void SetScore(long playerScore, long enemyScore)
    {
        
    }

    public void SetGoldProgress(int maxGold,int currentGold)
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

    public void OnClickForFire()
    {
        handler_Ship.ShipFire(CharacterTypeEnum.Player);
        //屏幕抖动
        RectTransform rtf = (RectTransform)transform;
        rtf.DOShakeAnchorPos(0.3f,50,30,90,false,true);
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