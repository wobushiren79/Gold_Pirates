using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameLevelBean : BaseBean
{
    //当前关卡等级
    public int level;
    //对应gold_type的id
    public long gold_id;
    //总共生成金币数量
    public int gold_number;
    //金币堆数据
    public string gold_pile;
    //敌人上限数量
    public int enemy_number;
    //敌人初始生命值
    public int enemy_life;
    //对应ship_type的id
    public long enemy_ship_id;
    //敌人开火间隔时间
    public float enemy_fire_interval;
    //敌人开火限制人数
    public int enemy_fire_limit_number;
    //敌人创建间隔
    public float enemy_build_interval;
    //敌人移动速度
    public float enemy_speed;
    //敌人速度增加间隔
    public float enemy_speed_interval;
    //敌人速度增加增量
    public int enemy_speed_incremental;
    //敌人生命增加间隔
    public float enemy_life_interval;
    //敌人生命增加增量
    public int enemy_life_incremental;

}