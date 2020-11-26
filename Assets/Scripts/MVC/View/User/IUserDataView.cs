using UnityEditor;
using UnityEngine;

public interface IUserDataView
{

    void GetUserDataSuccess(UserDataBean userData);

    void GetUserDataFail(string failMsg);

}