using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GoldCpt : BaseMonoBehaviour
{
    public GoldDataBean goldData;
    public GoldHandler handler_Gold;

    public GoldStatusEnum goldStatus = GoldStatusEnum.Idle;

    private void Awake()
    {
        AutoLinkHandler();
    }

    public void SetData(GoldDataBean goldData)
    {
        this.goldData = goldData;
    }

    public void ChangeGoldStatus(GoldStatusEnum goldStatus)
    {
        this.goldStatus = goldStatus;
    }

    public GoldStatusEnum GetGoldStatus()
    {
        return goldStatus;
    }

    public void SetCarry(Transform tfHand)
    {
        SetPhysic(false);
        goldStatus = GoldStatusEnum.Carry;
        transform.SetParent(tfHand);
        transform.localPosition = Vector3.zero;
    }

    public void SetRecycle()
    {
        handler_Gold.RecycleGold(this);
        SetPhysic(true);
        goldStatus = GoldStatusEnum.Recycle;
        transform.SetParent(handler_Gold.transform);
        StartCoroutine(CoroutineForDestroyGold());
    }

    /// <summary>
    /// 延迟删除金币
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoroutineForDestroyGold()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    /// <summary>
    /// 设置物理效果
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetPhysic(bool isOpen)
    {
        if (isOpen)
        {
            Rigidbody rigidbody = CptUtil.AddCpt<Rigidbody>(gameObject);
        }
        else
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Destroy(rigidbody);
        }
    }

}