using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIGameStart : BaseUIComponent, IBaseObserver
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtLifeAdd;

    public Text ui_TvPlayerGold;
    public Text ui_TvEnemyGold;

    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;

    protected void Start()
    {
        if(handler_GameData)
            handler_GameData.AddObserver(this);

        if (ui_BtSpeedAdd)
            ui_BtSpeedAdd.onClick.AddListener(OnClickForAddSpeed);
        if (ui_BtNumberAdd)
            ui_BtNumberAdd.onClick.AddListener(OnClickForAddNumber);
        if (ui_BtLifeAdd)
            ui_BtLifeAdd.onClick.AddListener(OnClickForAddLife);
    }

    public override void RefreshUI()
    {
        base.RefreshUI();
    
    }

    private void Update()
    {
        UserDataBean userData =  handler_GameData.GetUserData();
        if(userData!=null)
           SetGold(userData.gold);
    }

    public void SetGold(long gold)
    {
        if (ui_TvPlayerGold)
            ui_TvPlayerGold.text = gold + "";
    }

    public void OnClickForAddSpeed()
    {
        handler_GameData.AddSpeed(0.5f);
        //刷新玩家角色
        handler_Character.RefreshCharacter();
    }

    public void OnClickForAddNumber()
    {
        handler_GameData.AddPirateNumber(1);
    }

    public void OnClickForAddLife()
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