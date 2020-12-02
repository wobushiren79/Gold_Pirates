using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class GoldManager : BaseManager, IGoldDataView
{
    protected GoldDataController goldDataController;

    public List<GoldCpt> listGold = new List<GoldCpt>();

    protected ICallBack callBack;

    private void Awake()
    {
        goldDataController = new GoldDataController(this, this);
    }

    public void SetCallBack(ICallBack callBack)
    {
        this.callBack = callBack;
    }

    /// <summary>
    /// 通过ID获取金币数据
    /// </summary>
    /// <param name="id"></param>
    public void GetGoldDataById(long id)
    {
        goldDataController.GetGoldDataById(id);
    }

    /// <summary>
    /// 创建金币
    /// </summary>
    /// <param name="goldData"></param>
    public void CreateGold(GameObject objModel, GoldDataBean goldData, Vector3 position)
    {
        GameObject itemObj = Instantiate(gameObject, objModel, position);
        GoldCpt goldCpt = itemObj.GetComponent<GoldCpt>();
        goldCpt.SetData(goldData);
        if (listGold == null)
            listGold = new List<GoldCpt>();
        listGold.Add(goldCpt);
    }

    /// <summary>
    /// 移除金币
    /// </summary>
    /// <param name="goldCpt"></param>
    public void RemoveGold(GoldCpt goldCpt)
    {
        if (goldCpt != null)
            listGold.Remove(goldCpt);
    }

    /// <summary>
    /// 获取闲置的金币
    /// </summary>
    /// <returns></returns>
    public GoldCpt GetGoldByStatus( GoldStatusEnum goldStatus)
    {
        if (CheckUtil.ListIsNull(listGold))
        {
            return null;
        }
        List<GoldCpt> listTemp = new List<GoldCpt>();
        for (int i = 0; i < listGold.Count; i++)
        {
            GoldCpt itemGold = listGold[i];
            if (itemGold.GetGoldStatus() == goldStatus)
            {
                listTemp.Add(itemGold);
            }
        }
        return RandomUtil.GetRandomDataByList(listTemp);
    }

    /// <summary>
    /// 获取场景中的金币数量
    /// </summary>
    /// <returns></returns>
    public int GetGoldNumber()
    {
        return listGold.Count;
    }


    #region 金币数据回掉
    public void GetGoldDataSuccess(GoldDataBean goldData)
    {
        if (callBack != null)
            callBack.GetGoldDataByIdSuccess(goldData);
    }

    public void GetGoldDataFail(string failMsg)
    {

    }

    #endregion

    public interface ICallBack
    {
        void GetGoldDataByIdSuccess(GoldDataBean goldData);

        void CreateGoldSuccess();
    }
}