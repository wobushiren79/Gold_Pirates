using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class DialogForGodView : DialogView
{
    public InputField ui_EtGold;
    public Button ui_BtGold;

    public InputField ui_EtEnemySpeed;
    public Button ui_BtEnemySpeed;

    public Button ui_BtEnemyNumber;

    public GameHandler handler_Game;
    public CharacterHandler handler_Character;
    public override void Awake()
    {
        base.Awake();
        AutoLinkUI();
        AutoLinkHandler();

        ui_BtGold.onClick.AddListener(OnClickForGold);
        ui_BtEnemySpeed.onClick.AddListener(OnClickForEnemySpeed);
        ui_BtEnemyNumber.onClick.AddListener(OnClickForEnemyNumber);
    }


    public void OnClickForGold()
    {
        if (long.TryParse(ui_EtGold.text, out long data)) {
            handler_Game.AddUserGold(data);
        }
    }
    public void OnClickForEnemySpeed()
    {
        if (float.TryParse(ui_EtEnemySpeed.text, out float data))
        {
            handler_Character.AddCharacterSpeed(CharacterTypeEnum.Enemy, data);
        }
    }

    public void OnClickForEnemyNumber()
    {
        List<CharacterCpt> listData = handler_Character.GetCharacter(CharacterTypeEnum.Enemy);
        if (CheckUtil.ListIsNull(listData))
            return;
        CharacterDataBean enemyCharacterData = listData[0].GetCharacterData().Clone();
        handler_Character.CreateCharacter(enemyCharacterData);
    }
}