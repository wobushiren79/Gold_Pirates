using System.Collections.Generic;

public class ShipDataModel : BaseMVCModel
{
    protected ShipDataService shipDataService;

    public override void InitData()
    {
        shipDataService = new ShipDataService();
    }

    /// <summary>
    /// 通过ID获取主舰数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ShipDataBean GetShipDataById(long id)
    {
        List<ShipDataBean> listData = shipDataService.QueryDataById(id);
        if (listData.Count > 0)
        {
            return listData[0];
        }
        else
        {
            return null;
        }
    }
}