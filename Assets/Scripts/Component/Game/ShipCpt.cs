using DG.Tweening;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class ShipCpt : BaseObservable<IBaseObserver>
{
    public ShipDataBean shipData;
    public GameObject bulletModel;
    public UIManager manager_UI;
    public bool isAutoFire = false;
    public bool canFire = true;

    private void Awake()
    {
        AutoLinkManager();
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
        GameObject objBullet = Instantiate(gameObject, bulletModel, transform.position);
        ShipBulletCpt shipBullet = objBullet.GetComponent<ShipBulletCpt>();
        shipBullet.SetData(shipData.characterType, shipData.bulletDamage);
        shipBullet.MoveParabola(targetPosition, 10);
        //玩家打炮倒计时
        if (shipData.characterType == CharacterTypeEnum.Player)
        {
            StartCoroutine(CoroutineForFireCD(shipData.intervalForFire));
        }
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
            OpenFire(targetPosition);
        }
    }

    public IEnumerator CoroutineForFireCD(float time)
    {
        canFire = false;
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            //修改UI
            UIGameStart uiGameStart = (UIGameStart)manager_UI.GetUI(UIEnum.GameStart);
            if (uiGameStart != null)
                uiGameStart.SetFireCD(time);

            time -= 1;
        }
        canFire = true;
    }

}