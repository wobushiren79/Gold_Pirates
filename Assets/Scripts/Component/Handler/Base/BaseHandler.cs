using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public class BaseHandler<T> : BaseObservable<IBaseObserver> where T: BaseManager
{
    protected T manager;

    protected virtual void Awake()
    {
        manager = CptUtil.AddCpt<T>(gameObject);
        AutoLinkHandler();
        AutoLinkManager();
    }
    /// <summary>
    /// 通过反射链接UI控件
    /// </summary>
    public void AutoLinkHandler()
    {
        ReflexUtil.AutoLinkData(this,"handler_");
    }

    public void AutoLinkManager()
    {
        ReflexUtil.AutoLinkData(this, "manager_");
    }
}