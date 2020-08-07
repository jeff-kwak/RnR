using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLookChangeAxis))]
[RequireComponent(typeof(CinemachineFreeLookZoom))]
public class PlayerController : MonoBehaviour
{
  public float ZoomStep = 0.1f;

  private CinemachineFreeLookZoom zoom;
  private CinemachineFreeLookChangeAxis axis;
  private EventBus bus;

  void Start()
  {
    zoom = GetComponent<CinemachineFreeLookZoom>();
    axis = GetComponent<CinemachineFreeLookChangeAxis>();
    bus = FindObjectOfType<EventBus>();
  }

  public void OnOrbit(InputValue value)
  {
    var inputVector = value.Get<Vector2>();
    axis.SetCameraAxis(inputVector.x, inputVector.y);
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