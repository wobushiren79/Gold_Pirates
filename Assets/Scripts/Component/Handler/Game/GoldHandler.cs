using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GoldHandler : BaseHandler<GoldManager>, GoldManager.ICallBack
{
    protected int goldNumber = 0;
    protected override void Awake()
    {
        base.Awake();
        manager.SetCallBack(this);
    }

    public void CreateGold(int number, long goldId)
    {
        goldNumber = number;
        manager.GetGoldDataById(goldId);
    }

    /// <summary>
    /// 携程 创建金币
    /// </summary>
    /// <param name="goldData"></param>
    /// <returns></returns>
    public IEnumerator CoroutineForCreateGold(GoldDataBean goldData)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync("Gold/" + goldData.model_name);
        yield return resourceRequest;
        GameObject objModel = resourceRequest.asset as GameObject;

        //计算每个金币的坐标
        List<Vector3> listPosition = CalculatePostionForGold(goldNumber);
        for (int i = 0; i < listPosition.Count; i++)
        {
            manager.CreateGold(objModel, goldData, listPosition[i]);
            yield return new WaitForEndOfFrame();
        }
        Resources.UnloadUnusedAssets();
    }


    /// <summary>
    /// 计算金币位置
    /// </summary>
    /// <param name="goldNumber"></param>
    /// <returns></returns>
    public List<Vector3> CalculatePostionForGold(int goldNumber)
    {
        List<Vector3> listData = new List<Vector3>();
        int layer = 1;
        int layerNumber = 1;
        float offsetX = 0;
        float offsetY = 0.25f;
        float offsetZ = 0;
        for (int i = 0; i < goldNumber; i++)
        {
            listData.Add(new Vector3(offsetX, offsetY, offsetZ));

            layerNumber--;
  
            if (layerNumber % layer == 0)
            {
                offsetX += 1f;
            }
            offsetY += 0.5f;
            if (offsetY > layer * 0.5f) 
            {
                offsetY = 0.25f;
            }
            if (layerNumber <= 0)
            {
                layer++;
                layerNumber = layer * layer;
                offsetZ += 1f;
                offsetY = 0.25f;
                offsetX = -0.5f * (layer-1);
            }
        }
        return listData;
    }

    #region 数据回掉
    public void GetGoldDataByIdSuccess(GoldDataBean goldData)
    {
        StartCoroutine(CoroutineForCreateGold(goldData));
    }

    public void CreateGoldSuccess()
    {

    }
    #endregion
}