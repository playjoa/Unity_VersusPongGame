using UnityEngine;
using System.IO;
using PlayerDataSystem;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveLoadSystem
{
    public static class DataStorer
    {
        private static readonly string fileName = "PlayersData.data";

        public static void SaveData(PlayersData dataToSave)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(SaveDataPath, FileMode.Create);

                binaryFormatter.Serialize(fileStream, dataToSave);

                fileStream.Close();
            }
            catch (System.Exception e)
            {
                Debug.Log("Error Saving Data:" + e);
                throw;
            }
        }

        public static PlayersData LoadData()
        {
            if (!SaveFileExists())
            {
                PlayersDataManager.CreateFirstTimeDataAndSave();
                return LoadData();
            }

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(SaveDataPath, FileMode.Open);
                PlayersData dataLoaded = binaryFormatter.Deserialize(fileStream) as PlayersData;

                fileStream.Close();

                return dataLoaded;
            }
            catch (System.Exception e)
            {
                Debug.Log("Error Loading Data:" + e);
                throw;
            }
        }

        static string SaveDataPath => Application.persistentDataPath + "/" + fileName;

        static bool SaveFileExists()
        {
            if (File.Exists(SaveDataPath))
                return true;

            return false;
        }
    }
}