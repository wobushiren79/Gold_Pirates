using UnityEditor;
using UnityEngine;
using Pathfinding.Examples;
public class AIForCharacterPathAuto : RVOExampleAgent
{
    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetMoveSpeed(float moveSpeed)
    {
        maxSpeed = moveSpeed;
    }

    /// <summary>
    /// 自动移动
    /// </summary>
    /// <param name="position">目的地</param>
    public void SetDestination(Vector3 position)
    {
        SetTarget(position);
    }

    /// <summary>
    /// 自动寻路是否停止
    /// </summary>
    /// <returns></returns>
    public bool IsAutoMoveStop()
    {
        if (vectorPath != null || vectorPath.Count == 0|| canSearchAgain)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}