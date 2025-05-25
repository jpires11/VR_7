using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System.Collections.Generic;

public class VRButtonLogger : MonoBehaviour
{
    private InputDevice rightController;

    void Start()
    {
        // 获取右手控制器
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void Update()
    {
        if (rightController.isValid)
        {
            bool primaryButtonValue = false;
            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
            {
                Debug.Log("VR 控制器按钮被点击！");
            }
        }
    }

    public void OnButtonClick()
    {
        Debug.Log("UI Button 被点击！");
    }
}