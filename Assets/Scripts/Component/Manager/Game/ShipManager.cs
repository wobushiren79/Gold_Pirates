using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipManager : BaseManager, IShipDataView
{
    protected ShipDataController shipDataController;

    public ShipCpt shipForPlayer;
    public ShipCpt shipForEnemy;

    protected ICallBack callBack;

    private void Awake()
    {
        shipDataController = new ShipDataController(this, this);
    }

    public void SetCallBack(ICallBack callBack)
    {
        this.callBack = callBack;
    }

    /// <summary>
    /// 通过ID获取主舰数据
    /// </summary>
    /// <param name="id"></param>
    public void GetShipDataById(Action<ShipDataBean> action, long id)
    {
        shipDataController.GetShipDataById(action, id);
    }

    /// <summary>
    /// 创建舰队
    /// </summary>
    /// <param name="goldData"></param>
    public void CreateShip(GameObject objModel,GameObject objBullet, ShipDataBean shipData, Vector3 position,Vector3 startPlayerAngles,Vector3 startEnemyAngles)
    {
        GameObject itemObj = Instantiate(gameObject, objModel, position);
        ShipCpt shipCpt = itemObj.AddComponent<ShipCpt>();
        shipCpt.SetData(shipData, objBullet);
        if (shipData.characterType == CharacterTypeEnum.Player)
        {
            shipForPlayer = shipCpt;
            itemObj.transform.eulerAngles = startPlayerAngles;
        }
        else if (shipData.characterType == CharacterTypeEnum.Enemy)
        {
            shipForEnemy = shipCpt;
            itemObj.transform.eulerAngles = startEnemyAngles;
        }
    }

    public ShipCpt GetShip(CharacterTypeEnum characterType)
    {
        switch (characterType)
        {
            case CharacterTypeEnum.Player:
                return shipForPlayer;
            case CharacterTypeEnum.Enemy:
                return shipForEnemy;
            default:
                break;
        }
        return null;
    }

    #region 数据回调
    public void GetShipDataSuccess(Action<ShipDataBean> action, ShipDataBean shipData)
    {
        if (callBack != null)
            callBack.GetShipDataByIdSuccess(action, shipData);
    }
    public void GetShipDataFail(string failMsg)
    {

    }
    #endregion

    public interface ICallBack
    {
        void GetShipDataByIdSuccess(Action<ShipDataBean> rank, ShipDataBean shipData);

        void CreateShipSuccess();
    }
}