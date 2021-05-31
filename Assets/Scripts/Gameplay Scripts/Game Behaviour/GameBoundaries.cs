using UnityEngine;

public class GameBoundaries : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    private static Vector2 bottomPosition, topPosition;
    
    public static float Y_TopBoundarie => topPosition.y;
    public static float Y_BottomBoundarie => bottomPosition.y;

    private void Awake()
    {
        GetGameBoundaries();
    }

    void GetGameBoundaries()
    {
        bottomPosition = GetWorldPositionFromCamera(Vector2.zero);
        topPosition = GetWorldPositionFromCamera(ScreenSize);
    }
    Vector2 GetWorldPositionFromCamera(Vector2 targetPosition)
    {
        return GameCamera().ScreenToWorldPoint(targetPosition);
    }

    Vector2 ScreenSize => new Vector2(Screen.width, Screen.height);

    Camera GameCamera()
    {
        if (!targetCamera)
            return Camera.main;

        return targetCamera;
    }
}