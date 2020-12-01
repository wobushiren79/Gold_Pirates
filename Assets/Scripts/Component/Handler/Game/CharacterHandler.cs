using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHandler : BaseHandler<CharacterManager>
{
    protected int numberForEnemy = 100;

    public GameDataHandler handler_GameData;
    public GameHandler handler_Game;
    public GameStartSceneHandler handler_Scene;
    public GoldHandler handler_Gold;

    /// <summary>
    /// 初始化创建角色 用于游戏刚开始
    /// </summary>
    public IEnumerator InitCreateCharacter()
    {
        yield return manager.InitCharacterData();
        //创建一个友方海盗
        CreateCharacter(CharacterTypeEnum.Player);
        //延迟创建敌方海盗
        StartCoroutine(CoroutineForCreateEnmeyCharacter());
    }

    public void CreateCharacter(CharacterTypeEnum characterType)
    {
        if (!handler_Gold.GetTargetGold())
        {
            //没有金币了也不创建角色
            return;
        }
        CharacterDataBean characterData = new CharacterDataBean(characterType);
        UserDataBean userData = handler_GameData.GetUserData();
        if (characterType == CharacterTypeEnum.Player)
        {
            //如果超过上线则不创建
            if (manager.GetPlayerCharacterNumber() >= userData.pirateNumber)
            {          
                return;
            }
        }
        if (characterType == CharacterTypeEnum.Enemy)
        {
            //如果超过上线则不创建
            if (manager.GetEnemyCharacterNumber() >= numberForEnemy)
            {
                return;
            }
            characterData.life = 5;
            characterData.maxLife = 5;
        }
        
        Vector3 startPosition = handler_Scene.GetStartPosition(characterType);
        manager.CreateCharacter(startPosition, characterData);
    }

    public void StopCreateCharacter()
    {
        StopAllCoroutines();
    }

    public void RefreshCharacter()
    {
        manager.RefreshPlayerCharacter();
    }

    public void SetPlayerCharacterLife(int maxLife)
    {
        List<CharacterCpt> listCharacter = manager.GetAllPlayerCharacter();
        for (int i = 0; i < listCharacter.Count; i++)
        {
            CharacterCpt itemCharacter = listCharacter[i];
            itemCharacter.SetLife(maxLife);
        }
    }


    public void CleanCharacter(CharacterCpt characterCpt)
    {
        manager.CleanCharacter(characterCpt);
    }

    public void CleanAllCharacter()
    {
        manager.CleanAllCharacter();
    }

    public IEnumerator CoroutineForCreateEnmeyCharacter()
    {
        for (int i = 0; i < numberForEnemy; i++)
        {
            CreateCharacter(CharacterTypeEnum.Enemy);
            yield return new WaitForSeconds(3);
        }
    }

}