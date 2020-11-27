using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGameEnd : BaseUIComponent
{
    public Button ui_BtStartNew;
    public GameHandler handler_Game;
    public void Start()
    {
        if (ui_BtStartNew)
            ui_BtStartNew.onClick.AddListener(OnClickForStartNew);
    }

    public void OnClickForStartNew()
    {
        handler_Game.ChangeGameStatus(GameStatusEnum.GamePre);
        handler_Game.ChangeGameStatus(GameStatusEnum.GameIng);
    }
}