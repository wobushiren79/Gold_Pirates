﻿using UnityEditor;
using UnityEngine;

public class AnalyticsSdkManager : BaseManager
{
    public FacebookSdkManager manager_FacebookSdk;

    private void Awake()
    {
        AutoLinkManager();
        Init();
    }

    public void Init()
    {
        manager_FacebookSdk.Init();
    }

    public void LogEvent(string name)
    {
#if UNITY_EDITOR
        // Debug.LogError("打点事件:" + name);
#endif

        // Debug.LogError("打点事件:" + name);

        //FirebaseSdkManager.Instance.LogEvent(name);
        manager_FacebookSdk.LogEvent(name);
        //////GameAnalytics.NewDesignEvent(name);
        //////AppsFlyer.sendEvent(name, null);
    }



    public void LogEvent(string name, string parameterName, long parameterValue)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
#endif
        // Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
        //FirebaseSdkManager.Instance.LogEvent(name, parameterName, parameterValue);
        manager_FacebookSdk.LogEvent(name, parameterName, parameterValue);

        //////if (parameterName.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName + ":" + parameterValue);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName, parameterValue);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName, parameterValue.ToString() } });
    }

    public void LogEvent(string name, string parameterName, string parameterValue)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
#endif
        //FirebaseSdkManager.Instance.LogEvent(name, parameterName, parameterValue);
        manager_FacebookSdk.LogEvent(name, parameterName, parameterValue);
        // Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
        //////if (parameterName.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName + ":" + parameterValue);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName, parameterValue);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName, parameterValue.ToString() } });
    }
    public void LogEvent(string name, string parameterName1, string parameterValue1, string parameterName2, string parameterValue2)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
#endif
        //FirebaseSdkManager.Instance.LogEvent(name, parameterName1, parameterValue1, parameterName2, parameterValue2);
        manager_FacebookSdk.LogEvent(name, parameterName1, parameterValue1, parameterName2, parameterValue2);
        // Debug.LogError($"打点事件:{name}  {parameterName1}  {parameterValue1} {parameterName2} {parameterValue2}");
        //////if (parameterName.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName + ":" + parameterValue);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName, parameterValue);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName, parameterValue.ToString() } });
    }
    public void LogEvent(string name, string parameterName1, long parameterValue1, string parameterName2, long parameterValue2)
    {
#if UNITY_EDITOR
        // Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2}");
#endif

        //FirebaseSdkManager.Instance.LogEvent(name, new Parameter(parameterName1, parameterValue1), new Parameter(parameterName2, parameterValue2));

        manager_FacebookSdk.LogEvent(name, parameterName1, parameterValue1, parameterName2, parameterValue2);
        // Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2}");
        //////if (parameterName1.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1 + ":" + parameterValue1);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1, parameterValue1);
        //////}

        //////if (parameterName2.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2 + ":" + parameterValue2);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2, parameterValue2);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName1, parameterValue1.ToString() }, { parameterName2, parameterValue2.ToString() } });
    }

    public void LogEvent(string name, string parameterName1, long parameterValue1, string parameterName2, long parameterValue2, string parameterName3, long parameterValue3)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2} {parameterName3} {parameterValue3}");
#endif

        //FirebaseSdkManager.Instance.LogEvent(name, new Parameter(parameterName1, parameterValue1), new Parameter(parameterName2, parameterValue2), new Parameter(parameterName3, parameterValue3));
        manager_FacebookSdk.LogEvent(name, parameterName1, parameterValue1, parameterName2, parameterValue2, parameterName3, parameterValue3);
        // Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2} {parameterName3} {parameterValue3}");
        //////if (parameterName1.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1 + ":" + parameterValue1);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1, parameterValue1);
        //////}

        //////if (parameterName2.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2 + ":" + parameterValue2);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2, parameterValue2);
        //////}

        //////if (parameterName3.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName3 + ":" + parameterValue3);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName3, parameterValue3);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName1, parameterValue1.ToString() }, { parameterName2, parameterValue2.ToString() }, { parameterName3, parameterValue3.ToString() } });
    }

    public void LogEvent(string name, string parameterName1, long parameterValue1, string parameterName2, long parameterValue2, string parameterName3, double parameterValue3)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2} {parameterName3} {parameterValue3}");
#endif

        //FirebaseSdkManager.Instance.LogEvent(name, new Parameter(parameterName1, parameterValue1), new Parameter(parameterName2, parameterValue2), new Parameter(parameterName3, parameterValue3));

        manager_FacebookSdk.LogEvent(name, parameterName1, parameterValue1, parameterName2, parameterValue2, parameterName3, parameterValue3);

        // Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2} {parameterName3} {parameterValue3}");

        //////if (parameterName1.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1 + ":" + parameterValue1);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1, parameterValue1);
        //////}

        //////if (parameterName2.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2 + ":" + parameterValue2);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2, parameterValue2);
        //////}

        //////GameAnalytics.NewDesignEvent(name + ":" + parameterName3, (float)parameterValue3);

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName1, parameterValue1.ToString() }, { parameterName2, parameterValue2.ToString() }, { parameterName3, parameterValue3.ToString() } });
    }

    public void LogEvent(string name, string parameterName1, long parameterValue1, string parameterName2, long parameterValue2, string parameterName3, long parameterValue3, string parameterName4, long parameterValue4)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2} {parameterName3} {parameterValue3} {parameterName4} {parameterValue4}");
#endif

        //FirebaseSdkManager.Instance.LogEvent(name, new Parameter(parameterName1, parameterValue1), new Parameter(parameterName2, parameterValue2), new Parameter(parameterName3, parameterValue3), new Parameter(parameterName4, parameterValue4));

        manager_FacebookSdk.LogEvent(name, parameterName1, parameterValue1, parameterName2, parameterValue2, parameterName3, parameterValue3, parameterName4, parameterValue4);
        // Debug.LogError($"打点事件:{name} {parameterName1}  {parameterValue1} {parameterName2}  {parameterValue2} {parameterName3} {parameterValue3} {parameterName4} {parameterValue4}");
        //////if (parameterName1.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1 + ":" + parameterValue1);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName1, parameterValue1);
        //////}

        //////if (parameterName2.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2 + ":" + parameterValue2);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName2, parameterValue2);
        //////}

        //////if (parameterName3.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName3 + ":" + parameterValue3);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName3, parameterValue3);
        //////}

        //////if (parameterName4.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName4 + ":" + parameterValue4);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName4, parameterValue4);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName1, parameterValue1.ToString() }, { parameterName2, parameterValue2.ToString() }, { parameterName3, parameterValue3.ToString() }, { parameterName4, parameterValue4.ToString() } });
    }


    public void LogEvent(string name, string parameterName, int parameterValue)
    {
#if UNITY_EDITOR
        //Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
#endif

        //FirebaseSdkManager.Instance.LogEvent(name, parameterName, parameterValue);

        manager_FacebookSdk.LogEvent(name, parameterName, parameterValue);
        // Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
        //////if (parameterName.EndsWith("Id"))
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName + ":" + parameterValue);
        //////}
        //////else
        //////{
        //////    GameAnalytics.NewDesignEvent(name + ":" + parameterName, parameterValue);
        //////}

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName, parameterValue.ToString() } });
    }



    public void LogEvent(string name, string parameterName, double parameterValue)
    {
#if UNITY_EDITOR
        // Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
#endif
        // Debug.LogError($"打点事件:{name}  {parameterName}  {parameterValue}");
        //FirebaseSdkManager.Instance.LogEvent(name, parameterName, parameterValue);

        manager_FacebookSdk.LogEvent(name, parameterName, parameterValue);

        //////GameAnalytics.NewDesignEvent(name + ":" + parameterName, (float)parameterValue);

        //////AppsFlyer.sendEvent(name, new Dictionary<string, string> { { parameterName, parameterValue.ToString() } });
    } 
}
