using UnityEditor;
using UnityEngine;
using System.Collections;

public class CharacterHandler : BaseHandler<CharacterManager>
{
    public Transform tfPlayerStartPosition;
    public Transform tfEnemyStartPosition;
    protected bool isStartCreate = false;
    protected int numberForEnemy = 2;

    public GameDataHandler handler_GameData;

    private void Start()
    {
        manager.InitCharacterData();
    }

    public void StartCreateCharacter()
    {
        isStartCreate = true;
        StartCoroutine(CoroutineForCreateCharacter());
    }

    public void StopCreateCharacter()
    {
        isStartCreate = false;
        StopAllCoroutines();
    }

    public void RefreshCharacter()
    {
        manager.RefreshPlayerCharacter();
    }

    public void CleanAllCharacter()
    {
        manager.CleanAllCharacter();
    }

    public IEnumerator CoroutineForCreateCharacter()
    {
        while (isStartCreate)
        {
         
            UserDataBean userData = handler_GameData.GetUserData();
            if (manager.GetPlayerCharacterNumber() < userData.pirateNumber)
            {
                CharacterDataBean characterDataForPlayer = new CharacterDataBean(CharacterTypeEnum.Player);
                manager.CreateCharacter(tfPlayerStartPosition.position, characterDataForPlayer);
            }
            if (manager.GetEnemyCharacterNumber() < numberForEnemy)
            {
                CharacterDataBean characterDataForPlayer = new CharacterDataBean(CharacterTypeEnum.Enemy);
                manager.CreateCharacter(tfEnemyStartPosition.position, characterDataForPlayer);
            }
            yield return new WaitForSeconds(1);
        }
    }

}