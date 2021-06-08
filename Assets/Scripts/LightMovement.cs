using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    public struct CameraDirections
    {
        public bool horizontal { get; set; }
        public float hSpeed { get; set; }

        public bool vertical { get; set; }
        public float vSpeed { get; set; }



        public CameraDirections (bool horizontal, bool vertical, float hSpeed, float vSpeed)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.hSpeed = hSpeed;
            this.vSpeed = vSpeed;
        }
    }

    public bool movementAllowed;

    public float rotationSpeed = 22.0f;
    public float minRotationSpeed = 15.0f;
    public float maxRotationSpeed = 25.0f;

    public float maximumLifetime = 20.0f;

    public CameraDirections cameraDirections;
    public float horizontalLimit = 135.0f;
    public float verticalLimit = 75.0f;

    private float distanceTravelledY = 0.0f;
    private int horizontalDirection = 1;

    private float distanceTravelledX = 0.0f;
    private int verticalDirection = 1;

    private float hAngle, vAngle;
    private float currentLifetime = 0.0f;

    CameraDirections UpdateDirections()
    {
        CameraDirections newDirections = new CameraDirections(false, false, 0.0f, 0.0f);

        while (!newDirections.horizontal && !newDirections.vertical)
        {
            newDirections.horizontal = (Random.value > 0.5f);
            newDirections.vertical = (Random.value > 0.5f);
        }

        // Set new Speeds
        newDirections.vSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        newDirections.hSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        return newDirections;
    }

    private void Start()
    {
        if (movementAllowed)
        {
            cameraDirections = UpdateDirections();
        }
    }


    void Update()
    {
        if (cameraDirections.horizontal)
        {
            hAngle = Mathf.Sin(cameraDirections.hSpeed * Time.deltaTime) * horizontalDirection;
            transform.Rotate(0f, hAngle, 0f, Space.World);

            distanceTravelledY += hAngle;
            if (Mathf.Abs(distanceTravelledY) >= horizontalLimit)
            {
                horizontalDirection = -horizontalDirection;
            }
        }
        if (cameraDirections.vertical)
        {
            vAngle = Mathf.Sin(cameraDirections.vSpeed * Time.deltaTime) * verticalDirection;
            transform.Rotate(vAngle, 0f, 0f, Space.Self);

            distanceTravelledX += vAngle;
            if (Mathf.Abs(distanceTravelledX) >= verticalLimit)
            {
                verticalDirection = -verticalDirection;
            }
        }

        currentLifetime += Time.deltaTime;
        if (currentLifetime > maximumLifetime && movementAllowed)
        {
            cameraDirections = UpdateDirections();
            currentLifetime = 0.0f;
        }
    }
}
