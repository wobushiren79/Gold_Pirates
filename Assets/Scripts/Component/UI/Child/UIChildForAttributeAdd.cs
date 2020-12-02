using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UIChildForAttributeAdd : BaseUIChildComponent<UIGameStart>
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtLifeAdd;
    public Button ui_BtDamageAdd;

    public Text ui_TvSpeed;
    public Text ui_TvNumber;
    public Text ui_TvLife;
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
        if (ui_BtLifeAdd)
            ui_BtLifeAdd.onClick.AddListener(OnClickForAddLife);
        if (ui_BtDamageAdd)
            ui_BtDamageAdd.onClick.AddListener(OnClickForAddDamage);
    }

    public void OnClickForAddNumber()
    {
        CharacterDataBean playerCharacterData = new CharacterDataBean(CharacterTypeEnum.Player);
        uiComponent.handler_Character.CreateCharacter(playerCharacterData);
    }

    public void OnClickForAddLife()
    {
        int life = uiComponent.handler_GameData.AddLife(1);
        uiComponent.handler_Character.SetPlayerCharacterLife(life);
        uiComponent.handler_Character.RefreshCharacter(CharacterTypeEnum.Player);
    }

    public void OnClickForAddDamage()
    {
        int damage = uiComponent.handler_GameData.AddDamage(1);
        uiComponent.handler_Ship.ChangePlayerShipDamage(damage);
    }

    public void OnClickForAddSpeed()
    {
        uiComponent.handler_GameData.AddSpeed(0.5f);
        uiComponent.handler_Character.RefreshCharacter(CharacterTypeEnum.Player);
    }

}