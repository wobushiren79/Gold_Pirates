using UnityEditor;
using UnityEngine;

public class ShipAnimCpt : BaseMonoBehaviour
{
    protected Animator shipAnimator;
    public void InitAnim()
    {
        shipAnimator = GetComponentInChildren<Animator>();
    }

    public void SetShipFire()
    {
        PlayAnim("Fire");
    }
    public void PlayAnim(string stateName)
    {
        shipAnimator.Play(stateName,0,0);
    }
}