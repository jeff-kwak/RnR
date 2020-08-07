using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CinemachineFreeLookChangeAxis : MonoBehaviour
{
  private CinemachineFreeLook cam;

  void Start()
  {
    cam = GetComponent<CinemachineFreeLook>();
  }

  public void SetCameraAxis(float x, float y)
  {
    cam.m_XAxis.m_InputAxisValue = x;
    cam.m_YAxis.m_InputAxisValue = y;
  }
}
