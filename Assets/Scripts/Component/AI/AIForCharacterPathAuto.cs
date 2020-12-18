using UnityEditor;
using UnityEngine;
using Pathfinding.Examples;
using Pathfinding;
using Pathfinding.RVO;

public class AIForCharacterPathAuto : BaseMonoBehaviour
{
    public RichAI aiPath;
    public RVOController rvoController;
    protected CharacterAnimCpt characterAnim;

    public Transform tfTarget;

    private void Awake()
    {
        aiPath = GetComponent<RichAI>();
        characterAnim = GetComponent<CharacterAnimCpt>();
        rvoController = GetComponent<RVOController>();
        rvoController.priority = Random.Range(0f, 1f);
    }


    /// <summary>
    /// 修改本地规避半径
    /// </summary>
    /// <param name="radius"></param>
    public void ChangeRadius(float radius)
    {
        aiPath.radius = radius;
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
        //aiPath.reachedDestination（终点）
        //aiPath.reachedEndOfPath (路径的末端 可能没有达到终点 用于寻路不可到达的地点)
        if (!aiPath.pathPending && aiPath.hasPath &&  aiPath.reachedDestination)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsAutoMoveStopForEndPath()
    {
        //有路径，到达目的地或者与最终目的地相隔
        //aiPath.reachedDestination（终点）
        //aiPath.reachedEndOfPath (路径的末端 可能没有达到终点 用于寻路不可到达的地点)
        if (!aiPath.pathPending && aiPath.hasPath && aiPath.reachedEndOfPath)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsAutoMoveStop(float remainingDistance)
    {
        if (!aiPath.pathPending && aiPath.hasPath && aiPath.remainingDistance <= remainingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}