using UnityEditor;
using System.Collections;
using UnityEngine;

public class CharacterLifeCpt : BaseMonoBehaviour
{
    public SpriteRenderer srLife;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.LookAt(Camera.main.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - transform.position), 10 * Time.deltaTime);
        }
    }


    public void SetLife(int maxLife, int currentLife)
    {
        if (maxLife == 0)
            maxLife = 1;
        float lifeRate = (float)currentLife / maxLife;
        srLife.size = new Vector2(0.73f * lifeRate, srLife.size.y);
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
        gameObject.SetActive(true);
        yield return new WaitForSeconds(showTime);
        gameObject.SetActive(false);
    }
}