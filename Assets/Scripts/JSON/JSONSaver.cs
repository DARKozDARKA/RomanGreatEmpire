using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class JSONSaver : MonoBehaviour
{
    [SerializeField] private string SavePath => GetPath();

    public void SaveToFile(ItemsParameters parameters)
    {
        string json = JsonUtility.ToJson(parameters, true);

        if (!Directory.Exists(GetPath()))
            Directory.CreateDirectory(GetPath());
        File.WriteAllText(GetPathWithFile(parameters.filename), json);
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

[System.Serializable]
public struct ItemsParameters
{
    public string filename;
    public List<string> names;

    public ItemsParameters(string filename, List<string> names)
    {
        this.filename = filename;
        this.names = names;
    }
}