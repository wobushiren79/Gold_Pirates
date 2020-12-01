using DG.Tweening;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class ShipCpt : BaseMonoBehaviour
{
    public ShipDataBean shipData;
    public GameObject bulletModel;
    public bool isAutoFire = false;

    public void SetData(ShipDataBean shipData, GameObject bulletModel)
    {
        this.bulletModel = bulletModel;
        this.shipData = shipData;
    }

    public void OpenFire(Vector3 targetPosition)
    {
        GameObject objBullet = Instantiate(gameObject, bulletModel, transform.position);
        ShipBulletCpt shipBullet = objBullet.GetComponent<ShipBulletCpt>();
        shipBullet.SetData(shipData.characterType, shipData.bulletDamage);
        shipBullet.MoveParabola(targetPosition, 10);
    }

    public void CloseFire()
    {
        isAutoFire = false;
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
            yield return new WaitForSeconds(Random.Range(10, 20));
            OpenFire(targetPosition);
        }
    }
}