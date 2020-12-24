using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraHandler : BaseHandler<CameraManager>
{
    public GameDataHandler handler_GameData;

    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2)  
    protected float speedScale = 1;
    protected float minFov;
    protected float maxFov;

    private void Start()
    {
        handler_GameData.GetCameraData(out  minFov, out  maxFov, out speedScale);
    }

    private void Update()
    {
        HandleForScale();
    }

    public void ShakeCamera()
    {
        manager.ShakeCamera();
    }

    public float ChangeCameraFov(float fov)
    {
        if (fov < minFov)
            fov = minFov;
        if (fov > maxFov)
            fov = maxFov;
        return manager.ChangeCameraFov(fov);
    }

    public void HandleForScale()
    {
        //如果触碰到了UI
        if (Input.touchCount != 2)
            return;
        //if (Input.touchCount != 2
        //    || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
        //    || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
        //    return;
        //多点触摸, 放大缩小  
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        //第2点刚开始接触屏幕, 只记录，不做处理  
        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差，为正表示放大手势， 为负表示缩小手势  
        float offset = newDistance - oldDistance;

        //进行缩放
        float scaleFactor = offset * speedScale * Time.deltaTime;
        ChangeCameraFov(manager.GetCameraFov() - scaleFactor);

        //记住最新的触摸点，下次使用  
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }
}
