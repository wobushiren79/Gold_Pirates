using UnityEditor;
using UnityEngine;

public class CharacterAnimCpt : BaseMonoBehaviour
{
    public Animator characterAnim;

    public void SetCharacterStand()
    {
        characterAnim.Play("stand");
    }

    public void SetCharacterRun()
    {
        characterAnim.Play("run");
    }

    public void SetCharacterWalk()
    {
        characterAnim.Play("walk");

    }

    public void SetCharacterThrow()
    {
        characterAnim.Play("throw");
    }

}