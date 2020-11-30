using UnityEditor;
using UnityEngine;

public class CharacterLifeCpt : BaseMonoBehaviour
{
    public TextMesh tvLife;

    private void Update()
    {
        tvLife.transform.LookAt(Camera.main.transform.position);
        tvLife.transform.rotation = Quaternion.Slerp(tvLife.transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - tvLife.transform.position), 10 * Time.deltaTime);
    }


    public void SetLife(int maxLife, int currentLife)
    {
        tvLife.text = currentLife+"/"+ maxLife;
    }
}