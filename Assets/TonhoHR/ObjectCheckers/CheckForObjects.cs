using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TonhoHR.ObjectCheckers
{ 
public class CheckForObjects<T>
{
    private TriggerNotifier notifier;

    public CheckForObjects(TriggerNotifier notifier)
    {
        this.notifier = notifier;
        objects = new List<T>();

        notifier.ObjectEntered += Notifier_ObjectEntered;
        notifier.ObjectLeft += Notifier_ObjectLeft;
    }


    private List<T> objects;

    public IEnumerable<T> CurrentObjects { get { return objects.AsReadOnly(); } }
    public bool HasAny { get { return objects.Count > 0; } }
    public T First { get { return objects[0]; } }
    private bool IsTypeComponent { get{ return typeof(T).IsSubclassOf(typeof(Component)); } }
    private bool IsTypeInterface { get { return typeof(T).IsInterface; } }

    public event Action<T> ObjectEntered;
    public event Action<T> ObjectLeft;


    private void Notifier_ObjectLeft(GameObject obj)
    {
        if (IsTypeComponent || IsTypeInterface)
        {
            var newObj = obj.GetComponent<T>();
            if (!ReferenceEquals(newObj, null))
            {
                OnObjectLeft(newObj);
                return;
            }
        }

        var proxy = obj.GetComponent<IProxyFor<T>>();
        if (!ReferenceEquals(proxy, null))
        {
            Debug.Assert(proxy.Owner != null);
            OnObjectLeft(proxy.Owner);
            return;
        }
    }

    private void OnObjectLeft(T newObj)
    {
        objects.Remove(newObj);
        if (ObjectLeft != null)
            ObjectLeft(newObj);
    }

    private void Notifier_ObjectEntered(GameObject obj)
    {
        if (IsTypeComponent || IsTypeInterface)
        {
            var newObj = obj.GetComponent<T>();
            if (newObj != null)
            {
                OnObjectEntered(newObj);
                return;
            }
        }

        var proxy = obj.GetComponent<IProxyFor<T>>();
        if (proxy != null)
        {
            Debug.Assert(proxy.Owner != null);
            OnObjectEntered(proxy.Owner);
            return;
        }
        
    }

    private void OnObjectEntered(T newObj)
    {
        objects.Add(newObj);
        if (ObjectEntered != null)
            ObjectEntered(newObj);
    }
}
    }