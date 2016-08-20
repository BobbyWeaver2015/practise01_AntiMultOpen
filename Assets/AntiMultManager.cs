using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Text;

public class AntiMultManager : MonoBehaviour
{
	void Start ()
	{
	    Debug.Log(" XYZ ====== " + Application.persistentDataPath);
	    string str = "Login in";
	    string filePath = Application.persistentDataPath + "/AntiMultTest.txt";
	    ReadFile(filePath, FileAccess.Read, FileShare.Read);
	    WriteFile(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
	}

    static void ReadFile(string filePath, FileAccess fileAccess, FileShare fileShare)
    {
        if (!File.Exists(filePath))
            return;
        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, fileAccess, fileShare);
            fs.Dispose();
            fs.Close();
        }
        catch (Exception e)
        {
            Debug.Log("XYZ ======== READ :" + e.ToString());
            Application.Quit();
            throw;
        }
    }

    static void WriteFile(string filePath, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
    {
        try
        {
            FileStream fs = new FileStream(filePath, fileMode, fileAccess, fileShare);
            var buffer = Encoding.Default.GetBytes("");
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
        }
        catch (Exception e)
        {
            Debug.Log("XYZ ======== WRITE :" + e.ToString());
            Application.Quit();
            throw;
        }
    }
}
