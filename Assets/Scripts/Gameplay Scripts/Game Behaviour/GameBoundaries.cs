using UnityEngine;

public class GameBoundaries : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    private static Vector2 bottomPosition, topPosition;
    
    public static Vector2 TopBoundarie => topPosition;
    public static Vector2 BottomBoundarie => bottomPosition;

    private void Awake()
    {
        GetGameBoundaries();
    }

    void GetGameBoundaries()
    {
        bottomPosition = GetWorldPositionFromCamera(Vector2.zero);
        topPosition = GetWorldPositionFromCamera(ScreenSize);
    }

    Vector2 ScreenSize => new Vector2(Screen.width, Screen.height);

    Vector2 GetWorldPositionFromCamera(Vector2 targetPosition) 
    {
        return GameCamera().ScreenToWorldPoint(targetPosition);
    }

    Camera GameCamera()
    {
        if (!targetCamera)
            return Camera.main;

        return targetCamera;
    }
}