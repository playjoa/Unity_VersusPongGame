using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoveUpDownMovement : MonoBehaviour
{
    [Header("-----Movement Speed Values-----")]
    [Range(1f, 10f)]
    [SerializeField]
    private float minSpeedMovement = 1f;

    [Range(1f, 10f)]
    [SerializeField]
    private float maxSpeedMovement = 1f;

    private float speed_Movement;

    private void Start()
    {
        CalculateValues();
    }

    private void Update()
    {
        ControlMovement();
    }


    void CalculateValues()
    {
        speed_Movement = Random.Range(minSpeedMovement, maxSpeedMovement);
        speed_Movement *= RandomDirection();
    }

    int RandomDirection()
    {
        if (Random.value >= 0.5f)
            return 1;

        return -1;
    }

    void ControlMovement()
    {
        if (isMovingOutDown(speed_Movement))
            ToggleSpeedDirection();

        if (isMovingOutUp(speed_Movement))
            ToggleSpeedDirection();

        transform.Translate(speed_Movement * Vector2.up * Time.deltaTime);
    }

    bool isMovingOutUp(float moveDir)
    {
        if (transform.localPosition.y > GameBoundaries.Y_TopBoundarie - ObstacleHeight() / 2 && moveDir > 0)
            return true;

        return false;
    }

    bool isMovingOutDown(float moveDir)
    {
        if (transform.localPosition.y < GameBoundaries.Y_BottomBoundarie + ObstacleHeight() / 2 && moveDir < 0)
            return true;

        return false;
    }

    void ToggleSpeedDirection()
    {
        speed_Movement *= -1;
    }

    float ObstacleHeight() 
    {
        return transform.localScale.y;
    }
}