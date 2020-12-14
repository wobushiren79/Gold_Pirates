using UnityEditor;
using UnityEngine;
using Pathfinding.Examples;
using Pathfinding;

public class AIForCharacterPathAuto : BaseMonoBehaviour
{
    public RichAI aiPath;
    protected CharacterAnimCpt characterAnim;

    public Transform tfTarget;


    private void Awake()
    {
        aiPath = GetComponent<RichAI>();
        characterAnim = GetComponent<CharacterAnimCpt>(); 
    }

    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetMoveSpeed(float moveSpeed)
    {
        aiPath.maxSpeed = moveSpeed;
    }

    /// <summary>
    /// 自动移动
    /// </summary>
    /// <param name="position">目的地</param>
    public void SetDestination(Vector3 targetPosition)
    {
        aiPath.acceleration = float.MaxValue;
        aiPath.destination = targetPosition;
        aiPath.SearchPath();
        aiPath.onSearchPath = () =>
        {
            aiPath.canMove = true;
        };
    }
    
    /// <summary>
    /// 停止寻路
    /// </summary>
    public void StopMove()
    {
        aiPath.canMove = false;
    }

    /// <summary>
    /// 关闭寻路
    /// </summary>
    public void ClosePath()
    {
        aiPath.enabled = false;
    }

    /// <summary>
    /// 自动寻路是否停止
    /// </summary>
    /// <returns></returns>
    public bool IsAutoMoveStop()
    {
        //有路径，到达目的地或者与最终目的地相隔
        if (aiPath.hasPath && aiPath.reachedDestination)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}