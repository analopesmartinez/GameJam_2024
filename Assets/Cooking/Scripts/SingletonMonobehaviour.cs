
using UnityEngine;

public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    //T as private member data type 
    private static T instance;

    //T as return type of property
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// protected - method only called by inherited classes
    /// virtual - can be overriden
    /// If the static field instance isn't already populated, then we set whatever class the awake method is being called in 
    /// and we assign that class to the static field. If its already populated, we destroy the whole gameobject that class is attached to. 
    /// </summary>

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
