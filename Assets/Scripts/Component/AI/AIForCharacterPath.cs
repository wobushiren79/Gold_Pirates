using Pathfinding;
using Pathfinding.RVO;
using UnityEditor;
using UnityEngine;

public class AIForCharacterPath : BaseMonoBehaviour
{
    protected CharacterAnimCpt characterAnim;
    protected Seeker aiSeeker;
    //移动差值
    public float lerpOffset = 0.9f;
    //移动速度
    public float moveSpeed = 1;
    //找到的路径
    public Path aiPath;
    public int aiPathPosition;
    public bool aiPathReached = false;

    private void Awake()
    {
        aiSeeker = CptUtil.AddCpt<Seeker>(gameObject);
        characterAnim = GetComponent<CharacterAnimCpt>();
    }

    public void Update()
    {
        if (aiPath != null)
        {
            if (aiPathPosition >= aiPath.vectorPath.Count)
            {
                InitAIPath();
                return;
            }
            else
            {
                aiPathReached = true;
                bool isArrive = Move(aiPath.vectorPath[aiPathPosition]);
                if (isArrive)
                {
                    aiPathPosition++;
                }
            }
        }
    }

    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    /// <summary>
    /// 自动移动
    /// </summary>
    /// <param name="position">目的地</param>
    public void SetDestination(Vector3 position)
    {
        if (aiSeeker != null)
        {
            aiPath = null;
            aiPathPosition = 0;
            aiPathReached = true;
            aiSeeker.StartPath(transform.position, position, OnPathComplete);
        }
    }

    /// <summary>
    /// 路径找寻完毕
    /// </summary>
    /// <param name="path"></param>
    public void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            aiPath = path;
            aiPathPosition = 0;
            characterAnim.SetCharacterWalk();
        }
        else
        {
            InitAIPath();
            //LogUtil.LogWarning("路径查询失败");
        }
    }
    protected void InitAIPath()
    {
        aiPath = null;
        aiPathPosition = 0;
        aiPathReached = false;
        characterAnim.SetCharacterStand();
    }

    /// <summary>
    /// 自动寻路
    /// </summary>
    /// <param name="movePosition"></param>
    public bool Move(Vector3 movePosition)
    {
        //objMove.transform.position = Vector3.Lerp(objMove.transform.position, movePosition, moveSpeed * Time.deltaTime);
        transform.LookAt(movePosition);

        transform.position = Vector3.MoveTowards(transform.position, movePosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePosition) < 0.01f)
        {
            transform.position = movePosition;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// 自动寻路是否停止
    /// </summary>
    /// <returns></returns>
    public bool IsAutoMoveStop()
    {
        if (aiPath != null || aiPathReached)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}