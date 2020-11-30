using Pathfinding;
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

public class CharacterCpt : BaseMonoBehaviour
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

    protected AIForCharacterPath aiForCharacterPath;
    protected CharacterAnimCpt characterAnim;
    protected CharacterLifeCpt characterLife;

    protected CharacterDataBean characterData;

    protected CharacterIntentEnum characterIntent = CharacterIntentEnum.Idle;

    protected GoldCpt handGold;

    private void Awake()
    {
        aiForCharacterPath = CptUtil.AddCpt<AIForCharacterPath>(gameObject);
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

    /// <summary>
    /// 刷新角色数据
    /// </summary>
    public void RefreshCharacter()
    {
        UserDataBean userData = handler_GameData.GetUserData();
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            SetCharacterSpeed(userData.speed + characterData.moveSpeed);

        }
        else
        {

        }
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
            //掉落金币
            if (handGold)
                handGold.SetDrop();
            //删除角色
            Destroy(gameObject);
        }
    }

    public void SetLife(int maxLife)
    {
         characterData.SetLife(maxLife);
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
    /// 设置角色速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetCharacterSpeed(float speed)
    {
        aiForCharacterPath.SetMoveSpeed(speed);
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
        aiForCharacterPath.SetDestination(islandPosition);
    }

    public void SetIntentForGoToGold()
    {
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.GoToGold;
        Vector3  goldPosition = handler_Gold.GetIdleGoldPosition();
        aiForCharacterPath.SetDestination(goldPosition);
    }

    public void SetIntentForExitIsland()
    {
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.ExitIsland;
        Vector3 islandPosition = handler_Scene.GetIslandPosition(characterData.characterType);
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
            SetIntentForGoToGold();
        }
    }

    public void HandleForGoToGold()
    {   
        if (aiForCharacterPath.IsAutoMoveStop())
        {
            //获取金币
            handGold = handler_Gold.GetIdleGold();
            if (handGold == null)
            {
                SetIntentForIdle();
            }
            else
            {
                handGold.SetCarry(transform_Hand);
                SetIntentForExitIsland();
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
            //归还金币
            if (handGold != null)
            {
                //增加金币
                handler_Game.AddGold(characterData.characterType, handGold.goldData.gold_price);
                //回收金币
                ShipCpt shipCpt =  handler_Ship.GetShip(characterData.characterType);
                handGold.SetRecycle(shipCpt.transform.position);
                //检测游戏是否结束
                handler_Game.CheckGameOver();
            }
            SetIntentForGoToIsland();
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
}