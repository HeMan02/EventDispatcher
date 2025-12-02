using System;
using System.Collections.Generic;

public class EventDispatcher
{
    private Dictionary<object, List<Delegate>> _actions = new Dictionary<object, List<Delegate>>();
    #region API
    public void SetAction<T>(object key, T ev) where T : IEvents
    {
        if (_actions.TryGetValue(key, out var del))
        {
            for (int i = 0; i < del.Count; i++)
            {
                Action<T> action = del[i] as Action<T>;

                if (action == null)
                    continue;

                if (action.GetType() == typeof(Action<T>))
                    action?.Invoke(ev);
            }
        }
    }

    public void RegisterAction<T>(object key, Action<T> a) where T : IEvents
    {
        if (_actions.TryGetValue(key, out var value))
        {
            if (!value.Contains(a))
                _actions[key].Add(a);
        }
        else
        {
            _actions[key] = new List<Delegate> { a };
        }
    }

    public void UnregisterAction<T>(object key, Action<T> a) where T : IEvents
    {
        if (_actions.TryGetValue(key, out var value))
        {
            if (value.Contains(a))
                value.Remove(a);

            if (value is null)
                _actions.Remove(key);
            else
                _actions[key] = value;
        }
    }
    #endregion
}

