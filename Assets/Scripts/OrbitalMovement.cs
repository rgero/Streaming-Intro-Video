using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public struct Limit
    {
        public float lower { get; set; }
        public float upper { get; set; }

        public Limit(float lower, float upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }

    public Limit rotationLimits = new Limit(-22.0f, 22.0f);
    public Limit movementLimits = new Limit(1f, 3.0f);
    public Limit verticalLimits = new Limit(-12.0f, 12.0f);
    public Limit depthsLimits = new Limit(10.0f, 40.0f);
    public Limit angleLimits = new Limit(-45.0f, 45.0f);
    public Limit scaleLimits = new Limit(1.0f, 3.0f);
    public float widthValue = 15.0f;

    private float rotationSpeedY;
    private float rotationSpeedX;
    private float movementSpeed;

    private bool rotationDirectionX;
    private bool rotationDirectionY;
    private bool spawnSide;
    private Vector3 directionAngle;
    private float verticalStartPos;
    private float depthStartPos;

    void Start()
    {
        spawnSide = (Random.value > 0.5f);
        rotationDirectionX = (Random.value > 0.5f);
        rotationDirectionY = (Random.value > 0.5f);
        verticalStartPos = Random.Range(verticalLimits.lower, verticalLimits.upper);
        depthStartPos = Random.Range(depthsLimits.lower, depthsLimits.upper);
        this.transform.position = new Vector3((spawnSide ? widthValue : -widthValue), verticalStartPos, depthStartPos);

        float directionDegrees = Random.Range(angleLimits.lower, angleLimits.upper) * (spawnSide ? 1 : -1);
        directionAngle = SetAngle(directionDegrees);

        movementSpeed = Random.Range(movementLimits.lower, movementLimits.upper);
        rotationSpeedX = Random.Range(rotationLimits.lower, rotationLimits.upper);
        rotationSpeedY = Random.Range(rotationLimits.lower, rotationLimits.upper);

        this.transform.localScale = new Vector3(Random.Range(scaleLimits.lower, scaleLimits.upper),
                                                Random.Range(scaleLimits.lower, scaleLimits.upper),
                                                Random.Range(scaleLimits.lower, scaleLimits.upper));

    }

    Vector3 SetAngle(float dDegrees)
    {
        float xValue = Mathf.Cos(dDegrees);
        float yValue = Mathf.Sin(dDegrees);
        float zValue = 0; // for now just presume zero.

        return new Vector3(xValue, yValue, zValue).normalized;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = this.transform.position;
        currentPos = currentPos + directionAngle*movementSpeed*Time.deltaTime;
        this.transform.position = currentPos;

        // Process Rotation
        if (rotationDirectionX)
        {
            float hAngle = Mathf.Sin(rotationSpeedX* Time.deltaTime);
            transform.Rotate(0f, hAngle, 0f, Space.World);
        }

        if (rotationDirectionY)
        {
            float yAngle = Mathf.Sin(rotationSpeedY * Time.deltaTime);
            transform.Rotate(yAngle, 0f, 0f, Space.Self);
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
