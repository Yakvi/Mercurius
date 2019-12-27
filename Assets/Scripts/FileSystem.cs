using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileSystem 
{
    private static string pathPrefix = Application.persistentDataPath + "/";

    public static void CreateWriteBin<T>(string filename, T data)
    {
        var filePath = pathPrefix + filename;
        var binaryFormatter = new BinaryFormatter();
        FileStream file;
        if (!File.Exists(filePath))
        {
            file = File.Create(filePath);
        }
        else
        {
            file = File.OpenWrite(filePath);
        }
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public static T ReadBin<T>(string filename)
    {
        // Debug.Log("reading file " + filename + " from " + pathPrefix);
        var filePath = pathPrefix + filename;

        T result = default(T);
        
        if (File.Exists(filePath))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.OpenRead(filePath);
            if (file.Length > 0)
            {
                result = (T) binaryFormatter.Deserialize(file);
                file.Close();
            }
        }

        return result;
    }
}
