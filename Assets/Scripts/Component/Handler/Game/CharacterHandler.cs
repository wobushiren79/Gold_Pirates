using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHandler : BaseHandler<CharacterManager>
{
    protected int numberForEnemy = 5;

    public GameDataHandler handler_GameData;
    public GameHandler handler_Game;
    public GameStartSceneHandler handler_Scene;

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
        UserDataBean userData = handler_GameData.GetUserData();
        if (characterType == CharacterTypeEnum.Player && manager.GetPlayerCharacterNumber() >= userData.pirateNumber)
        {
            //如果超过上线则不创建
            return;
        }
        if (characterType == CharacterTypeEnum.Enemy && manager.GetEnemyCharacterNumber() >= numberForEnemy)
        {
            //如果超过上线则不创建
            return;
        }
        CharacterDataBean characterDataForPlayer = new CharacterDataBean(characterType);
        Vector3 startPosition = handler_Scene.GetStartPosition(characterType);
        manager.CreateCharacter(startPosition, characterDataForPlayer);
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
            UserDataBean userData = handler_GameData.GetUserData();
            CreateCharacter(CharacterTypeEnum.Enemy);
            yield return new WaitForSeconds(3);
        }
    }

}