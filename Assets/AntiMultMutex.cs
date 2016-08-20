using UnityEngine;
using System.Collections;
using System.Threading;

public class AntiMultMutex : MonoBehaviour
{
    private Mutex hObject;
	void Start ()
	{
	    bool createdNew = false;
        hObject = new Mutex(true, "xianjian20160816", out createdNew);
        if (!createdNew)
	    {
	        Debug.Log("XYZ ========= MUTEX Has already exist");
	        hObject.Close();
	        Application.Quit();
	    }
	}

    void OnDestroy()
    {
        hObject.Close();
    }
}
