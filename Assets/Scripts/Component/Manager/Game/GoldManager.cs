using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class GoldManager : BaseManager, IGoldDataView
{
    protected GoldDataController goldDataController;

    public List<GoldCpt> listGold = new List<GoldCpt>();
    public int goldMaxNumber = 0;


    private void Awake()
    {
        goldDataController = new GoldDataController(this, this);
    }

    /// <summary>
    /// 通过ID获取金币数据
    /// </summary>
    /// <param name="id"></param>
    public void GetGoldDataById(long id, Action<GoldDataBean> action)
    {
        goldDataController.GetGoldDataById(id,action);
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
    /// 创建金币堆
    /// </summary>
    /// <param name="objModel"></param>
    /// <param name="position"></param>
    public void CreateGoldPile(GoldDataBean goldData, GameObject objModel, Vector3 position)
    {
        GameObject itemObj = Instantiate(gameObject, objModel, position);
        GoldCpt[] listData = itemObj.GetComponentsInChildren<GoldCpt>();
        if (listGold == null)
            listGold = new List<GoldCpt>();
        for (int i = 0; i < listData.Length; i++)
        {
            GoldCpt itemGold = listData[i];
            itemGold.goldData.gold_price = goldData.gold_price;
            itemGold.transform.SetParent(gameObject.transform);
            listGold.Add(itemGold);
        }
        goldMaxNumber += listData.Length;
        Destroy(itemObj);
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
    public GoldCpt GetGoldByStatus(GoldStatusEnum goldStatus)
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
    public GoldCpt GetCloseGoldByStatus(Vector3 position, GoldStatusEnum goldStatus)
    {
        if (CheckUtil.ListIsNull(listGold))
        {
            return null;
        }
        float minDistance = float.MaxValue;
        GoldCpt minGold = null;
        for (int i = 0; i < listGold.Count; i++)
        {
            GoldCpt itemGold = listGold[i];
            if (itemGold.GetGoldStatus() == goldStatus)
            {
                float tempDistance = Vector3.Distance(position, itemGold.transform.position);
                if(tempDistance < minDistance)
                {
                    minDistance = tempDistance;
                    minGold = itemGold;
                }
            }
        }
        return minGold;
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
    public void GetGoldDataSuccess(GoldDataBean goldData, Action<GoldDataBean> action)
    {
        if(action!=null)
            action?.Invoke(goldData);
    }

    public void GetGoldDataFail(string failMsg)
    {

    }

    #endregion
}