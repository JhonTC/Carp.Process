using System;
using UnityEngine;

public interface IDynamicSelector
{
    public void OnClickHit(RaycastHit hit);
}

public class Selector<T> : IDynamicSelector
{
    public Action<T> OnTypeSelected;

    public Selector() {}

    public virtual void OnClickHit(RaycastHit hit)
    {
        T component = hit.collider.GetComponent<T>();
        if (CanInvoke(component))
        {
            OnTypeSelected?.Invoke(component);
        }
    }

    public virtual bool CanInvoke(T component)
    {
        return component != null;
    }
}
