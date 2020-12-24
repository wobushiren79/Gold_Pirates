using System;
using UnityEditor;
using UnityEngine;

public interface IGoldDataView 
{
    void GetGoldDataSuccess(GoldDataBean goldData, Action<GoldDataBean> action);

    void GetGoldDataFail(string failMsg);

}