using Cinemachine;
using System;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
class CinemachineFreeLookZoom : MonoBehaviour
{
  private CinemachineFreeLook cam;
  private CinemachineFreeLook.Orbit[] originalOrbits;
  private float currentZoom;


  [Tooltip("The desired zoom factor. Higher numbers mean the camera is zoomed in.")]
  public float ZoomFactor = 1.0f;

  [Tooltip("Max amount to zoom out. Lower numbers are further away.")]
  public float MaxZoomOut = 0.2f;

  [Tooltip("Max amount to zoom in. Higher numbers are closer.")]
  public float MaxZoomIn = 5f;

  [Tooltip("The camera speed for acheiving the zoom in units per second")]
  public float CameraSpeed = 2f;

  void Start()
  {
    cam = GetComponentInChildren<CinemachineFreeLook>();
    originalOrbits = new CinemachineFreeLook.Orbit[cam.m_Orbits.Length];
    for (int i = 0; i < cam.m_Orbits.Length; i++)
    {
      originalOrbits[i].m_Height = cam.m_Orbits[i].m_Height;
      originalOrbits[i].m_Radius = cam.m_Orbits[i].m_Radius;
    }

    SetCameraZoom(ZoomFactor);
  }

  void Update()
  {
    ZoomFactor = Mathf.Clamp(ZoomFactor, MaxZoomOut, MaxZoomIn);
    if (Mathf.Approximately(currentZoom, ZoomFactor)) return;

    //var zoom = currentZoom + ((ZoomFactor - currentZoom)/Math.Abs(ZoomFactor - currentZoom)) * Time.deltaTime * CameraSpeed;

    if (ZoomFactor > currentZoom) SetCameraZoom(currentZoom + CameraSpeed * Time.deltaTime);
    if (ZoomFactor < currentZoom) SetCameraZoom(currentZoom - CameraSpeed * Time.deltaTime);
  }

  private void SetCameraZoom(float zoomFactor)
  {
    var zoom = Mathf.Clamp(zoomFactor, MaxZoomOut, MaxZoomIn);
    for (int i = 0; i < cam.m_Orbits.Length; i++)
    {
      cam.m_Orbits[i].m_Height = originalOrbits[i].m_Height / zoom;
      cam.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius / zoom;
    }
    currentZoom = zoom;
  }
}

