using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class ShipBulletCpt : BaseMonoBehaviour
{
    protected int bulletDamage;
    protected CharacterTypeEnum characterType;
    public void SetData(CharacterTypeEnum characterType,int damage)
    {
        this.bulletDamage = damage;
        this.characterType = characterType;
    }

    public void MoveParabola(Vector3 targetPosition, float parabolaH)
    {
        Vector3[] path = new Vector3[3];
        path[0] = transform.position;
        path[1] = Vector3.Lerp(targetPosition, transform.position, 0.5f) + Vector3.up * parabolaH;
        path[2] = targetPosition;
        transform
            .DOPath(path, 1f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterCpt characterCpt = other.GetComponent<CharacterCpt>();
        if (characterCpt)
        {
            //不可以攻击自己人
            if (characterCpt.GetCharacterData().characterType!=this.characterType)
            {
                characterCpt.AddLife(-bulletDamage);
            }
        }
    }
}