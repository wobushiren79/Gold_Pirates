using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ShipDataBean : BaseBean
{
    public string model_name;

    public int bulletDamage = 1;
    public CharacterTypeEnum characterType;
}