using UnityEditor;
using UnityEngine;

public class GoldDataController : BaseMVCController<GoldDataModel, IGoldDataView>
{
    public GoldDataController(BaseMonoBehaviour content, IGoldDataView view) : base(content, view)
    {

    }

    public override void InitData()
    {

    }

    public void GetGoldDataById(long id)
    {
        GoldDataBean goldData =  GetModel().GetGoldDataById(id);
        if (goldData == null)
        {
            GetView().GetGoldDataFail("没有数据");
        }
        else
        {
            GetView().GetGoldDataSuccess(goldData);
        }
        
    }
}