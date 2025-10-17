using System;
using System.Collections.Generic;

public class Fsm 
{
    private FsmState stateCurrent { get;set;}

    private Dictionary<Type, FsmState> states = new Dictionary<Type, FsmState>();

    public void AddState(FsmState state)
    {
        states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);
        if(stateCurrent != null && stateCurrent.GetType() == type)
        {
            return;
        }
        if(states.TryGetValue(type, out var newState))
        {
            stateCurrent?.Exit();
            stateCurrent = newState;
            stateCurrent.Enter();
        }
    }

    public void Start()
    {
        stateCurrent?.Start();
    }

    public void Update()
    {
        stateCurrent?.Update();
    } 

    public void FixedUpdate()
    {
        stateCurrent?.FixedUpdate();
    }

}
