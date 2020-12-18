using System;
using UnityEditor;
using UnityEngine;

public class ShipAnimCpt : BaseMonoBehaviour
{
    protected Animator shipAnimator;
    protected Action actionAfter;

    private void Update()
    {
        HandlerForAnimtorAfter();
    }

    public void InitAnim()
    {
        shipAnimator = GetComponentInChildren<Animator>();
        SetShipIdle();
    }


    /// <summary>
    /// 动画结束监听处理
    /// </summary>
    public void HandlerForAnimtorAfter()
    {
        if (actionAfter != null)
        {
            AnimatorStateInfo animationState = shipAnimator.GetCurrentAnimatorStateInfo(0);
            if (animationState.normalizedTime >= 1f)
            {
                actionAfter?.Invoke();
                actionAfter = null;
            }
        }
    }

    public void SetShipFire(Action actionAfter)
    {
        PlayAnim("Fire");
        this.actionAfter = actionAfter;
    }

    public void SetShipIdle()
    {
        PlayAnim("Idle");
    }

    public void PlayAnim(string stateName)
    {
        shipAnimator.Play(stateName, 0, 0);
        //如果不加update(0)有时候动画会不重置
        shipAnimator.Update(0);
    }
}