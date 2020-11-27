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

}