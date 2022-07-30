using UnityEngine;

//https://answers.unity.com/questions/1714455/monosingleton-call-a-singleton-method.html
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;

    public static T instance
    {
	    get
	    {
		    // Instance required for the first time, we look for it
		    if (_instance == null)
		    {
			    Debug.LogError(typeof(T).ToString() + " is NULL.");
		    }

		    return _instance;
	    }
    }

    private void Awake()
    {
	    if (_instance != null && _instance != this)
	    {
		    Destroy(this.gameObject);
	    }
	    else
	    {
		    _instance = this as T;
	    }

	    Init();
    }

	public virtual void Init(){}
 
    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        _instance = null;
    }
}
