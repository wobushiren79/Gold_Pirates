using System;
using UnityEditor;
using UnityEngine;

public class ShipDataController : BaseMVCController<ShipDataModel,IShipDataView>
{
    public ShipDataController(BaseMonoBehaviour content, IShipDataView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    public void GetShipDataById(Action<ShipDataBean> action, long id)
    {
        ShipDataBean shipData = GetModel().GetShipDataById(id);
        if (shipData == null)
        {
            GetView().GetShipDataFail("没有数据");
        }
        else
        {
            GetView().GetShipDataSuccess(action, shipData);
        }
    }
}