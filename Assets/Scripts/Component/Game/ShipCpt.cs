using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class ShipCpt : BaseMonoBehaviour
{
    public ShipDataBean shipData;
    public GameObject bulletModel;

    public void SetData(ShipDataBean shipData, GameObject bulletModel)
    {
        this.bulletModel = bulletModel;
        this.shipData = shipData;
    }

    public void OpenFire(Vector3 targetPosition)
    {
        GameObject objBullet = Instantiate(gameObject, bulletModel, transform.position);
        ShipBulletCpt shipBullet = objBullet.GetComponent<ShipBulletCpt>();
        shipBullet.SetData(shipData.bulletDamage);
        shipBullet.MoveParabola(targetPosition, 10);
    }


}