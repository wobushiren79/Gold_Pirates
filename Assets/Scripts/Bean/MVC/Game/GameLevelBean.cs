using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameLevelBean : BaseBean
{
    public int level;
    public long gold_id;
    public int gold_number;
    public int enemy_number;
    public int enemy_life;
    public long enemy_ship_id;
    public float enemy_speed;
}