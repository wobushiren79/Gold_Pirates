using UnityEditor;
using System.Collections;
using UnityEngine;

public class CharacterLifeCpt : BaseMonoBehaviour
{
    public TextMesh tvLife;

    private void Update()
    {
        if (tvLife.gameObject.activeSelf)
        {
            tvLife.transform.LookAt(Camera.main.transform.position);
            tvLife.transform.rotation = Quaternion.Slerp(tvLife.transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - tvLife.transform.position), 10 * Time.deltaTime);
        }
    }


    public void SetLife(int maxLife, int currentLife)
    {
        tvLife.text = currentLife+"/"+ maxLife;
    }

    public void ShowLife(float showTime)
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineForShowLife(showTime));
    }


    /// <summary>
    /// 携程-展示血量
    /// </summary>
    /// <param name="showTime"></param>
    /// <returns></returns>
    public IEnumerator CoroutineForShowLife(float showTime)
    {
        tvLife.gameObject.SetActive(true);
        yield return new WaitForSeconds(showTime);
        tvLife.gameObject.SetActive(false);
    }
}