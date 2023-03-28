using System;
using System.Collections.Generic;

using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private readonly Dictionary<EventType, List<Delegate>> _eventTable = new Dictionary<EventType, List<Delegate>>();

    private void RegisterEvent(EventType eventType, Delegate invokableAction)
    {
        if (_eventTable.TryGetValue(eventType, out List<Delegate> value))
        {
            value.Add(invokableAction);
            return;
        }

        value = new List<Delegate>();
        value.Add(invokableAction);
        _eventTable.Add(eventType, value);
    }

    private List<Delegate> GetActionList(EventType eventType)
    {
        if (_eventTable.TryGetValue(eventType, out var value))
        {
            return value;
        }

        return null;
    }

    private void CheckForEventRemoval(EventType eventType, List<Delegate> actionList)
    {
        if (actionList.Count == 0)
        {
            _eventTable.Remove(eventType);
        }
    }

    public void RegisterEvent(EventType eventType, Action action)
    {
        RegisterEvent(eventType, (Delegate)action);
    }

    public void RegisterEvent<T1>(EventType<T1> eventType, Action<T1> action)
    {
        RegisterEvent(eventType, (Delegate)action);
    }

    public void RegisterEvent<T1, T2>(EventType<T1, T2> eventType, Action<T1, T2> action)
    {
        RegisterEvent(eventType, (Delegate)action);
    }

    public void RegisterEvent<T1, T2, T3>(EventType<T1, T2, T3> eventType, Action<T1, T2, T3> action)
    {

        RegisterEvent(eventType, (Delegate)action);
    }

    public void RegisterEvent<T1, T2, T3, T4>(EventType<T1, T2, T3, T4> eventType, Action<T1, T2, T3, T4> action)
    {
        RegisterEvent(eventType, (Delegate)action);
    }

    public void RegisterEvent<T1, T2, T3, T4, T5>(EventType<T1, T2, T3, T4, T5> eventType, Action<T1, T2, T3, T4, T5> action)
    {
        RegisterEvent(eventType, (Delegate)action);
    }

    public void RegisterEvent<T1, T2, T3, T4, T5, T6>(EventType<T1, T2, T3, T4, T5, T6> eventType, Action<T1, T2, T3, T4, T5, T6> action)
    {
        RegisterEvent(eventType, (Delegate)action);
    }

    public void ExecuteEvent(EventType eventType)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action).Invoke();
        }
    }

    public void ExecuteEvent<T1>(EventType<T1> eventType, T1 arg1)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action<T1>).Invoke(arg1);
        }
    }

    public void ExecuteEvent<T1, T2>(EventType eventType, T1 arg1, T2 arg2)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action<T1, T2>).Invoke(arg1, arg2);
        }
    }
    public void ExecuteEvent<T1, T2, T3>(EventType<T1, T2, T3> eventType, T1 arg1, T2 arg2, T3 arg3)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action<T1, T2, T3>).Invoke(arg1, arg2, arg3);
        }
    }
    public void ExecuteEvent<T1, T2, T3, T4>(EventType<T1, T2, T3, T4> eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action<T1, T2, T3, T4>).Invoke(arg1, arg2, arg3, arg4);
        }
    }
    public void ExecuteEvent<T1, T2, T3, T4, T5>(EventType<T1, T2, T3, T4, T5> eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action<T1, T2, T3, T4, T5>).Invoke(arg1, arg2, arg3, arg4, arg5);
        }
    }

    public void ExecuteEvent<T1, T2, T3, T4, T5, T6>(EventType<T1, T2, T3, T4, T5, T6> eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        int num = 0;
        int count = actionList.Count;
        while (num < count)
        {
            if (count != actionList.Count)
            {
                num += actionList.Count - count;
                count = actionList.Count;
            }

            int index = actionList.Count - num - 1;
            num++;
            (actionList[index] as Action<T1, T2, T3, T4, T5, T6>).Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    public void UnregisterEvent(EventType eventType, Action action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    public void UnregisterEvent<T1>(EventType<T1> eventType, Action<T1> action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    public void UnregisterEvent<T1, T2>(EventType<T1, T2> eventType, Action<T1, T2> action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    public void UnregisterEvent<T1, T2, T3>(EventType<T1, T2, T3> eventType, Action<T1, T2, T3> action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    public void UnregisterEvent<T1, T2, T3, T4>(EventType<T1, T2, T3, T4> eventType, Action<T1, T2, T3, T4> action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    public void UnregisterEvent<T1, T2, T3, T4, T5>(EventType<T1, T2, T3, T4, T5> eventType, Action<T1, T2, T3, T4, T5> action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    public void UnregisterEvent<T1, T2, T3, T4, T5, T6>(EventType<T1, T2, T3, T4, T5, T6> eventType, Action<T1, T2, T3, T4, T5, T6> action)
    {
        List<Delegate> actionList = GetActionList(eventType);
        if (actionList == null)
        {
            return;
        }

        actionList.Remove(action);

        CheckForEventRemoval(eventType, actionList);
    }

    private void OnDisable()
    {
        if (!(base.gameObject != null) || base.gameObject.activeSelf)
        {
            ClearTable();
        }
    }

    private void OnDestroy()
    {
        ClearTable();
    }

    private void ClearTable()
    {
        _eventTable.Clear();
    }
}

public class EventType
{

}
public class EventType<T1> : EventType
{

}
public class EventType<T1, T2> : EventType
{

}
public class EventType<T1, T2, T3> : EventType
{

}
public class EventType<T1, T2, T3, T4> : EventType
{

}
public class EventType<T1, T2, T3, T4, T5> : EventType
{

}
public class EventType<T1, T2, T3, T4, T5, T6> : EventType
{

}