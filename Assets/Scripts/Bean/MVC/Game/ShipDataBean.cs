using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ShipDataBean : BaseBean
{
    public string model_name;
    public int ship_damage;

    public int bulletDamage = 1;
    public int intervalForFire = 10;
    public CharacterTypeEnum characterType;
}