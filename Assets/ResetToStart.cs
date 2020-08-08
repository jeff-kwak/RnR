using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToStart : MonoBehaviour
{
  private enum State
  {
    Normal,
    PreReset,
    InReset
  }

  private enum Trigger
  {
    ResetPuzzle,
    ArrivedAtDestination,
    Update
  }

  private StateMachine<State, Trigger> state;

  private Vector3 startPos;
  private Rigidbody rb;
  private EventBus bus;
  private bool isResetting;

  void Start()
  {
    state = new StateMachine<State, Trigger>(State.Normal);

    state.Configure(State.Normal)
      .Permit(Trigger.ResetPuzzle, State.InReset);

    state.Configure(State.PreReset)
      .OnEnter(PrepareRigidBodyForMovement)
      .Permit(Trigger.Update, State.InReset);

    state.Configure(State.InReset)
       .OnEnter(UpdateInReset)
       .OnExit(ResetRigidBody)
       .Permit(Trigger.Update, State.InReset)
       .Permit(Trigger.ArrivedAtDestination, State.Normal);

    startPos = transform.position;
    rb = GetComponent<Rigidbody>();
    bus = FindObjectOfType<EventBus>();
    bus.OnResetPuzzle += OnResetPuzzle;

    state.Start();
  }

  private void PrepareRigidBodyForMovement()
  {
    Debug.Log($"{gameObject.name} prepare for rigid body");
    rb.isKinematic = true;
  }

  private void ResetRigidBody()
  {
    Debug.Log($"{gameObject.name} reset rigid body");
    rb.isKinematic = false;
  }

  private void OnResetPuzzle()
  {
    Debug.Log($"{gameObject.name} to reset");
    state.Fire(Trigger.ResetPuzzle);
  }

  private void FixedUpdate()
  {
    state.Fire(Trigger.Update);
  }

  private void UpdateInReset()
  {
    Debug.Log($"{gameObject.name} moving {transform.position}, isKinematic: {rb.isKinematic}");
    transform.SetPositionAndRotation(startPos, Quaternion.identity);
    if ((transform.position - startPos).sqrMagnitude < 0.001f) state.Fire(Trigger.ArrivedAtDestination);
  }

}
