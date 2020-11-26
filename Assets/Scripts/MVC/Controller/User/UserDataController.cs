using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataController : BaseMVCController<UserDataModel, IUserDataView>
{
    public UserDataController(BaseMonoBehaviour content, IUserDataView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }
    public void GetUserData()
    {
        UserDataBean userData = GetModel().GetUserData();
        GetView().GetUserDataSuccess(userData);
    }

    public void SaveUserData(UserDataBean userData)
    {
        GetModel().SetUserData(userData);
    }


}
