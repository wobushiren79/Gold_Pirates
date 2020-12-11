using System;
using UnityEditor;
using UnityEngine;

public class CharacterAnimCpt : BaseMonoBehaviour
{
    public Animator characterAnim;

    public AnimatorStateInfo animationState;
    //动作之前
    public Action actionBefore;
    //动作之后
    public Action actionAfter;
    private void Update()
    {
        HandlerForAnimtorAfter();
    }

    /// <summary>
    /// 动画结束监听处理
    /// </summary>
    public void HandlerForAnimtorAfter()
    {
        if (actionAfter != null)
        {
            AnimatorStateInfo animationState = characterAnim.GetCurrentAnimatorStateInfo(0);
            if (animationState.normalizedTime >= 1f)
            {
                actionAfter?.Invoke();
                actionAfter = null;
                actionBefore = null;
            }
        }
    }

    public void InitAnim()
    {
        characterAnim = GetComponentInChildren<Animator>();
    }

    public void SetCharacterStand()
    {
        PlayAnim("stand");
    }

    public void SetCharacterRun()
    {
        PlayAnim("run");
    }

    public void SetCharacterWalk()
    {
        PlayAnim("walk");
    }

    public void SetCharacterThrow(Action actionBefore, Action actionAfter)
    {
        this.actionBefore = actionBefore;
        this.actionAfter = actionAfter;
        PlayAnim("throw");
    }

    public void SetCharacterRow()
    {
        //PlayAnim("row");
    }

    public void PlayAnim(string stateName)
    {
        actionBefore?.Invoke();
        characterAnim.Play(stateName);
    }
}