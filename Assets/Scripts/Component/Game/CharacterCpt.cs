﻿using Pathfinding;
using UnityEditor;
using UnityEngine;
using System.Collections;
using DG.Tweening;

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
    public Transform transform_HandTop;
    public Transform transform_HandFront;
    //船
    public Transform transform_Boat;
    public Transform transform_BoatPosition;
    //加速特效
    public Transform transform_EffectSpeed;

    public GoldHandler handler_Gold;
    public CharacterHandler handler_Character;
    public GameDataHandler handler_GameData;
    public GameStartSceneHandler handler_Scene;
    public GameHandler handler_Game;
    public ShipHandler handler_Ship;
    public EffectHandler handler_Effect;

    //protected AIForCharacterPath aiForCharacterPath;
    protected AIForCharacterPathAuto aiForCharacterPath;
    protected CharacterAnimCpt characterAnim;
    public CharacterLifeCpt characterLife;

    protected CharacterDataBean characterData;

    public CharacterIntentEnum characterIntent = CharacterIntentEnum.Idle;

    protected GoldCpt handGold;
    protected GoldCpt targetGold;

    protected float radiusForBoat = 0.3f;
    protected float radiusForCharacter = 0.3f;

    public Material matBoatRed;
    public Material matBoatBlue;
    private void Awake()
    {
        aiForCharacterPath = CptUtil.AddCpt<AIForCharacterPathAuto>(gameObject);
        characterAnim = GetComponent<CharacterAnimCpt>();

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

    /// <summary>
    /// 刷新角色数据
    /// </summary>
    public void RefreshCharacter()
    {
        aiForCharacterPath.SetMoveSpeed(characterData.moveSpeed);
        characterLife.SetLife(characterData.maxLife, characterData.life);
    }

    public void AddLife(int life, out bool isDead)
    {
        isDead = false;
        int currentLife = characterData.AddLife(life);
        RefreshCharacter();
        if (currentLife <= 0)
        {
            isDead = true;
            SetCharacterDead();
        }
    }

    public void ShowLife(float showTime)
    {
        characterLife.ShowLife(showTime);
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
    ///  设置角色加速
    /// </summary>
    /// <param name="addSpeed"></param>
    /// <param name="time"></param>
    public void SetCharacterSpeedUp(float addSpeed, float time)
    {
        if (transform_EffectSpeed.gameObject.activeSelf)
            return;
        StartCoroutine(CoroutineForCharacterSpeedUp(addSpeed, time));
    }

    /// <summary>
    /// 设置角色数据
    /// </summary>
    /// <param name="characterData"></param>
    public void SetCharacterData(CharacterDataBean characterData)
    {
        this.characterData = characterData;
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            gameObject.tag = "Player";
        }
        else if (characterData.characterType == CharacterTypeEnum.Enemy)
        {
            gameObject.tag = "Enemy";
        }
        SetIntentForGoToIsland();
        RefreshCharacter();
        SetBoat(characterData.characterType);
    }

    public void SetBoat(CharacterTypeEnum characterType)
    {
        Renderer renderer = transform_Boat.GetComponent<Renderer>();

        switch (characterType)
        {
            case CharacterTypeEnum.Enemy:
                renderer.material = matBoatRed;
                break;
            case CharacterTypeEnum.Player:
                renderer.material = matBoatBlue;
                break;
        }

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

    public void AddCharacterSpeed(float addSpeed)
    {
        float moveSpeed = characterData.AddSpeed(addSpeed);
        aiForCharacterPath.SetMoveSpeed(moveSpeed);
    }

    /// <summary>
    /// 设置角色死亡
    /// </summary>
    public void SetCharacterDead()
    {
        //死亡
        handler_Character.CleanCharacter(this);
        //如果是友方角色死亡 降低升级所需金币
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            handler_Game.GetGameData().LevelDownForPlayerPirateNumber(1);
            //播放粒子特效
            handler_Effect.PlayEffect(EffectInfo.PIRATE_DIE_BLUE, transform.position + new Vector3(0, 0.7f, 0));
        }
        else if (characterData.characterType == CharacterTypeEnum.Enemy)
        {
            //播放粒子特效
            handler_Effect.PlayEffect(EffectInfo.PIRATE_DIE_RED, transform.position + new Vector3(0, 0.7f, 0));
        }
        //刷新UI
        handler_Game.manager_UI.RefreshAllUI();
        //设置角色闲置
        SetIntentForIdle();
        //掉落金币
        if (handGold)
            handGold.SetDrop();
        //关闭寻路
        aiForCharacterPath.ClosePath();
        //协程-删除角色
        float destoryTime = handler_GameData.GetCharacterCorpseDestoryTime();
        StartCoroutine(CoroutineForCharacterDead(destoryTime));

    }

    /// <summary>
    /// 炸飞
    /// </summary>
    /// <param name="blowPosition"></param>
    public void BlowUp(Vector3 blowPosition)
    {
        Collider collider = transform.GetComponent<Collider>();
        collider.isTrigger = false;
        Rigidbody rigidbody = CptUtil.AddCpt<Rigidbody>(gameObject);
        rigidbody.AddExplosionForce(500f, blowPosition, 10);
    }


    /// <summary>
    /// 意图-闲置
    /// </summary>
    public void SetIntentForIdle()
    {
        this.characterIntent = CharacterIntentEnum.Idle;
        //停止移动
        aiForCharacterPath.StopMove();
        //设置角色动画状态
        characterAnim.SetCharacterStand();
        aiForCharacterPath.ChangeRadius(radiusForCharacter);
    }

    /// <summary>
    /// 意图-前往岛屿
    /// </summary>
    public void SetIntentForGoToIsland()
    {
        SetBoatStatus(true);
        this.characterIntent = CharacterIntentEnum.GoToIsland;
        Vector3 islandPosition = handler_Scene.GetIslandPosition(characterData.characterType);
        //islandPosition += new Vector3(Random.Range(-3f, 3f), 0, 0);
        aiForCharacterPath.SetDestination(islandPosition + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)));
        characterAnim.SetCharacterRow();
        aiForCharacterPath.ChangeRadius(radiusForBoat);
    }

    /// <summary>
    /// 意图-前往金币
    /// </summary>
    /// <param name="goldPosition"></param>
    public void SetIntentForGoToGold(Vector3 goldPosition)
    {
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.GoToGold;
        aiForCharacterPath.SetDestination(goldPosition);
        characterAnim.SetCharacterRun();
        aiForCharacterPath.ChangeRadius(radiusForCharacter);
    }

    /// <summary>
    /// 意图-搜索金币
    /// </summary>
    public void SetIntentForSearch()
    {
        if (targetGold != null)
        {
            targetGold = null;
        }
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.Search;
        targetGold = handler_Gold.GetTargetGold(transform.position);
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
        aiForCharacterPath.ChangeRadius(radiusForCharacter);
    }

    /// <summary>
    /// 意图-离开岛屿
    /// </summary>
    public void SetIntentForExitIsland()
    {
        SetBoatStatus(false);
        this.characterIntent = CharacterIntentEnum.ExitIsland;
        Vector3 islandPosition = handler_Scene.GetExitIslandPosition(characterData.characterType);
        //islandPosition += new Vector3(Random.Range(-3f, 3f), 0, 0);
        aiForCharacterPath.SetDestination(islandPosition + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)));
        aiForCharacterPath.ChangeRadius(radiusForCharacter);
    }

    /// <summary>
    /// 意图-返回舰队
    /// </summary>
    public void SetIntentForBack()
    {
        SetBoatStatus(true);
        this.characterIntent = CharacterIntentEnum.Back;
        Vector3 startPosition = handler_Scene.GetStartPosition(characterData.characterType);
        aiForCharacterPath.SetDestination(startPosition + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)));
        //将金币放在船上
        if (handGold != null)
        {
            handGold.transform.position = transform_BoatPosition.transform.position;
        }
        characterAnim.SetCharacterRow();
        aiForCharacterPath.ChangeRadius(radiusForBoat);
    }

    /// <summary>
    /// 获取角色数据
    /// </summary>
    /// <returns></returns>
    public CharacterDataBean GetCharacterData()
    {
        return characterData;
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
            int randomHand = Random.Range(0, 2);
            if (randomHand == 0)
            {
                handGold.SetCarry(characterData.characterType, transform_HandTop);
                characterAnim.SetCharacterCarryTop();
            }
            else
            {
                handGold.SetCarry(characterData.characterType, transform_HandFront);
                characterAnim.SetCharacterCarryFront();
            }
            SetIntentForExitIsland();
        }
    }

    /// <summary>
    /// 设置船的状态
    /// </summary>
    /// <param name="isShow"></param>
    public void SetBoatStatus(bool isShow)
    {
        transform_Boat.DOKill();
        transform_Boat.transform.localScale = new Vector3(6, 6, 6);
        if (isShow)
        {
            if (!transform_Boat.gameObject.activeSelf)
            {
                handler_Effect.PlayEffect(EffectInfo.SMOKE_1, transform.position, 3);
            }
            transform_Boat.DOScale(new Vector3(0, 0, 0), 0.3f).From().SetEase(Ease.OutBack);
            transform_Boat.gameObject.SetActive(true);
        }
        else
        {
            if (transform_Boat.gameObject.activeSelf)
            {
                handler_Effect.PlayEffect(EffectInfo.SMOKE_1, transform.position, 3);
            }
            transform_Boat.DOScale(new Vector3(0, 0, 0), 0.3f).OnComplete(() => { transform_Boat.gameObject.SetActive(false); });
        }

    }

    public void HandleForGoToIsland()
    {
        if (aiForCharacterPath.IsAutoMoveStopForEndPath())
        {
            SetIntentForSearch();
        }
    }

    public void HandleForGoToGold()
    {
        if (aiForCharacterPath.IsAutoMoveStopForEndPath())
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
        if (aiForCharacterPath.IsAutoMoveStopForEndPath())
        {
            SetIntentForBack();
        }
    }

    public void HandleForBack()
    {
        if (aiForCharacterPath.IsAutoMoveStopForEndPath())
        {
            System.Action afterAction = () =>
            {
                //扔完之后处理
                if (handler_Gold.GetTargetGold(transform.position) == null)
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
            };

            if (handGold != null)
            {
                //增加角色金币
                long addGold = handler_Game.AddGoldNumber(characterData.characterType, handGold.goldData.gold_price, 1);
                //刷新UI
                handler_Gold.manager_UI.RefreshAllUI();
                //回收金币
                ShipCpt shipCpt = handler_Ship.GetShip(characterData.characterType);
                handGold.SetRecycle(addGold, shipCpt.transform.position);
                //角色扔金币
                characterAnim.SetCharacterThrow(null, afterAction);
            }
            else
            {
                afterAction?.Invoke();
            }
            //检测游戏是否结束
            handler_Game.CheckGameOver();
        }
    }

    /// <summary>
    /// 协程-角色死亡
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoroutineForCharacterDead(float disappearTime)
    {
        yield return new WaitForSeconds(disappearTime);
        //删除角色
        Destroy(gameObject);
    }

    /// <summary>
    /// 协程-角色加速
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator CoroutineForCharacterSpeedUp(float addSpeed, float time)
    {
        aiForCharacterPath.SetMoveSpeed(characterData.moveSpeed + addSpeed);
        transform_EffectSpeed.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        aiForCharacterPath.SetMoveSpeed(characterData.moveSpeed);
        transform_EffectSpeed.gameObject.SetActive(false);
    }

    #region 通知回调
    public void ObserbableUpdate<T>(T observable, int type, params object[] obj) where T : Object
    {
        if (observable as GoldCpt)
        {
            if (type == (int)GoldCpt.NotifyTypeEnum.HasCarry && handGold == null && characterIntent == CharacterIntentEnum.GoToGold)
            {
                SetIntentForSearch();
            }
        }
    }
    #endregion
}