using System;
using UnityEditor;
using UnityEngine;

public interface IShipDataView 
{
    void GetShipDataSuccess(Action<ShipDataBean> action, ShipDataBean shipData);

    void GetShipDataFail(string failMsg);

}