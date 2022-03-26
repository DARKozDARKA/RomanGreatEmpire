using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{

    public List<string> ReadFromFile(string filename)
    {
        var path = GetPathWithFile(filename);
        if (!File.Exists(path)) return null;
        var file = File.ReadAllText(path);
        ItemsParameters itemParameters = JsonUtility.FromJson<ItemsParameters>(file);

        var finalList = new List<string>();
        foreach (string item in itemParameters.names)
        {
            finalList.Add(item);
        }
        return finalList;
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
