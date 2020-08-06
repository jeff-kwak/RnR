using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLookZoom))]
public class PlayerController : MonoBehaviour
{
  public CinemachineFreeLook FreeLookCamera;
  public float ZoomStep = 0.1f;

  private CinemachineFreeLookZoom zoom;

  void Start()
  {
    zoom = GetComponent<CinemachineFreeLookZoom>();
  }

  void Update()
  {
    
  }

  public void OnOrbit(InputValue value)
  {
    var inputVector = value.Get<Vector2>();
    FreeLookCamera.m_XAxis.m_InputAxisValue = inputVector.x;
    FreeLookCamera.m_YAxis.m_InputAxisValue = inputVector.y;
  }

  
  public void OnZoom(InputValue value)
  {
    // scroll is just weird in the new input system.
    var scroll = value.Get<Vector2>().y;
    if (scroll == 0) return;
    if (scroll > 0) zoom.ZoomFactor += ZoomStep;
    if (scroll < 0) zoom.ZoomFactor -= ZoomStep;
  }
}