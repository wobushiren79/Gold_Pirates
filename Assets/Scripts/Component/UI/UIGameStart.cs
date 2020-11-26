using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIGameStart : BaseUIComponent, IBaseObserver
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtLifeAdd;

    public Text ui_TvGold;

    public GameDataHandler handler_GameData;

    protected void Start()
    {
        if(handler_GameData)
            handler_GameData.AddObserver(this);
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
        if (ui_TvGold)
            ui_TvGold.text = gold + "";
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