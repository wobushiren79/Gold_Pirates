using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIViewForFireButton : BaseUIView
{
    public Button ui_BtFire;
    public Image ui_IvFire;
    public Image ui_Progress;
    public RectTransform ui_TimeCD;
    public TextMeshProUGUI ui_TvTimeCD;

    public ICallBack callBack;

    public void Start()
    {
        ui_BtFire.onClick.AddListener(OnClickForFire);
    }

    public void SetCallBack(ICallBack callBack)
    {
        this.callBack = callBack;
    }

    public void ChangeStatus(bool isTimeCD)
    {
        if (isTimeCD)
        {
            ui_BtFire.interactable = false;
            ui_TimeCD.gameObject.SetActive(true);

            ColorUtility.TryParseHtmlString("#FF2E2E", out Color colorTimeCDOutline);
            ui_TvTimeCD.outlineColor = colorTimeCDOutline;
        }
        else
        {
            ui_BtFire.interactable = true;
            ui_TimeCD.gameObject.SetActive(false);
        }
    }

    public void SetTime(float maxTime, float time)
    {
        if (maxTime == 0)
            maxTime = 1;
        float pro = time / maxTime;
        ui_Progress.fillAmount = pro;
        ui_TvTimeCD.text = (int)(time + 1) + "";
    }

    public void OnClickForFire()
    {
        if (callBack != null)
            callBack.OnClickForFire();
    }

    public interface ICallBack
    {
        /// <summary>
        /// 点击开火
        /// </summary>
        void OnClickForFire();
    }

}