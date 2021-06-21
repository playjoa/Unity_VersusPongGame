using UnityEngine;

public static class PlayerPrefsBool
{
    public static void SetBool(string playerPrefID, bool valueToSet)
    {
        if (!valueToSet)
        {
            PlayerPrefs.SetInt(playerPrefID, 0);
            return;
        }
        PlayerPrefs.SetInt(playerPrefID, 1);
    }

    public static bool GetBool(string playerPrefID)
    {
        return PlayerPrefs_IntToBool(playerPrefID);
    }

    public static bool GetBool(string playerPrefID, bool startingValue)
    {
        if(!PlayerPrefs.HasKey(playerPrefID))
        {
            SetBool(playerPrefID, startingValue);
            return true;
        }

        return PlayerPrefs_IntToBool(playerPrefID);
    }

    static bool PlayerPrefs_IntToBool(string playerPrefID)
    {
        if (PlayerPrefs.GetInt(playerPrefID) == 0)
            return false;

        return true;
    }
}
