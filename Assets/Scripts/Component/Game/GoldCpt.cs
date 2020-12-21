using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GoldCpt : BaseObservable<CharacterCpt>
{
    public enum NotifyTypeEnum
    {
        HasCarry = 1,
    }

    public GoldDataBean goldData;
    public GoldHandler handler_Gold;
    public ShipHandler handler_Ship;
    public MsgManager manager_Msg;

    public GoldStatusEnum goldStatus = GoldStatusEnum.Idle;
    public CharacterTypeEnum characterType = CharacterTypeEnum.Null;
    private void Awake()
    {
        AutoLinkHandler();
        AutoLinkManager();
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

    public void SetCarry(CharacterTypeEnum characterType, Transform tfHand)
    {
        SetPhysic(false);
        goldStatus = GoldStatusEnum.Carry;
        this.characterType = characterType;
        transform.SetParent(tfHand);
        transform.DOLocalMove(Vector3.zero, 0.5f);
        //transform.localPosition = Vector3.zero;

        //通知所有搬运者 已经有人搬运
        NotifyAllObserver((int)NotifyTypeEnum.HasCarry);
        //然后删除所有想要的搬运者
        RemoveAllObserver();
    }

    public void SetDrop()
    {
        goldStatus = GoldStatusEnum.Drop;
        SetPhysic(true);
        if (handler_Gold)
            transform.SetParent(handler_Gold.transform);
    }

    public void SetRecycle(long addGold, Vector3 recyclePosition)
    {
        goldStatus = GoldStatusEnum.Recycle;
        handler_Gold.RecycleGold(this);
        //SetPhysic(true);
        transform.SetParent(handler_Gold.transform);
        //弹出信息框
        if (addGold > 0)
        {
            Vector2 uiPosition = GameUtil.WorldPointToUILocalPoint(null, manager_Msg.GetContainer(), transform.position + new Vector3(1, 0, 0));
            MsgForGoldView msgView = manager_Msg.ShowMsg<MsgForGoldView>(MsgEnum.Gold, "", uiPosition);
            msgView.SetGold(addGold);
        }
        //抛物线动画
        AnimForRecycle(recyclePosition);
    }

    public void AnimForRecycle(Vector3 targetPosition)
    {
        Vector3[] path = new Vector3[3];
        path[0] = transform.position;
        path[1] = Vector3.Lerp(targetPosition, transform.position, 0.5f) + Vector3.up * 6;
        path[2] = targetPosition;
        transform
            .DOPath(path, 1f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    /// <summary>
    /// 设置物理效果
    /// </summary>
    /// <param name="isOpen"></param>
    public void SetPhysic(bool isOpen)
    {
        if (isOpen)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.isTrigger = false;
        }
        else
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.isTrigger = true;
        }
    }
}