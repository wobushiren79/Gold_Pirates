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

    /// <summary>
    /// 获取着陆位置
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public Vector3 GetIslandPosition(CharacterTypeEnum characterType)
    {
        return  manager.GetIslandPosition(characterType);
    }

    /// <summary>
    /// 获取开始位置
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public Vector3 GetStartPosition(CharacterTypeEnum characterType)
    {
        return manager.GetStartPosition(characterType);
    }

    /// <summary>
    /// 获取金币位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetGoldPosition()
    {
        return manager.GetGoldPosition();
    }

    /// <summary>
    /// 开火地点
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public Vector3 GetFirePosition(CharacterTypeEnum characterType)
    {
        return manager.GetFirePosition(characterType);
    }

    /// <summary>
    /// 获取子弹模型
    /// </summary>
    /// <returns></returns>
    public GameObject GetShipBulletModel()
    {
        return manager.GetShipBulletModel();
    }


}