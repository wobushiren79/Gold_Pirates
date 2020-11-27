using UnityEditor;
using UnityEngine;

public class GameStartSceneHandler : BaseHandler<GameStartSceneManager>
{

    public GameDataHandler handler_GameData;

    public GameHandler handler_Game;


    private void Start()
    {
        handler_Game.ChangeGameStatus(GameStatusEnum.GamePre);

        handler_Game.ChangeGameStatus(GameStatusEnum.GameIng);


    }
}