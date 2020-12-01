using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIGameStart : BaseUIComponent, IBaseObserver
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtLifeAdd;
    public Button ui_BtDamageAdd;

    public Text ui_TvSpeedAdd;
    public Text ui_TvNumberAdd;
    public Text ui_TvLifeAdd;
    public Text ui_TvDamageAdd;

    public Button ui_BtFire;

    public Text ui_TvGold;
    public Text ui_TvPlayerGold;
    public Text ui_TvEnemyGold;

    public GameDataHandler handler_GameData;
    public CharacterHandler handler_Character;
    public GameHandler handler_Game;
    public ShipHandler handler_Ship;

    protected void Start()
    {
        if (handler_GameData)
            handler_GameData.AddObserver(this);

        if (ui_BtSpeedAdd)
            ui_BtSpeedAdd.onClick.AddListener(OnClickForAddSpeed);
        if (ui_BtNumberAdd)
            ui_BtNumberAdd.onClick.AddListener(OnClickForAddNumber);
        if (ui_BtLifeAdd)
            ui_BtLifeAdd.onClick.AddListener(OnClickForAddLife);
        if (ui_BtDamageAdd)
            ui_BtDamageAdd.onClick.AddListener(OnClickForAddDamage);

        if (ui_BtFire)
            ui_BtFire.onClick.AddListener(OnClickForFire);
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

        ui_TvSpeedAdd.text = "速度:" + userData.speed;
        ui_TvLifeAdd.text = "生命:"+ userData.life ;
        ui_TvDamageAdd.text = "伤害:"+ userData.damage;
    }

    public void SetGold(long gold)
    {
        if (ui_TvGold)
            ui_TvGold.text = gold + "";
    }

    public void SetScore(long playerScore, long enemyScore)
    {
        if (ui_TvPlayerGold)
        {
            ui_TvPlayerGold.text = playerScore + "";
        }
        if (ui_TvEnemyGold)
        {
            ui_TvEnemyGold.text = enemyScore + "";
        }
    }

    public void OnClickForAddSpeed()
    {
        handler_GameData.AddSpeed(0.5f);
        handler_Character.RefreshCharacter();
    }

    public void OnClickForAddNumber()
    {
        handler_Character.CreateCharacter(CharacterTypeEnum.Player);
    }

    public void OnClickForAddLife()
    {
        int life = handler_GameData.AddLife(1);
        handler_Character.SetPlayerCharacterLife(life);
        handler_Character.RefreshCharacter();
    }

    public void OnClickForAddDamage()
    {
        int damage = handler_GameData.AddDamage(1);
        handler_Ship.ChangePlayerShipDamage(damage);
    }

    public void OnClickForFire()
    {
        handler_Ship.ShipFire(CharacterTypeEnum.Player);
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