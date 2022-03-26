using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{

    public void ReadFromFile(string filename)
    {
        var path = GetPathWithFile(filename);
        var file = File.ReadAllText(path);
        ItemsParameters itemParameters = JsonUtility.FromJson<ItemsParameters>(file);

        foreach (string item in itemParameters.names)
        {
            Debug.Log("Found employee: " + item);
        }
    }


    private string GetPathWithFile(string filename)
    {
        return GetPath() + "/" + filename + ".data";
    }

    private string GetPath()
    {
        return Application.streamingAssetsPath;
    }
}
