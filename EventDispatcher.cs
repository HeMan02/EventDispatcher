using System;
using System.Collections.Generic;
using System.Linq;

public class EventDispatcher
{
    private Dictionary<IEvents.TypesEvents, Action<object>> _actions = new Dictionary<IEvents.TypesEvents, Action<object>>();
    #region API
    public void SetAction<T>(T value, IEvents.TypesEvents events)
    {
        object objectValue = value as object;

        if (_actions.Keys.Contains(events))
            _actions[events].Invoke(objectValue);
    }

    public void UnregisterAction<T>(Action<T> a, IEvents.TypesEvents events)
    {
        Action<object> actionCast = new Action<object>((o) => a((T)o));

        if (_actions.ContainsKey(events))
            _actions[events] -= actionCast;
    }

    public void RegisterAction<T>(Action<T> a, IEvents.TypesEvents events)
    {
        Action<object> actionCast = new Action<object>((o) => a((T)o));  

        if (_actions.ContainsKey(events))
            _actions[events] += actionCast;
        else
            _actions.Add(events, actionCast);
    }
    #endregion
}

