/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    public static CameraMoveController Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void CameraPositionUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
*/