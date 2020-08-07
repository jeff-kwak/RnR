using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shake : MonoBehaviour
{
  [Tooltip("How hard does the shake.")]
  [Range(1f, 10f)]
  public float Strength = 5f;

  [Tooltip("How long does it shake in seconds")]
  [Range(1f, 10f)]
  public float ShakeTime = 2f;

  [Tooltip("What to shake by tag")]
  public string ShakeWhat;


  private bool isShaking = false;
  private float shakeTimer = 0f;
  private Rigidbody[] shakeables;

  public void StartShaking()
  {
    isShaking = true;
    shakeTimer = 0f;

    shakeables = GameObject.FindGameObjectsWithTag(ShakeWhat)
      .Select(go => go.GetComponent<Rigidbody>())
      .Where(rb => rb != null).ToArray();
  }

  private void FixedUpdate()
  {
    if (shakeTimer > ShakeTime) StopShaking();
    if (!isShaking) return;

    shakeTimer += Time.fixedDeltaTime;

    foreach (var s in shakeables)
    {
      var direction = Random.value > 0.5f ?
        -1 * s.velocity + Random.onUnitSphere :
        Random.onUnitSphere;

      s.AddForce(direction.normalized * Random.Range(0f, Strength), ForceMode.Impulse);
    }

  }

  public void StopShaking()
  {
    isShaking = false;
    shakeTimer = 0f;
  }
}
