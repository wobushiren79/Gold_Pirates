using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : BaseHandler<CameraManager>
{

    public void ShakeCamera()
    {
        manager.ShakeCamera();
    }
}
