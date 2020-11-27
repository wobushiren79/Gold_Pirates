using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameManager>
{
    public UIManager manager_UI;
    public GoldHandler handler_Gold;
    public CharacterHandler handler_Character;

    protected GameBean gameData;
 
    public void ChangeGameStatus(GameStatusEnum gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatusEnum.GamePre:
                gameData = new GameBean();
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameStart);
                //创建金币
                handler_Gold.CreateGold(2, 1);
                break;
            case GameStatusEnum.GameIng:
                //开启角色创建
                handler_Character.StartCreateCharacter();
                break;
            case GameStatusEnum.GameEnd:
                //打开UI
                manager_UI.OpenUIAndCloseOther(UIEnum.GameEnd);
                handler_Character.StopCreateCharacter();
                handler_Character.CleanAllCharacter();
                break;
        }
        gameData.gameStatus = gameStatus;
    }

    public void CheckGameOver()
    {
        int goldNumber =  handler_Gold.GetGoldNumber();
        if (goldNumber == 0)
        {
            ChangeGameStatus(GameStatusEnum.GameEnd);
        }
    }

}