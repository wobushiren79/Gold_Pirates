﻿using UnityEditor;
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


        if (ui_TvTitleGoldPrice)
        {
            ui_TvTitleGoldPrice.outlineColor = Color.white;
            ui_TvTitleGoldPrice.text = GameCommonInfo.GetUITextById(1);
            SetUnderlayOffsetY(ui_TvTitleGoldPrice);
        }
        if (ui_TvTitleSpeed)
        {
            ui_TvTitleSpeed.outlineColor = Color.white;
            ui_TvTitleSpeed.text = GameCommonInfo.GetUITextById(2);
            SetUnderlayOffsetY(ui_TvTitleSpeed);
        }

        if (ui_TvTitleNumber)
        {
            ui_TvTitleNumber.outlineColor = Color.white;
            ui_TvTitleNumber.text = GameCommonInfo.GetUITextById(3);
            SetUnderlayOffsetY(ui_TvTitleNumber);

        }     
        RefreshUI();
    }

    protected void SetUnderlayOffsetY(TextMeshProUGUI textMeshPro)
    {
        Material fontMaterial= textMeshPro.fontMaterial;
        fontMaterial.SetFloat("_UnderlayOffsetY", -0.8f);
    }

    public void RefreshUI()
    {
        GameBean gameData = uiComponent.handler_Game.GetGameData();
        GameDataHandler gameDataHandler = uiComponent.handler_GameData;
        
        gameDataHandler.GetLevelLevelUpDataForSpeed(gameData.levelForSpeed, out float addSpeed, out long preSpeedGold);
        SetTextForAttribute(ui_TvSpeedLevel, ui_TvSpeedMoney, gameData.levelForSpeed, gameDataHandler.GetLevelMaxForSpeed(), preSpeedGold);
        SetButtonStatusForAttribute(ui_BtSpeedAdd, preSpeedGold);

        gameDataHandler.GetLevelLevelUpDataForNumber(gameData.levelForPirateNumber, out int addNumber, out long preNumberGold);
        SetTextForAttribute(ui_TvNumberLevel, ui_TvNumberMoney, gameData.levelForPirateNumber, gameDataHandler.GetLevelMaxForNumber(), preNumberGold);
        SetButtonStatusForAttribute(ui_BtNumberAdd, preNumberGold);

        gameDataHandler.GetLevelUpDataForGoldPrice(gameData.levelForGoldPrice, out int addPrice, out long prePriceGold);
        SetTextForAttribute(ui_TvGoldPriceLevel, ui_TvGoldPriceMoney, gameData.levelForGoldPrice, gameDataHandler.GetLevelMaxForGoldPrice(), prePriceGold);
        SetButtonStatusForAttribute(ui_BtGoldPriceAdd, prePriceGold);
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
            tvLevelMoney.text = levelUpMoney.FormatKM() + "";
            tvLevelMoney.outlineColor = color_MoneyOutline;
        }
         
    }

    public void SetButtonStatusForAttribute(Button btAttribute, long levelUpMoney)
    {
        GameBean gameData = uiComponent.handler_Game.GetGameData();
        if (gameData.HasEnoughGold(levelUpMoney))
        {
            btAttribute.interactable = true;
        }
        else
        {
            btAttribute.interactable = false;
        }
    }

    public void OnClickForAddNumber()
    {
        GameBean gameData = uiComponent.handler_Game.GetGameData();
        GameDataHandler gameDataHandler = uiComponent.handler_GameData;
        UserDataBean userData = gameDataHandler.GetUserData();
 
        int maxLevel = uiComponent.handler_GameData.GetLevelMaxForNumber();
        gameDataHandler.GetLevelLevelUpDataForNumber(gameData.levelForPirateNumber, out int addNumber, out long preNumberGold);

        if (!gameData.HasEnoughGold(preNumberGold))
        {
            //钱不够
            uiComponent.manager_Msg.ShowMsg(GameCommonInfo.GetUITextById(1001));
            return;
        }
        bool isLevelUp = gameData.LevelUpForPlayerPirateNumber(maxLevel, addNumber);
        if (!isLevelUp)
        {
            //升级失败
            uiComponent.manager_Msg.ShowMsg(GameCommonInfo.GetUITextById(1002));
            return;
        }
        //支付金币
        gameData.PayGold(preNumberGold);
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
        GameDataHandler gameDataHandler = uiComponent.handler_GameData;
        GameBean gameData = uiComponent.handler_Game.GetGameData();

        int maxLevel = uiComponent.handler_GameData.GetLevelMaxForGoldPrice();
        gameDataHandler.GetLevelUpDataForGoldPrice(gameData.levelForGoldPrice, out int addPrice, out long prePriceGold);
        if (!gameData.HasEnoughGold(prePriceGold))
        {
            //钱不够
            uiComponent.manager_Msg.ShowMsg(GameCommonInfo.GetUITextById(1001));
            return;
        }

        bool isLevelUp = gameData.LevelUpForGoldPrice(maxLevel, addPrice);
        if (!isLevelUp)
        {
            //升级失败
            uiComponent.manager_Msg.ShowMsg(GameCommonInfo.GetUITextById(1002));
            return;
        }

        //支付金币
        gameData.PayGold(prePriceGold);

        RefreshUI();
    }

    public void OnClickForAddSpeed()
    {
        GameDataHandler gameDataHandler = uiComponent.handler_GameData;
        GameBean gameData = uiComponent.handler_Game.GetGameData();
        UserDataBean userData= gameDataHandler.GetUserData();
        int maxLevel = uiComponent.handler_GameData.GetLevelMaxForSpeed();
        gameDataHandler.GetLevelLevelUpDataForSpeed(gameData.levelForSpeed, out float addSpeed, out long preSpeedGold);
        if (!gameData.HasEnoughGold(preSpeedGold))
        {
            //钱不够
            uiComponent.manager_Msg.ShowMsg(GameCommonInfo.GetUITextById(1001));
            return;
        }

        bool isLevelUp = gameData.LevelUpForPlayerSpeed(maxLevel, addSpeed);
        if (!isLevelUp)
        {
            //升级失败
            uiComponent.manager_Msg.ShowMsg(GameCommonInfo.GetUITextById(1002));
            return;
        }

        //支付金币
        gameData.PayGold(preSpeedGold);

        uiComponent.handler_Character.SetCharacterSpeed(CharacterTypeEnum.Player, gameData.GetPlayerSpeed() + userData.speed);
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