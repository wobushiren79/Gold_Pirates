using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIChildForAttributeAdd : BaseUIChildComponent<UIGameStart>
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtGoldPriceAdd;
    public Button ui_BtDamageAdd;

    public Text ui_TvSpeed;
    public Text ui_TvNumber;
    public Text ui_TvGoldPrice;
    public Text ui_TvDamage;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (ui_BtSpeedAdd)
            ui_BtSpeedAdd.onClick.AddListener(OnClickForAddSpeed);
        if (ui_BtNumberAdd)
            ui_BtNumberAdd.onClick.AddListener(OnClickForAddNumber);
        if (ui_BtGoldPriceAdd)
            ui_BtGoldPriceAdd.onClick.AddListener(OnClickForAddGoldPrice);
        if (ui_BtDamageAdd)
            ui_BtDamageAdd.onClick.AddListener(OnClickForAddDamage);
    }

    public void OnClickForAddNumber()
    {
        UserDataBean userData = uiComponent.handler_GameData.GetUserData();
        GameBean gameData= uiComponent.handler_Game.GetGameData();
        CharacterDataBean playerCharacterData = new CharacterDataBean(CharacterTypeEnum.Player)
        {
            life = userData.life+ gameData.player_life,
            maxLife = userData.life + gameData.player_life,
            moveSpeed = userData.speed + gameData.player_speed
        };
        uiComponent.handler_Character.CreateCharacter(playerCharacterData);
    }

    public void OnClickForAddGoldPrice()
    {
        int goldPrice = uiComponent.handler_Game.GetGameData().AddGoldPrice(1);
    }

    public void OnClickForAddLife()
    {
        int life = uiComponent.handler_Game.GetGameData().AddPlayerLife(1);
        uiComponent.handler_Character.SetCharacterLife(CharacterTypeEnum.Player, life);
        uiComponent.handler_Character.RefreshCharacter(CharacterTypeEnum.Player);
    }

    public void OnClickForAddDamage()
    {
        int damage = uiComponent.handler_Game.GetGameData().AddPlayerDamage(1);
        uiComponent.handler_Ship.ChangePlayerShipDamage(damage);
    }

    public void OnClickForAddSpeed()
    {
        float speed = uiComponent.handler_Game.GetGameData().AddPlayerSpeed(0.5f);
        uiComponent.handler_Character.SetCharacterSpeed(CharacterTypeEnum.Player, speed);
        uiComponent.handler_Character.RefreshCharacter(CharacterTypeEnum.Player);
    }

}