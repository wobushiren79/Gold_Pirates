using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class ShipBulletCpt : BaseMonoBehaviour
{
    public EffectHandler handler_Effect;
    public UIManager manager_UI;

    protected int bulletDamage;
    protected CharacterTypeEnum characterType;
    private void Awake()
    {
        AutoLinkHandler();
        AutoLinkManager();
    }

    public void SetData(CharacterTypeEnum characterType, int damage)
    {
        this.bulletDamage = damage;
        this.characterType = characterType;
    }

    public void MoveParabola(Vector3 targetPosition, float parabolaH ,float bulletSpeed)
    {
        Vector3[] path = new Vector3[3];
        path[0] = transform.position;
        path[1] = Vector3.Lerp(targetPosition, transform.position, 0.5f) + Vector3.up * parabolaH;
        path[2] = targetPosition;
        transform
            .DOPath(path, bulletSpeed, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(gameObject);
                handler_Effect.PlayEffect(EffectInfo.BULLET_BLOW, transform.position, bulletSpeed);
                //屏幕震动
                UIGameStart uiGameStart = manager_UI.GetUI<UIGameStart>(UIEnum.GameStart);
                uiGameStart.AnimForShakeUI();
            });
        SphereCollider bulletCollider = GetComponent<SphereCollider>();

        //友方敌方攻击范围颜色
        string effectData = "";
        if(characterType == CharacterTypeEnum.Player)
        {
            effectData = EffectInfo.FIRE_RANGE_BLUE;
        }
        else if (characterType == CharacterTypeEnum.Enemy)
        {
            effectData = EffectInfo.FIRE_RANGE_RED;
        }
        handler_Effect.PlayEffect(effectData, targetPosition, bulletSpeed, Vector3.one * bulletCollider.radius * 0.3f);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        CharacterCpt characterCpt = other.GetComponent<CharacterCpt>();
        if (characterCpt)
        {
            //不可以攻击自己人
            CharacterDataBean characterData = characterCpt.GetCharacterData();
            if (characterData.characterType == this.characterType)
                return;
            //死人不攻击
            if (characterData.life <= 0)
                return;
            characterCpt.AddLife(-bulletDamage, out bool isDead);
            //如果死亡
            if (isDead)
            {
                //炸飞
                characterCpt.BlowUp(transform.position);
            }
            else
            {
               //没死的话展示生命值
               characterCpt.ShowLife(3);
            }
        }
    }
}