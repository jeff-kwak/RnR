using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StateMachine<TState, TTrigger>
{
  private TState state;

  public class StateConfiguration
  {
    private readonly StateMachine<TState, TTrigger> target;
    private readonly TState state;

    public StateConfiguration(StateMachine<TState, TTrigger> target, TState state)
    {
      this.target = target;
      this.state = state;
    }

    public StateConfiguration Permit(TTrigger trigger, TState nextState)
    {
      if (!target.transtionTable.ContainsKey(state)) target.transtionTable[state] = new Dictionary<TTrigger, TState>();
      target.transtionTable[state][trigger] = nextState;
      return this;
    }

    public StateConfiguration OnEnter(Action action)
    {
      target.onEnterTable[state] = action;
      return this;
    }

    public StateConfiguration OnExit(Action action)
    {
      target.onExitTable[state] = action;
      return this;
    }
  }

  private IDictionary<TState, IDictionary<TTrigger, TState>> transtionTable;
  private IDictionary<TState, Action> onEnterTable;
  private IDictionary<TState, Action> onExitTable;

  public StateMachine(TState startState)
  {
    state = startState;
    transtionTable = new Dictionary<TState, IDictionary<TTrigger, TState>>();
    onEnterTable = new Dictionary<TState, Action>();
    onExitTable = new Dictionary<TState, Action>();
  }

  public StateConfiguration Configure(TState state)
  {
    return new StateConfiguration(this, state);
  }

  public void Start()
  {
    InvokeEnterAction();
  }

  public void Fire(TTrigger trigger)
  {
    if (!transtionTable.ContainsKey(state)) return;
    if (!transtionTable[state].ContainsKey(trigger)) return;

    if (onExitTable.ContainsKey(state)) onExitTable[state]();

    state = transtionTable[state][trigger];

    InvokeEnterAction();
  }

  private void InvokeEnterAction()
  {
    if (!onEnterTable.ContainsKey(state)) return;
    onEnterTable[state]();
  }
}
