using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class ShipHandler : BaseHandler<ShipManager>, ShipManager.ICallBack
{
    public GameStartSceneHandler handler_Scene;

    protected override void Awake()
    {
        base.Awake();
        manager.SetCallBack(this);
    }

    public void CreateShip(CharacterTypeEnum characterType, long shipId, Action callBack)
    {
        CptUtil.RemoveChild(transform);
        switch (characterType)
        {
            case CharacterTypeEnum.Player:
                manager.GetShipDataById((shipData) =>
                {
                    shipData.characterType = CharacterTypeEnum.Player;
                    StartCoroutine(CoroutineForCreateShip(shipData, callBack));
                }, shipId);
                break;
            case CharacterTypeEnum.Enemy:
                manager.GetShipDataById((shipData) =>
                {
                    shipData.characterType = CharacterTypeEnum.Enemy;
                    StartCoroutine(CoroutineForCreateShip(shipData, callBack));
                }, shipId);
                break;
        }
    }

    /// <summary>
    /// 获取舰队
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public ShipCpt GetShip(CharacterTypeEnum characterType)
    {
        return manager.GetShip(characterType);
    }

    /// <summary>
    /// 舰队开火
    /// </summary>
    /// <param name="characterType"></param>
    public void ShipFire(CharacterTypeEnum characterType)
    {
        ShipCpt shipCpt = manager.GetShip(characterType);
        Vector3 firePosition = handler_Scene.GetFirePosition(characterType);
        shipCpt.OpenFire(firePosition);
    }

    /// <summary>
    /// 敌人自动开火启动
    /// </summary>
    public void OpenShipFireAutoForEnemy()
    {
        ShipCpt shipCpt = manager.GetShip(CharacterTypeEnum.Enemy);
        Vector3 firePosition = handler_Scene.GetFirePosition(CharacterTypeEnum.Enemy);
        StartCoroutine(shipCpt.CoroutineForAutoFire(firePosition));
    }

    /// <summary>
    /// 关闭敌人自动开火
    /// </summary>
    public void CloseShipFireAutoForEnemy()
    {
        ShipCpt shipCpt = manager.GetShip(CharacterTypeEnum.Enemy);
        shipCpt.CloseFire();
    }

    /// <summary>
    /// 改变玩家舰队攻击力
    /// </summary>
    public void ChangePlayerShipDamage(int damage)
    {
        ShipCpt shipCpt = manager.GetShip(CharacterTypeEnum.Player);
        shipCpt.SetDamage(damage);
    }


    protected IEnumerator CoroutineForCreateShip(ShipDataBean shipData, Action callBack)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync("Ship/" + shipData.model_name);
        yield return resourceRequest;
        GameObject objModel = resourceRequest.asset as GameObject;
        Vector3 startPosition = handler_Scene.GetStartPosition(shipData.characterType);
        GameObject objShipBulletModel = handler_Scene.GetShipBulletModel();
        manager.CreateShip(objModel, objShipBulletModel, shipData, startPosition + new Vector3(0, 0, -1));
        Resources.UnloadUnusedAssets();
        callBack?.Invoke();
    }



    #region 数据回调
    public void CreateShipSuccess()
    {

    }

    public void GetShipDataByIdSuccess(Action<ShipDataBean> action, ShipDataBean shipData)
    {
        action(shipData);
    }
    #endregion
}