using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterManager : BaseManager
{
    protected List<CharacterCpt> listPlayer = new List<CharacterCpt>();
    protected List<CharacterCpt> listEnemy = new List<CharacterCpt>();

    protected GameObject objCharacterPlayerModel;
    protected GameObject objCharacterEnemyModel;

    public IEnumerator InitCharacterData()
    {
        CptUtil.RemoveChild(transform);
        //加载模型
        yield return CoroutineForLoadCharacterModel("Pirate_1", "Pirate_2");
    }

    public void CreateCharacter(Vector3 startPosition, CharacterDataBean characterData)
    {
        GameObject objModel = null;
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            objModel = objCharacterPlayerModel;
        }
        else if (characterData.characterType == CharacterTypeEnum.Enemy)
        {
            objModel = objCharacterEnemyModel;
        }
        GameObject objCharacter = Instantiate(gameObject, objModel, startPosition);
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

    public List<CharacterCpt> GetAllPlayerCharacter()
    {
        return listPlayer;
    }
    public List<CharacterCpt> GetAllEnemyCharacter()
    {
        return listEnemy;
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
    protected IEnumerator CoroutineForLoadCharacterModel(string playerModelName,string enemyModelName)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync("Character/Pirate_Base");
        yield return resourceRequest;
        GameObject objCharacterModel = resourceRequest.asset as GameObject;
        //初始化友方模型
        objCharacterPlayerModel = Instantiate(gameObject, objCharacterModel);
        ResourceRequest playerRequest = Resources.LoadAsync("Character/"+ playerModelName);
        yield return playerRequest;
        GameObject objPlayer = playerRequest.asset as GameObject;
        Instantiate(objCharacterPlayerModel, objPlayer);
        CharacterAnimCpt playerAnim = objCharacterPlayerModel.GetComponent<CharacterAnimCpt>();
        playerAnim.InitAnim();
        objCharacterPlayerModel.SetActive(false);
        //初始化敌方模型
        objCharacterEnemyModel = Instantiate(gameObject, objCharacterModel);
        ResourceRequest EnemyRequest = Resources.LoadAsync("Character/"+ enemyModelName);
        yield return EnemyRequest;
        GameObject objEnemy = EnemyRequest.asset as GameObject;
        Instantiate(objCharacterEnemyModel, objEnemy);
        CharacterAnimCpt EnemyAnim = objCharacterEnemyModel.GetComponent<CharacterAnimCpt>();
        EnemyAnim.InitAnim();
        objCharacterEnemyModel.SetActive(false);
        Resources.UnloadUnusedAssets();
    }
}