using System.Collections.Generic;

public class ShipDataService : BaseMVCService
{
    public ShipDataService() : base("ship_type", "ship_type_details_" + GameCommonInfo.GameConfig.language)
    {

    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<ShipDataBean> QueryDataById(long id)
    {
        return base.BaseQueryData<ShipDataBean>("id", id + "");
    }


}