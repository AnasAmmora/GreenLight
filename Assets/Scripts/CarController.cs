using UnityEngine;

public class CarController : MonoBehaviour
{           
    [Header("Car Movement")]
    public float moveSpeed = 10f; 
    public float maxX = 3f;
    public float forwardOffset = 1.5f;
    public float returnSpeed = 2f; 

    [Header("Car Tilt Effect")]
    public float tiltAngle = 15f; 
    public float tiltSpeed = 5f; 

    [Header("Wheel Rotation")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;
    public float wheelTurnAngle = 30f;
    public float wheelSpinSpeed = 500f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveCar();
    }

    void MoveCar()
    {
        float moveInput = Input.GetAxis("Horizontal");


        float newX = Mathf.Clamp(transform.position.x + moveInput * moveSpeed * Time.deltaTime, -maxX, maxX);
        float forwardMove = Mathf.Abs(moveInput) > 0 ? forwardOffset : 0; 
        float newZ = Mathf.Lerp(transform.position.z, startPosition.z + forwardMove, Time.deltaTime * returnSpeed);

        transform.position = new Vector3(newX, transform.position.y, newZ);


        float tilt = moveInput * -tiltAngle;
        Quaternion targetRotation = Quaternion.Euler(0, 0, tilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);


        float wheelRotation = moveInput * wheelTurnAngle;
        frontLeftWheel.localRotation = Quaternion.Euler(0, wheelRotation, 0);
        frontRightWheel.localRotation = Quaternion.Euler(0, wheelRotation, 0);

       
        float spinRotation = wheelSpinSpeed * Time.deltaTime;
        frontLeftWheel.Rotate(spinRotation, 0, 0, Space.Self);
        frontRightWheel.Rotate(spinRotation, 0, 0, Space.Self);
        rearLeftWheel.Rotate(spinRotation, 0, 0, Space.Self);
        rearRightWheel.Rotate(spinRotation, 0, 0, Space.Self);
    }
}
