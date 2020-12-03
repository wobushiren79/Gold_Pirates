using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHandler : BaseHandler<CharacterManager>
{
    protected int numberForEnemy = 1;
    protected bool isBuildEnemy = false;

    public GameDataHandler handler_GameData;
    public GameHandler handler_Game;
    public GameStartSceneHandler handler_Scene;
    public GoldHandler handler_Gold;


    /// <summary>
    /// 初始化创建角色 用于游戏刚开始
    /// </summary>
    public IEnumerator InitCreateCharacter(CharacterDataBean playerCharacterData, CharacterDataBean enemyCharacterData,int numberForEnemy)
    {
        this.numberForEnemy = numberForEnemy;
        yield return manager.InitCharacterData();
        //创建一个友方海盗
        CreateCharacter(playerCharacterData);


        //延迟创建敌方海盗
        StartCoroutine(CoroutineForCreateEnmeyCharacter(enemyCharacterData));
        StartCoroutine(CoroutineForEnemySpeedChange(enemyCharacterData));
        StartCoroutine(CoroutineForEnemyLifeChange(enemyCharacterData));
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="characterData"></param>
    public void CreateCharacter(CharacterDataBean characterData)
    {
        if (!handler_Gold.GetTargetGold())
        {
            //没有金币了也不创建角色
            return;
        }
        UserDataBean userData = handler_GameData.GetUserData();
        if (characterData.characterType == CharacterTypeEnum.Player)
        {
            //如果超过上线则不创建
            if (manager.GetPlayerCharacterNumber() >= userData.pirateNumber)
            {          
                return;
            }
        }
        if (characterData.characterType == CharacterTypeEnum.Enemy)
        {
            //如果超过上线则不创建
            if (manager.GetEnemyCharacterNumber() >= numberForEnemy)
            {
                return;
            }
        }
        
        Vector3 startPosition = handler_Scene.GetStartPosition(characterData.characterType);
        manager.CreateCharacter(startPosition, characterData);
    }

    /// <summary>
    /// 停止持续创建角色
    /// </summary>
    public void StopCreateCharacter()
    {
        isBuildEnemy = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// 刷新角色
    /// </summary>
    public void RefreshCharacter(CharacterTypeEnum characterType)
    {
        if (characterType == CharacterTypeEnum.Player)
        {
            manager.RefreshPlayerCharacter();
        }
        else if (characterType == CharacterTypeEnum.Enemy)
        {
            manager.RefreshEnemyCharacter();
        }
    }

    /// <summary>
    ///  设置角色生命值
    /// </summary>
    /// <param name="characterType"></param>
    /// <param name="maxLife"></param>
    public void SetCharacterLife(CharacterTypeEnum characterType, int maxLife)
    {
        List<CharacterCpt> listCharacter = manager.GetCharacterByType(characterType);

        for (int i = 0; i < listCharacter.Count; i++)
        {
            CharacterCpt itemCharacter = listCharacter[i];
            itemCharacter.SetLife(maxLife);
        }
    }

    public void SetCharacterSpeed(CharacterTypeEnum characterType,float speed)
    {
        List<CharacterCpt> listCharacter = manager.GetCharacterByType(characterType);

        for (int i = 0; i < listCharacter.Count; i++)
        {
            CharacterCpt itemCharacter = listCharacter[i];
            itemCharacter.SetCharacterSpeed(speed);
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

    public IEnumerator CoroutineForCreateEnmeyCharacter(CharacterDataBean enemyCharacterData)
    {
        isBuildEnemy = true;
        GameLevelBean gameLevelData = handler_Game.GetGameLevelData();
        while (isBuildEnemy)
        {
            CreateCharacter(enemyCharacterData);
            if (gameLevelData.enemy_build_interval <= 0)
            {
                gameLevelData.enemy_build_interval = 1;
            }
            yield return new WaitForSeconds(gameLevelData.enemy_build_interval);
        }
    }

    public IEnumerator CoroutineForEnemySpeedChange(CharacterDataBean enemyCharacterData)
    {
        isBuildEnemy = true;
        GameLevelBean gameLevelData = handler_Game.GetGameLevelData();
        while (isBuildEnemy)
        {
            yield return new WaitForSeconds(gameLevelData.enemy_speed_interval);
            float speed = enemyCharacterData.AddSpeed(gameLevelData.enemy_speed_incremental);
            SetCharacterSpeed(CharacterTypeEnum.Enemy, speed);
        }
    }

    public IEnumerator CoroutineForEnemyLifeChange(CharacterDataBean enemyCharacterData)
    {
        isBuildEnemy = true;
        GameLevelBean gameLevelData = handler_Game.GetGameLevelData();
        while (isBuildEnemy)
        {
            yield return new WaitForSeconds(gameLevelData.enemy_life_interval);
            enemyCharacterData.AddMaxLife(gameLevelData.enemy_life_incremental);
            SetCharacterLife(CharacterTypeEnum.Enemy, enemyCharacterData.maxLife);
        }
    }
}