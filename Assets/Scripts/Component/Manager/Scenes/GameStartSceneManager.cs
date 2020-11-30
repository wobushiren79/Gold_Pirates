using System;
using UnityEditor;
using UnityEngine;

public class GameStartSceneManager : BaseManager
{
    public Transform transform_ShipBullet;

    public Transform transform_GoldPosition;
    public Transform transform_PlayerStartPosition;
    public Transform transform_EnemyStartPosition;
    public Transform transform_PlayerIslandPosition;
    public Transform transform_EnemyIslandPosition;

    public Transform transform_PlayerFirePosition;
    public Transform transform_EnemyFirePosition;

    private void Awake()
    {
        ReflexUtil.AutoLinkDataForChild(this, "transform_");
    }

    /// <summary>
    /// 获取金币位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetGoldPosition()
    {
        return transform_GoldPosition.position;
    }

    /// <summary>
    /// 获取着陆位置
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public Vector3 GetIslandPosition(CharacterTypeEnum characterType)
    {
        if (characterType == CharacterTypeEnum.Player)
        {
            return transform_PlayerIslandPosition.position;
        }
        else if (characterType == CharacterTypeEnum.Enemy)
        {
            return transform_EnemyIslandPosition.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    /// <summary>
    /// 获取开始位置
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public Vector3 GetStartPosition(CharacterTypeEnum characterType)
    {
        if (characterType == CharacterTypeEnum.Player)
        {
            return transform_PlayerStartPosition.position;
        }
        else if (characterType == CharacterTypeEnum.Enemy)
        {
            return transform_EnemyStartPosition.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    /// <summary>
    /// 获取炮弹模型
    /// </summary>
    /// <returns></returns>
    public GameObject GetShipBulletModel()
    {
        return transform_ShipBullet.gameObject;
    }

    /// <summary>
    /// 获取开火地点
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public Vector3 GetFirePosition(CharacterTypeEnum characterType)
    {
        if (characterType == CharacterTypeEnum.Player)
        {
            return transform_PlayerFirePosition.position;
        }
        else if (characterType == CharacterTypeEnum.Enemy)
        {
            return transform_EnemyFirePosition.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
}