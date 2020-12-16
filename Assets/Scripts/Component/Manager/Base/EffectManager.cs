using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EffectManager : BaseManager
{
    public Dictionary<string, GameObject> listEffect = new Dictionary<string, GameObject>();
    protected string resUrl = "Effect/";

    public GameObject CreateEffect(string name)
    {
        GameObject objModel = null;
        if (listEffect.TryGetValue(name, out objModel))
        {
            
        }
        else
        {
            objModel = CreatEffictModel(name);
        }
        if (objModel == null)
            return null;
        GameObject objEffect = Instantiate(gameObject, objModel);
        return objEffect;
    }

    private GameObject CreatEffictModel(string name)
    {
        GameObject objEffictModel = Resources.Load<GameObject>(resUrl + name);
        objEffictModel.name = name;
        listEffect.Add(name, objEffictModel);
        return objEffictModel;
    }
}