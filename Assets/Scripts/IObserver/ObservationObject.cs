using System.Collections.Generic;
using UnityEngine;

public abstract class ObservationObject : MonoBehaviour
{
    public List<IObserver> Observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        Observers.Remove(observer);
    }

    protected void NotifyObservers(IObserver.Events currentEvent)
    {
        foreach (IObserver observer in Observers)
        {
            observer.OnNotify(currentEvent);
        }
    }
}
