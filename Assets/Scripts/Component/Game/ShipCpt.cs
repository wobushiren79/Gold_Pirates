using DG.Tweening;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class ShipCpt : BaseObservable<IBaseObserver>
{

    public ShipDataBean shipData;
    public GameObject bulletModel;
   
    public UIManager manager_UI;
    public GameDataHandler handler_GameData;
    public EffectHandler handler_Effect;
    public CharacterHandler handler_Character;
    public Transform tf_FirePosition;
    public bool isAutoFire = false;
    public bool canFire = true;

    protected ShipAnimCpt shipAnim;
    private void Awake()
    {
        ReflexUtil.AutoLinkDataForChild(this,"tf_");
        AutoLinkManager();
        AutoLinkHandler();
        shipAnim = gameObject.AddComponent<ShipAnimCpt>();
        shipAnim.InitAnim();
    }

    public void SetData(ShipDataBean shipData, GameObject bulletModel)
    {
        this.bulletModel = bulletModel;
        this.shipData = shipData;
    }

    public void OpenFire(Vector3 targetPosition)
    {
        if (!canFire)
        {
            return;
        }
        GameObject objBullet = Instantiate(gameObject, bulletModel, tf_FirePosition.position);
        ShipBulletCpt shipBullet = objBullet.GetComponent<ShipBulletCpt>();
        shipBullet.SetData(shipData.characterType, shipData.bulletDamage);
        //子弹高度
        float bulletHight= handler_GameData.GetBulletHight();
        float bulletSpeed= handler_GameData.GetBulletSpeed();
        shipBullet.MoveParabola(targetPosition, bulletHight, bulletSpeed);

        //玩家打炮倒计时
        if (shipData.characterType == CharacterTypeEnum.Player)
        {
            StartCoroutine(CoroutineForFireCD(shipData.intervalForFire));
        }
        //大炮粒子
        handler_Effect.PlayEffect(EffectInfo.SHIP_FIRE, tf_FirePosition.position,1.4f);
        //打炮动画
        shipAnim.SetShipFire(() => {
            shipAnim.SetShipIdle();
        });
    }

    public void StartAutoOpenFire(Vector3 targetPosition)
    {
        StartCoroutine(CoroutineForAutoFire(targetPosition));
    }

    public void CloseFire()
    {
        isAutoFire = false;
        canFire = true;
        StopAllCoroutines();
    }

    public void SetDamage(int damage)
    {
        shipData.bulletDamage = damage;
    }

    public IEnumerator CoroutineForAutoFire(Vector3 targetPosition)
    {
        isAutoFire = true;
        while (isAutoFire)
        {
            //容错处理
            if (shipData.intervalForFire <= 0)
            {
                shipData.intervalForFire = 1;
            }
            yield return new WaitForSeconds(shipData.intervalForFire);
            //限制处理 少于多少人不开火
            if (shipData.limitForFireNumber > handler_Character.GetCharacterNumber(CharacterTypeEnum.Player))
            {

            }
            else
            {
                OpenFire(targetPosition);
            }
        }
    }

    public IEnumerator CoroutineForFireCD(float time)
    {
        canFire = false;
        float maxTime = time;
        //修改UI
        UIGameStart uiGameStart = manager_UI.GetUI<UIGameStart>(UIEnum.GameStart);
        while (time > 0)
        {
            if (uiGameStart != null)
                uiGameStart.SetFireCD(maxTime, time);
            yield return new WaitForSeconds(0.02f);
            time -= 0.02f;
        }
        if (uiGameStart != null)
            uiGameStart.SetFireCD(0, 0);
        canFire = true;
    }

}