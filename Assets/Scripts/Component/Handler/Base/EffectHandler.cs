using UnityEditor;
using UnityEngine;
using System.Collections;
using System;

public class EffectHandler : BaseHandler<EffectManager>
{

    /// <summary>
    /// 播放粒子特效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="effectPosition"></param>
    public void PlayEffect(string name, Vector3 effectPosition,float delayTime)
    {
        GameObject objEffect = manager.CreateEffect(name);
        if (objEffect == null)
            return;
        objEffect.transform.position = effectPosition;
        ParticleSystem[] listParticleSystem = objEffect.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < listParticleSystem.Length; i++)
        {
            ParticleSystem particleSystem = listParticleSystem[i];
            ParticleSystem.MainModule psMain = particleSystem.main;
            psMain.loop = false;
            //psMain.stopAction = ParticleSystemStopAction.Callback;
            particleSystem.Play();
        }
        StartCoroutine(CoroutineForDelayDestroy(objEffect,delayTime));
    }

    public void PlayEffect(string name, Vector3 effectPosition)
    {
        PlayEffect(name, effectPosition, 5);
    }

    public IEnumerator CoroutineForDelayDestroy(GameObject objEffect, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(objEffect);
    }

}