using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoldDataService : BaseMVCService
{

    public GoldDataService() : base("gold_type", "gold_type_details_" + GameCommonInfo.GameConfig.language)
    {

    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<GoldDataBean> QueryDataById(long id)
    {
        return base.BaseQueryData<GoldDataBean>("id", id + "");
    }


}