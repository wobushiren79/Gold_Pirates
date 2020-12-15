using System;
using UnityEditor;
using UnityEngine;

public class CharacterAnimCpt : BaseMonoBehaviour
{
    public Animator characterAnim;
    public Transform tf_Row_L;
    public Transform tf_Row_R;

    public AnimatorStateInfo animationState;
    //动作之前
    public Action actionBefore;
    //动作之后
    public Action actionAfter;

    private void Awake()
    {
        ReflexUtil.AutoLinkDataForChild(this, "tf_");
    }

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
        tf_Row_L.gameObject.SetActive(false);
        tf_Row_R.gameObject.SetActive(false);
        PlayAnim("Run");
    }

    public void SetCharacterRun()
    {
        tf_Row_L.gameObject.SetActive(false);
        tf_Row_R.gameObject.SetActive(false);
        PlayAnim("Run");
    }

    public void SetCharacterCarryTop()
    {
        tf_Row_L.gameObject.SetActive(false);
        tf_Row_R.gameObject.SetActive(false);
        PlayAnim("CarryTop");
    }

    public void SetCharacterCarryFront()
    {
        tf_Row_L.gameObject.SetActive(false);
        tf_Row_R.gameObject.SetActive(false);
        PlayAnim("CarryFront");
    }

    public void SetCharacterThrow(Action actionBefore, Action actionAfter)
    {
        //this.actionBefore = actionBefore;
        //this.actionAfter = actionAfter;
        //PlayAnim("Run");
        actionBefore?.Invoke();
        actionAfter?.Invoke();
    }

    public void SetCharacterRow()
    {
        tf_Row_L.gameObject.SetActive(true);
        tf_Row_R.gameObject.SetActive(true);
        PlayAnim("Row");
    }

    public void PlayAnim(string stateName)
    {
        actionBefore?.Invoke();
        characterAnim.Play(stateName);
    }
}