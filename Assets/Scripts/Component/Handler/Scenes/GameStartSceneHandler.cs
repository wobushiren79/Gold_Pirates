using UnityEditor;
using UnityEngine;

public class GameStartSceneHandler : BaseHandler<GameStartSceneManager>
{
    public UIManager manager_UI;
    public GameDataHandler handler_GameData;
    public GoldHandler handler_Gold;
    public GameHandler handler_Game;

    private void Start()
    {
        handler_Game.ChangeGameStatus(GameStatusEnum.GamePre);
        //打开UI
        manager_UI.OpenUI(UIEnum.GameStart);
        //创建金币
        handler_Gold.CreateGold(100, 1);
        
        handler_Game.ChangeGameStatus(GameStatusEnum.GameIng);

        //开启角色创建


    }
}