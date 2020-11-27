using Pathfinding;
using UnityEditor;
using UnityEngine;

public enum CharacterIntentEnum
{
    Idle,
    GoToGold,
    Search,
    Back,
}

public class CharacterCpt : BaseMonoBehaviour
{
    //手
    public Transform tfHand;

    public GoldHandler handler_Gold;
    public GameDataHandler handler_GameData;
    public GameHandler handler_Game;

    protected AIForCharacterPath aiForCharacterPath;
    protected CharacterAnimCpt characterAnim;

    protected CharacterDataBean characterData;

    protected CharacterIntentEnum characterIntent = CharacterIntentEnum.Idle;

    protected Vector3 positionForStart = Vector3.zero;
    protected Vector3 positionForGold = Vector3.zero;

    protected GoldCpt handGold;

    private void Awake()
    {
        aiForCharacterPath = CptUtil.AddCpt<AIForCharacterPath>(gameObject);
        characterAnim = GetComponent<CharacterAnimCpt>();
        AutoLinkHandler();
    }

    private void Update()
    {
        switch (characterIntent)
        {
            case CharacterIntentEnum.GoToGold:
                HandleForGoToGold();
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
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            SetCharacterSpeed(handler_GameData.GetUserData().speed + characterData.moveSpeed);
        }
        else
        {

        } 
    }

    /// <summary>
    /// 设置角色数据
    /// </summary>
    /// <param name="characterData"></param>
    public void SetCharacterData(CharacterDataBean characterData)
    {
        positionForStart = transform.position;
        this.characterData = characterData;
        SetIntentForGoToGold();
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
    public void SetIntentForGoToGold()
    {
        this.characterIntent = CharacterIntentEnum.GoToGold;
        positionForGold = handler_Gold.GetIdleGoldPosition();
        aiForCharacterPath.SetDestination(positionForGold);
    }
    public void SetIntentForBack()
    {
        this.characterIntent = CharacterIntentEnum.Back;
        aiForCharacterPath.SetDestination(positionForStart);
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
                handGold.SetCarry(tfHand);
                SetIntentForBack();
            }
        }
    }

    public void HandleForBack()
    {
        if (aiForCharacterPath.IsAutoMoveStop())
        {
            //归还金币
            if (handGold != null)
            {
                if (characterData.characterType == CharacterTypeEnum.Player)
                {
                    handler_GameData.AddUserGold(handGold.goldData.gold_price);
                }
                handGold.SetRecycle();
                //检测游戏是否结束
                handler_Game.CheckGameOver();
            }
            SetIntentForGoToGold();
        }
    }
}