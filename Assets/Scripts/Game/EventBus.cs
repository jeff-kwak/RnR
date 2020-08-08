using UnityEngine;

public delegate void ScramblePuzzleHandler();
public delegate void ResetPuzzleHandler();

public class EventBus : MonoBehaviour
{
  public event ScramblePuzzleHandler OnScramblePuzzle;
  public event ResetPuzzleHandler OnResetPuzzle;

  public void FireScramblePuzzle()
  {
    OnScramblePuzzle?.Invoke();
  }

  public void FireResetPuzzle()
  {
    OnResetPuzzle?.Invoke();
  }
}
