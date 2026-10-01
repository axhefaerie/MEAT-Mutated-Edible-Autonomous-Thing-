using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    private List<IObserver> observers = new();

    public void AddObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObservers(int lifes, int collected)
    {
        for (int i = observers.Count - 1; i >= 0; i--)
        {
            if (observers[i] == null)
            {
                observers.RemoveAt(i);
                continue;
            }

            observers[i].OnNotify(lifes, collected);
        }
    }

}
