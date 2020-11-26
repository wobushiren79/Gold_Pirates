using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameManager>
{
    protected GameBean gameData;


    public void ChangeGameStatus( GameStatusEnum gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatusEnum.GamePre:
                gameData = new GameBean();
               
                break;
            case GameStatusEnum.GameIng:

                break;
            case GameStatusEnum.GameEnd:

                break;
        }

        gameData.gameStatus = gameStatus;
    }
}