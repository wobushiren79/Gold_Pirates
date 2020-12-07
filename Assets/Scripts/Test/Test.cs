using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : BaseMonoBehaviour {

    public ShipManager manager_Ship;
    private void Start()
    {
        AutoLinkManager();
        manager_Ship.GetShipDataById((data)=> { },1);
    }

}
