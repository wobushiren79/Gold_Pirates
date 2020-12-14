using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIChildForAttributeAdd : BaseUIChildComponent<UIGameStart>
{
    public Button ui_BtSpeedAdd;
    public Button ui_BtNumberAdd;
    public Button ui_BtGoldPriceAdd;
    public Button ui_BtDamageAdd;

    public TextMeshProUGUI ui_TvTitleSpeed;
    public Text ui_TvSpeedLevel;
    public TextMeshProUGUI ui_TvSpeedMoney;

    public TextMeshProUGUI ui_TvTitleNumber;
    public Text ui_TvNumberLevel;
    public TextMeshProUGUI ui_TvNumberMoney;

    public TextMeshProUGUI ui_TvTitleGoldPrice;
    public Text ui_TvGoldPriceLevel;
    public TextMeshProUGUI ui_TvGoldPriceMoney;


    public Color color_MoneyOutline;
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

        if (ui_TvTitleSpeed)
            ui_TvTitleSpeed.outlineColor = Color.white;
        if (ui_TvTitleNumber)
            ui_TvTitleNumber.outlineColor = Color.white;
        if (ui_TvTitleGoldPrice)
            ui_TvTitleGoldPrice.outlineColor = Color.white;
        RefreshUI();
    }

    public void RefreshUI()
    {
        GameBean gameData = uiComponent.handler_Game.GetGameData();
        GameDataHandler gameDataHandler = uiComponent.handler_GameData;
        SetTextForAttribute(ui_TvSpeedLevel, ui_TvSpeedMoney, gameData.levelForSpeed, gameDataHandler.GetLevelMaxForSpeed(), gameDataHandler.GetLevelMoneyForSpeed(gameData.levelForSpeed));
        SetTextForAttribute(ui_TvNumberLevel, ui_TvNumberMoney, gameData.levelForPirateNumber, gameDataHandler.GetLevelMaxForNumber(), gameDataHandler.GetLevelMoneyForNumber(gameData.levelForPirateNumber));
        SetTextForAttribute(ui_TvGoldPriceLevel, ui_TvGoldPriceMoney, gameData.levelForGoldPrice, gameDataHandler.GetLevelMaxForGoldPrice(), gameDataHandler.GetLevelMoneyForGoldPrice(gameData.levelForGoldPrice));
    }

    public void SetTextForAttribute(Text tvLevel, TextMeshProUGUI tvLevelMoney, int level, int maxLevel, long levelUpMoney)
    {
        if (tvLevel)
        {
            if (level == maxLevel)
            {
                tvLevel.text = "Lv.Max";
            }
            else
            {
                tvLevel.text = "Lv." + level;
            }
        }
        if (tvLevelMoney)
        {
            tvLevelMoney.text = levelUpMoney + "";
            tvLevelMoney.outlineColor = color_MoneyOutline;
        }
         
    }

    public void OnClickForAddNumber()
    {
        GameBean gameData = uiComponent.handler_Game.GetGameData();
        UserDataBean userData = uiComponent.handler_GameData.GetUserData();

        int maxLevel = uiComponent.handler_GameData.GetLevelMaxForNumber();
        int addNumber = uiComponent.handler_GameData.GetLevelAddForNumber();
        long levelMoney = uiComponent.handler_GameData.GetLevelMoneyForNumber(gameData.levelForPirateNumber);

        if (!userData.HasEnoughGold(levelMoney))
        {
            //钱不够
            return;
        }
        bool isLevelUp = gameData.LevelUpForPlayerPirateNumber(maxLevel, addNumber);
        if (!isLevelUp)
        {
            //升级失败
            return;
        }
        //支付金币
        userData.PayGold(levelMoney);
        //生成海盗
        for (int i = 0; i < addNumber; i++)
        {
            CharacterDataBean playerCharacterData = new CharacterDataBean(CharacterTypeEnum.Player)
            {
                life = userData.life + gameData.playerForLife,
                maxLife = userData.life + gameData.playerForLife,
                moveSpeed = userData.speed + gameData.GetPlayerSpeed()
            };
            uiComponent.handler_Character.CreateCharacter(playerCharacterData);
        }
        RefreshUI();
    }

    public void OnClickForAddGoldPrice()
    {
        UserDataBean userData = uiComponent.handler_GameData.GetUserData();
        GameBean gameData = uiComponent.handler_Game.GetGameData();

        int maxLevel = uiComponent.handler_GameData.GetLevelMaxForGoldPrice();
        int addGoldPrice = uiComponent.handler_GameData.GetLevelAddForGoldPrice();
        long levelMoney = uiComponent.handler_GameData.GetLevelMoneyForNumber(gameData.levelForGoldPrice);
        if (!userData.HasEnoughGold(levelMoney))
        {
            //钱不够
            return;
        }

        bool isLevelUp = uiComponent.handler_Game.GetGameData().LevelUpForGoldPrice(maxLevel, addGoldPrice);
        if (!isLevelUp)
        {
            //升级失败
            return;
        }        

        //支付金币
        userData.PayGold(levelMoney);

        RefreshUI();
    }

    public void OnClickForAddSpeed()
    {
        UserDataBean userData = uiComponent.handler_GameData.GetUserData();
        GameBean gameData = uiComponent.handler_Game.GetGameData();

        int maxLevel = uiComponent.handler_GameData.GetLevelMaxForSpeed();
        float addSpeed = uiComponent.handler_GameData.GetLevelAddForSpeed();
        long levelMoney = uiComponent.handler_GameData.GetLevelMoneyForNumber(gameData.levelForSpeed);
        if (!userData.HasEnoughGold(levelMoney))
        {
            //钱不够
            return;
        }

        bool isLevelUp = gameData.LevelUpForPlayerSpeed(maxLevel, addSpeed);
        if (!isLevelUp)
        {
            //升级失败
            return;
        }

        //支付金币
        userData.PayGold(levelMoney);

        uiComponent.handler_Character.SetCharacterSpeed(CharacterTypeEnum.Player, gameData.GetPlayerSpeed());
        uiComponent.handler_Character.RefreshCharacter(CharacterTypeEnum.Player);
        RefreshUI();
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



}