using UnityEditor;
using UnityEngine;

public class CharacterLifeCpt : BaseMonoBehaviour
{
    public TextMesh tvLife;

    public CharacterDataBean CharacterData;

    private void Update()
    {
        tvLife.transform.LookAt(Camera.main.transform.position);
        tvLife.transform.rotation = Quaternion.Slerp(tvLife.transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - tvLife.transform.position), 10 * Time.deltaTime);
    }

    public void SetData(CharacterDataBean CharacterData)
    {
        this.CharacterData = CharacterData;
        SetLife(CharacterData.maxLife, CharacterData.life);
    }

    public void SetLife(int maxLife, int currentLife)
    {
        tvLife.text = currentLife+"";
    }
}