using UnityEditor;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class DialogForLevelUpView : DialogView
{
    public TextMeshProUGUI ui_TvGold;
    public TextMeshProUGUI ui_TvAdvertisingGold;
    public TextMeshProUGUI ui_TvAdvertisingHint;

    public MsgManager manager_Msg;
    public UIManager manager_UI;
    protected float timeForDelayGold = 1;

    public override void Awake()
    {
        base.Awake();
        AutoLinkUI();
    }

    public override void Start()
    {
        base.Start();
        ui_TvAdvertisingHint.text = GameCommonInfo.GetUITextById(101);
        ColorUtility.TryParseHtmlString("#0b752b", out Color outline);
        ui_TvAdvertisingHint.outlineColor = outline;
    }

    public void SetData(long addGold)
    {
        SetAddGold(addGold);
    }

    public void SetAddGold(long addGold)
    {
        if (ui_TvGold)
            ui_TvGold.text = "" + addGold.FormatKM();
        if (ui_TvAdvertisingGold)
        {
            ui_TvAdvertisingGold.text = "" + (addGold * 3).FormatKM();
            ColorUtility.TryParseHtmlString("#0b752b", out Color outline);
            ui_TvAdvertisingGold.outlineColor = outline;
        }
    }

    public float GetTimeForDelayGold()
    {
        return timeForDelayGold;
    }

    public override void SubmitOnClick()
    {
        base.SubmitOnClick();
        AnimForGoldShow();
    }

    public override void CancelOnClick()
    {
        base.CancelOnClick();
        AnimForGoldShow();
    }

    public void AnimForGoldShow()
    {
        Vector3 startPosition = transform.InverseTransformPoint(btSubmit.transform.position);
        UIGameStart uiGameStart = manager_UI.GetUI<UIGameStart>(UIEnum.GameStart);
        Vector3 endPostion = uiGameStart.GetGoldIconUIRootPos();
        uiGameStart.AnimForPunchIvGold(timeForDelayGold);
        for (int i = 0; i < 50; i++)
        {
            MsgForGoldEffectView msgForGoldEffect = manager_Msg.ShowMsg<MsgForGoldEffectView>(MsgEnum.GoldEffect, "", startPosition + new Vector3(Random.Range(-300, 300), Random.Range(-100, 100)));
            msgForGoldEffect.SetTargetPosition(endPostion);
            msgForGoldEffect.SetAnimTime(timeForDelayGold);
        }
    }
}