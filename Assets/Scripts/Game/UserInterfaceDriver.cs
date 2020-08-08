using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceDriver : MonoBehaviour
{
  private EventBus bus;

  void Start()
  {
    bus = FindObjectOfType<EventBus>();
  }

  public void ShakePuzzle()
  {
    bus.FireScramblePuzzle();
  }

  public void ResetPuzzle()
  {
    bus.FireResetPuzzle();
  }
}
