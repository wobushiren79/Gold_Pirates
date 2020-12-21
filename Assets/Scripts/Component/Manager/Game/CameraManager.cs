using UnityEditor;
using UnityEngine;

public class CameraManager : BaseManager
{
    public Camera mainCamera;
    public Animator animForCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        animForCamera = GetComponent<Animator>();
    }


    public void  ShakeCamera()
    {
        animForCamera.Play("Shake", 0, 0);
        animForCamera.Update(0);
    }
}