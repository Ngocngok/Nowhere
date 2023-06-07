using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentRepository : MonoBehaviour
{
    private readonly List<Component> cachedComponents = new List<Component>();

    public T GetCachedComponent<T>() where T : Component
    {
        foreach (Component cachedComponent in cachedComponents)
        {
            if(cachedComponent is T t)
            {
                return t;
            }
        }

        Component component = GetComponentInChildren<T>();
        if (component != null)
        {
            cachedComponents.Add(component);
            return (T)component;
        }

        return default(T);
    }
}
