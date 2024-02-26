using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void SavePlayerName(string PlayerName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerName.Beephi";
        FileStream stream = new FileStream(path, FileMode.Create);

        string SavedName = new string(PlayerName);

        formatter.Serialize(stream, SavedName);
        stream.Close();
    }

    public static string LoadName()
    {

        string path = Application.persistentDataPath + "/PlayerName.Beephi";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveBuild(SavedBuildData data)
    {
        string path = Application.persistentDataPath + "/SavedBuild.Beephi";
        if (!File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            SavedBuildData SavedBuild = new SavedBuildData(data);

            formatter.Serialize(stream, SavedBuild);
            stream.Close();
        }
        else
        {
            File.Delete(path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            SavedBuildData SavedBuild = new SavedBuildData(data);

            formatter.Serialize(stream, SavedBuild);
            stream.Close();
        }

       
    }

    public static SavedBuildData LoadBuild()
    {

        string path = Application.persistentDataPath + "/SavedBuild.Beephi";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedBuildData data = formatter.Deserialize(stream) as SavedBuildData;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
