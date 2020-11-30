using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterManager : BaseManager
{
    protected List<CharacterCpt> listPlayer = new List<CharacterCpt>();
    protected List<CharacterCpt> listEnemy = new List<CharacterCpt>();

    protected GameObject objCharacterModel;

    public IEnumerator InitCharacterData()
    {
        //加载模型
        yield return CoroutineForLoadCharacterModel("Pirate_1");
    }

    public void CreateCharacter(Vector3 startPosition, CharacterDataBean characterData)
    {
        if (objCharacterModel == null)
            return;
        GameObject objCharacter = Instantiate(gameObject, objCharacterModel, startPosition);
        CharacterCpt characterCpt = objCharacter.GetComponent<CharacterCpt>();
        characterCpt.SetCharacterData(characterData);
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            listPlayer.Add(characterCpt);
        }
        else if (characterData.characterType == CharacterTypeEnum.Enemy)
        {
            listEnemy.Add(characterCpt);
        }
    }

    public void RefreshPlayerCharacter()
    {
        for (int i = 0; i < listPlayer.Count; i++)
        {
            CharacterCpt characterItem = listPlayer[i];
            characterItem.RefreshCharacter();
        }
    }

    /// <summary>
    /// 获取玩家海盗数量
    /// </summary>
    /// <returns></returns>
    public int GetPlayerCharacterNumber()
    {
        return listPlayer.Count;
    }

    /// <summary>
    /// 获取敌人海盗数量
    /// </summary>
    /// <returns></returns>
    public int GetEnemyCharacterNumber()
    {
        return listEnemy.Count;
    }
    
    /// <summary>
    /// 清除NPC
    /// </summary>
    /// <param name="characterCpt"></param>
    public void CleanCharacter(CharacterCpt characterCpt)
    {
        CharacterDataBean characterData = characterCpt.GetCharacterData();
        if (characterData.characterType== CharacterTypeEnum.Player)
        {
            listPlayer.Remove(characterCpt);
        }
        else if (characterData.characterType == CharacterTypeEnum.Enemy)
        {
            listEnemy.Remove(characterCpt);
        }
    }

    /// <summary>
    /// 清除所有NPC
    /// </summary>
    public void CleanAllCharacter()
    {
        for (int i = 0; i < listPlayer.Count; i++)
        {
            CharacterCpt characterItem = listPlayer[i];
            Destroy(characterItem.gameObject);
        }
        for (int i = 0; i < listEnemy.Count; i++)
        {
            CharacterCpt characterItem = listEnemy[i];
            Destroy(characterItem.gameObject);
        }
        listPlayer.Clear();
        listEnemy.Clear();
    }
    protected IEnumerator CoroutineForLoadCharacterModel(string modelName)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync("Character/" + modelName);
        yield return resourceRequest;
        objCharacterModel = resourceRequest.asset as GameObject;
    }
}