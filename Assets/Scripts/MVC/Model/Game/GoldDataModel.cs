using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoldDataModel : BaseMVCModel
{
    protected GoldDataService goldDataService;
    public override void InitData()
    {
        goldDataService = new GoldDataService();
    }

    /// <summary>
    /// 通过ID获取金币数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GoldDataBean GetGoldDataById(long id)
    {
         List<GoldDataBean> listData= goldDataService.QueryDataById(id);
        if (listData.Count>0)
        {
            return listData[0];
        }
        else
        {
            return null;
        }
    }
}