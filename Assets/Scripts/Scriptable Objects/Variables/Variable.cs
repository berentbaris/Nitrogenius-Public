using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents.Events;

public abstract class Variable<T> : ScriptableObject
{
    [field: SerializeField]
    public T Value { get; private set; }

    [SerializeField] private SimpleScriptableEvent OnValueChange;

    public void ResetValue(T amount)
    {
        Value = amount;
    }

    public void SetValue(T amount)
    {
        Value = amount;

        if (OnValueChange != null)
        {
            OnValueChange.Raise();
        }
    }
}