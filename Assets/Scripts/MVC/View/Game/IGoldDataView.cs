using UnityEditor;
using UnityEngine;

public interface IGoldDataView 
{
    void GetGoldDataSuccess(GoldDataBean goldData);

    void GetGoldDataFail(string failMsg);

}