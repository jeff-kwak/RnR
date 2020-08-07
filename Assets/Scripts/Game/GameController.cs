using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shake))]
public class GameController : MonoBehaviour
{
  private EventBus bus;
  private Shake shake;

  void Start()
  {
    shake = GetComponent<Shake>();
    bus = FindObjectOfType<EventBus>();
    bus.OnScramblePuzzle += OnScramblePuzzle;
  }

  private void OnScramblePuzzle()
  {
    shake.StartShaking();
  }
}
