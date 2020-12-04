using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : BaseUIManager {

    Action testAction = () =>
    {
        LogUtil.Log("Init");
    };

    private void Start()
    {
        testAction();
        Action testAction2= () =>
        {
            LogUtil.Log("Init2");
        };
        testAction += testAction2;
        testAction();
        testAction -= testAction2;
        testAction();
    }

}
