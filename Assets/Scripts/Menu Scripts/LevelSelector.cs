using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public void SelectLevelToPlay(ObstacleLevelData levelToRegister) 
    {
        int idLevel = levelToRegister.GetLevelID();

        PlayersDataManager.SetNewLevelIDToPlay(idLevel);
    }
}
