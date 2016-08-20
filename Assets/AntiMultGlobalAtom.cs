using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AntiMultGlobalAtom : MonoBehaviour
{

    [DllImport("Kernel32.dll")]
    private static extern uint GlobalAddAtom(string lpString);

    [DllImport("Kernel32.dll")]
    private static extern uint GlobalFindAtom(string lpString);

    [DllImport("Kernel32.dll")]
    private static extern uint GlobalDeleteAtom(uint nAtom);

    private uint nAtom = 0;
	void Start ()
	{
	    Debug.Log("XYZ ===== GlobalFindAtom before : " + GlobalFindAtom("ATOM_TEST"));
	    if (GlobalFindAtom("ATOM_TEST") == 0)
	    {
            nAtom = GlobalAddAtom("ATOM_TEST");
            Debug.Log("XYZ ===== GlobalFindAtom after : " + GlobalFindAtom("ATOM_TEST")); 
	    }
	    else
	    {
            Debug.Log("Game has already opened !!!!!");
            Application.Quit();
	    }
	}

    void OnDestroy()
    {
        if (nAtom != 0)
        {
            Debug.Log("XYZ ===== GlobalFindAtom final : " + GlobalFindAtom("ATOM_TEST"));
            GlobalDeleteAtom(nAtom);
            Debug.Log("XYZ ===== GlobalFindAtom final : " + GlobalFindAtom("ATOM_TEST"));
        }
    }
	
}
