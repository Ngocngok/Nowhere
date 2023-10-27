using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentRepository : MonoBehaviour
{
    private readonly List<Component> cachedComponents = new List<Component>();

    public T GetCachedComponent<T>()
    {
        foreach (Component cachedComponent in cachedComponents)
        {
            if(cachedComponent is T t)
            {
                return t;
            }
        }

        T component = GetComponentInChildren<T>();
        if (component != null)
        {
            cachedComponents.Add(component as Component);
            return component;
        }

        return default(T);
    }
    public T[] GetCachedComponents<T>()
    {
        T[] component = GetComponentsInChildren<T>();
        if (component.Length > 0)
        {
            return component;
        }

        return default;
    }

    public bool TryGetCachedComponent<T>(out T component)
    {
        foreach (Component cachedComponent in cachedComponents)
        {
            if (cachedComponent is T t)
            {
                component = t;
                return true;
            }
        }

        component = GetComponentInChildren<T>();
        if (component != null)
        {
            cachedComponents.Add(component as Component);
            return true;
        }

        return false;
    }

    public bool TryGetCachedComponents<T>(out T[] component)
    {
        //foreach (Component cachedComponent in cachedComponents)
        //{
        //    if (cachedComponent is T t)
        //    {
        //        component = (T)cachedComponent;
        //        return true;
        //    }
        //}

        component = GetComponentsInChildren<T>();
        if (component.Length > 0)
        {
            //cachedComponents.Add(component);
            return true;
        }

        return false;
    }
}
