﻿using Pathfinding;
using UnityEditor;
using UnityEngine;

public enum CharacterIntentEnum
{
    Idle,
    GoToIsland,
    GoToGold,
    Search,
    ExitIsland,
    Back,
}

public class CharacterCpt : BaseMonoBehaviour, IBaseObserver
{
    //手
    public Transform transform_Hand;
    //船
    public Transform transform_Boat;

    public GoldHandler handler_Gold;
    public CharacterHandler handler_Character;
    public GameDataHandler handler_GameData;
    public GameStartSceneHandler handler_Scene;
    public GameHandler handler_Game;
    public ShipHandler handler_Ship;

    //protected AIForCharacterPath aiForCharacterPath;
    protected AIForCharacterPathAuto aiForCharacterPath;
    protected CharacterAnimCpt characterAnim;
    protected CharacterLifeCpt characterLife;

    protected CharacterDataBean characterData;

    protected CharacterIntentEnum characterIntent = CharacterIntentEnum.Idle;

    protected GoldCpt handGold;
    protected GoldCpt targetGold;

    private void Awake()
    {
        aiForCharacterPath = CptUtil.AddCpt<AIForCharacterPathAuto>(gameObject);
        characterAnim = GetComponent<CharacterAnimCpt>();
        characterLife = GetComponent<CharacterLifeCpt>();

        AutoLinkHandler();
        ReflexUtil.AutoLinkDataForChild(this, "transform_");
    }

    private void Update()
    {
        switch (characterIntent)
        {
            case CharacterIntentEnum.GoToIsland:
                HandleForGoToIsland();
                break;
            case CharacterIntentEnum.GoToGold:
                HandleForGoToGold();
                break;
            case CharacterIntentEnum.ExitIsland:
                HandleForExitIsland();
                break;
            case CharacterIntentEnum.Back:
                HandleForBack();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (characterIntent != CharacterIntentEnum.GoToGold)
        {
            return;
        }
        GoldCpt goldCpt = other.gameObject.GetComponent<GoldCpt>();
        if (goldCpt != null && (goldCpt.GetGoldStatus() == GoldStatusEnum.Idle || goldCpt.GetGoldStatus() == GoldStatusEnum.Drop))
        {
            aiForCharacterPath.StopMove();
            SetHandGold(goldCpt);
        }
    }
    private void OnDestroy()
    {
        //掉落金币
        if (handGold)
            handGold.SetDrop();
    }

    /// <summary>
    /// 刷新角色数据
    /// </summary>
    public void RefreshCharacter()
    {
        aiForCharacterPath.SetMoveSpeed(characterData.moveSpeed);
        characterLife.SetLife(characterData.maxLife, characterData.life);
    }

    public void AddLife(int life)
    {
        int currentLife = characterData.AddLife(life);
        RefreshCharacter();
        if (currentLife <= 0)
        {
            //死亡
            handler_Character.CleanCharacter(this);
            //删除角色
            Destroy(gameObject);
        }
    }

    public void SetLife(int maxLife)
    {
        characterData.SetLife(maxLife);
        RefreshCharacter();
    }

    public void AddMaxLife(int addLife)
    {
        characterData.SetLife(characterData.maxLife + addLife);
        RefreshCharacter();
    }

    /// <summary>
    /// 设置角色数据
    /// </summary>
    /// <param name="characterData"></param>
    public void SetCharacterData(CharacterDataBean characterData)
    {
        this.characterData = characterData;
        SetIntentForGoToIsland();
        RefreshCharacter();
    }

    /// <summary>
    /// 增加角色速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetCharacterSpeed(float speed)
    {
        float moveSpeed = characterData.SetSpeed(speed);
        aiForCharacterPath.SetMoveSpeed(moveSpeed);
    }


    public void SetIntentForIdle()
    {
        this.characterIntent = CharacterIntentEnum.Idle;
    }

    public void SetIntentForGoToIsland()
    {
        SetBoatStatus(true);
        this.characterIntent = CharacterIntentEnum.GoToIsland;
        Vector3 islandPosition = handler_Scene.GetIslandPosition(characterData.characterType);
        islandPosition += new Vector3(Random.Range(-3f, 3f), 0, 0);
        aiForCharacterPath.SetDestination(islandPosition);
    }

    public void SetIntentForGoToGold(Vector3 goldPosition)
    {
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.GoToGold;
        aiForCharacterPath.SetDestination(goldPosition);
    }

    public void SetIntentForSearch()
    {
        if (targetGold != null)
        {
            targetGold = null;
        }
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.Search;
        targetGold = handler_Gold.GetTargetGold();
        if (targetGold)
        {
            targetGold.AddObserver(this);
            SetIntentForGoToGold(targetGold.transform.position);
        }
        else
        {
            //没有金币就离开
            SetIntentForExitIsland();
        }
    }

    public void SetIntentForExitIsland()
    {
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.ExitIsland;
        Vector3 islandPosition = handler_Scene.GetIslandPosition(characterData.characterType);
        islandPosition += new Vector3(Random.Range(-3f, 3f), 0, 0);
        aiForCharacterPath.SetDestination(islandPosition);
    }

    public void SetIntentForBack()
    {
        SetBoatStatus(true);
        this.characterIntent = CharacterIntentEnum.Back;
        Vector3 startPosition = handler_Scene.GetStartPosition(characterData.characterType);
        aiForCharacterPath.SetDestination(startPosition);
    }

    /// <summary>
    /// 获取角色数据
    /// </summary>
    /// <returns></returns>
    public CharacterDataBean GetCharacterData()
    {
        return characterData;
    }

    public void HandleForGoToIsland()
    {
        if (aiForCharacterPath.IsAutoMoveStop())
        {
            SetIntentForSearch();
        }
    }

    /// <summary>
    /// 拿起目标
    /// </summary>
    /// <param name="goldCpt"></param>
    public void SetHandGold(GoldCpt goldCpt)
    {
        handGold = goldCpt;
        targetGold = null;
        if (handGold == null)
        {
            SetIntentForIdle();
        }
        else
        {
            handGold.SetCarry(characterData.characterType, transform_Hand);
            SetIntentForExitIsland();
        }
    }

    public void HandleForGoToGold()
    {
        if (aiForCharacterPath.IsAutoMoveStop())
        {
            //获取金币
            if (targetGold.GetGoldStatus() != GoldStatusEnum.Idle && targetGold.GetGoldStatus() != GoldStatusEnum.Drop)
            {
                //另外搜寻目标
                SetIntentForSearch();
            }
            else
            {
                SetHandGold(targetGold);
            }
        }
    }

    public void HandleForExitIsland()
    {
        if (aiForCharacterPath.IsAutoMoveStop())
        {
            SetIntentForBack();
        }
    }

    public void HandleForBack()
    {
        if (aiForCharacterPath.IsAutoMoveStop())
        {
            if (handGold != null)
            {
                //归还金币
                //增加金币
                handler_Game.AddGold(characterData.characterType, handGold.goldData.gold_price);
                //回收金币
                ShipCpt shipCpt = handler_Ship.GetShip(characterData.characterType);
                handGold.SetRecycle(shipCpt.transform.position);
                //检测游戏是否结束
                handler_Game.CheckGameOver();
            }
            if (handler_Gold.GetTargetGold() == null)
            {
                //如果没有金币。说明已经搬完 回收
                SetIntentForIdle();
                handler_Character.CleanCharacter(this);
                Destroy(gameObject);
            }
            else
            {
                SetIntentForGoToIsland();
            }
        }
    }

    /// <summary>
    /// 设置船的状态
    /// </summary>
    /// <param name="isShow"></param>
    public void SetBoatStatus(bool isShow)
    {
        if (isShow)
        {
            transform_Boat.gameObject.SetActive(true);
        }
        else
        {
            transform_Boat.gameObject.SetActive(false);
        }
    }

    #region 通知回调
    public void ObserbableUpdate<T>(T observable, int type, params object[] obj) where T : Object
    {
        if (observable as GoldCpt)
        {
            if (type == (int)GoldCpt.NotifyTypeEnum.HasCarry && handGold == null)
            {
                SetIntentForSearch();
            }
        }
    }
    #endregion
}