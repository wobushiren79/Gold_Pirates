using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class GoldHandler : BaseHandler<GoldManager>, GoldManager.ICallBack
{
    protected int goldNumber = 0;


    public GameStartSceneHandler handler_Scene;
    public UIManager manager_UI;

    protected override void Awake()
    {
        base.Awake();
        manager.SetCallBack(this);
    }

    /// <summary>
    /// 创建金币
    /// </summary>
    /// <param name="number"></param>
    /// <param name="goldId"></param>
    public void CreateGold(string goldPileData, int number, long goldId)
    {
        if (CheckUtil.StringIsNull(goldPileData))
        {
            goldNumber = number;
            manager.GetGoldDataById(goldId);
        }
        else
        {
            StartCoroutine(CoroutineForCreateGoldPile(goldPileData));
        }
    }

    /// <summary>
    /// 获取没有分配的金币
    /// </summary>
    public GoldCpt GetIdleGold()
    {
        return manager.GetGoldByStatus(GoldStatusEnum.Idle);
    }

    /// <summary>
    /// 获取丢弃的金币
    /// </summary>
    public GoldCpt GetDropGold()
    {
        return manager.GetGoldByStatus(GoldStatusEnum.Drop);
    }

    /// <summary>
    /// 获取目标金币
    /// </summary>
    /// <returns></returns>
    public GoldCpt GetTargetGold()
    {
        GoldCpt dropGold = GetDropGold();
        if (dropGold == null)
        {
            return GetIdleGold();
        }
        else
        {
            return dropGold;
        }
    }

    /// <summary>
    /// 获取金币数量
    /// </summary>
    /// <returns></returns>
    public int GetGoldNumber()
    {
        return manager.GetGoldNumber();
    }

    /// <summary>
    /// 获取场景中总体金币数量
    /// </summary>
    /// <returns></returns>
    public int GetGoldMaxNumber()
    {
        return goldNumber;
    }

    /// <summary>
    /// 回收金币
    /// </summary>
    /// <param name="goldCpt"></param>
    public void RecycleGold(GoldCpt goldCpt)
    {
        manager.RemoveGold(goldCpt);
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
        // List<Vector3> listPosition = CalculatePostionForGold(goldNumber);
        BoxCollider boxCollider = objModel.GetComponent<BoxCollider>();
        Vector3 boxSize = new Vector3(boxCollider.size.x * objModel.transform.localScale.x, boxCollider.size.y * objModel.transform.localScale.y, boxCollider.size.z * objModel.transform.localScale.z);
        List<Vector3> listPosition = CalculatePostionForGold(boxSize, 8, 8, goldNumber);
        for (int i = 0; i < listPosition.Count; i++)
        {
            manager.CreateGold(objModel, goldData, listPosition[i]);
            yield return new WaitForEndOfFrame();
        }
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 协程-创建金币堆
    /// </summary>
    /// <param name="goldPileData"></param>
    /// <returns></returns>
    public IEnumerator CoroutineForCreateGoldPile(string goldPileData)
    {
        List<string> listData = StringUtil.SplitBySubstringForListStr(goldPileData, '|');
        for (int i = 0; i < listData.Count; i++)
        {
            string itemData = listData[i];
            string[] itemDataArray = StringUtil.SplitBySubstringForArrayStr(itemData, ':');

            float[] itemPositionArray = StringUtil.SplitBySubstringForArrayFloat(itemDataArray[1], ',');
            ResourceRequest resourceRequest = Resources.LoadAsync("GoldPile/" + itemDataArray[0]);
            yield return resourceRequest;
            GameObject objModel = resourceRequest.asset as GameObject;
            manager.CreateGoldPile(objModel, new Vector3(itemPositionArray[0], itemPositionArray[1], itemPositionArray[2]));
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
        Vector3 goldStartPosition = handler_Scene.GetGoldPosition();

        int layer = 1;
        int layerNumber = 1;
        float offsetX = 0 + goldStartPosition.x;
        float offsetY = 0.25f + goldStartPosition.y;
        float offsetZ = 0 + goldStartPosition.z;
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
                offsetY = 0.25f + goldStartPosition.y;
            }
            if (layerNumber <= 0)
            {
                layer++;
                layerNumber = layer * layer;
                offsetZ += 1f;
                offsetY = 0.25f + goldStartPosition.y;
                offsetX = -0.5f * (layer - 1) + goldStartPosition.x;
            }
        }
        return listData;
    }

    public List<Vector3> CalculatePostionForGold(Vector3 boxSize, int hNumber, int vNumber, int goldNumber)
    {
        List<Vector3> listData = new List<Vector3>();
        Vector3 goldStartPosition = handler_Scene.GetGoldPosition();
        float offsetX = goldStartPosition.x + hNumber / 2f  * boxSize.x;
        float offsetY = boxSize.y / 2f + goldStartPosition.y;
        float offsetZ = goldStartPosition.z + vNumber / 2f * boxSize.z;
        int layer = 0;
        int hTempNumber = 0;
        int vTempNumber = 0;
        for (int i = 0; i < goldNumber; i++)
        {
            listData.Add(new Vector3(offsetX - hTempNumber * boxSize.x , offsetY + layer * boxSize.y , offsetZ - vTempNumber * boxSize.z));
            hTempNumber++;
            if (hTempNumber >= hNumber)
            {
                hTempNumber = 0;
                vTempNumber++;
                if (vTempNumber >= vNumber)
                {
                    hTempNumber = 0;
                    vTempNumber = 0;
                    layer++;
                }
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