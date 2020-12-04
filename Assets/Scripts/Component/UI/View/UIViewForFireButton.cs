using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIViewForFireButton : BaseUIView
{
    public Button ui_BtFire;
    public Image ui_IvFire;
    public Image ui_Progress;
    public Text ui_TimeCD;

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
        }
        else
        {
            ui_BtFire.interactable = true;
            ui_TimeCD.gameObject.SetActive(false);
        }
    }

    public void SetTime(float maxTime, float time)
    {
        float pro = time / maxTime;
        ui_Progress.fillAmount = pro;
        ui_TimeCD.text = (int)time + "";
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