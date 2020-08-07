using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ScramblePuzzleHandler();

public class EventBus : MonoBehaviour
{
  public event ScramblePuzzleHandler OnScramblePuzzle;

  public void FireScramblePuzzle()
  {
    OnScramblePuzzle?.Invoke();
  }
}
