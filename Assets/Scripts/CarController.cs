using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Movement")]
    public float moveSpeed = 10f; // Speed of car movement (left & right)
    public float maxX = 3f; // Maximum movement limit on X-axis

    [Header("Car Tilt Effect")]
    public float tiltAngle = 15f; // Angle of car tilt when turning
    public float tiltSpeed = 5f; // Speed of tilt adjustment

    [Header("Wheel Rotation")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;
    public float wheelTurnAngle = 30f; // Angle of front wheels turning
    public float wheelSpinSpeed = 500f; // Speed of wheel spinning effect

    private void Update()
    {
        MoveCar(); // Call movement function every frame
    }

    void MoveCar()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get player input for left/right movement

        // Calculate new X position while clamping it within allowed limits
        float newX = Mathf.Clamp(transform.position.x + moveInput * moveSpeed * Time.deltaTime, -maxX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Apply car tilt effect based on movement
        float tilt = moveInput * -tiltAngle;
        Quaternion targetRotation = Quaternion.Euler(0, 0, tilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

        // Rotate front wheels based on movement direction
        float wheelRotation = moveInput * wheelTurnAngle;
        frontLeftWheel.localRotation = Quaternion.Euler(0, wheelRotation, 0);
        frontRightWheel.localRotation = Quaternion.Euler(0, wheelRotation, 0);

        // Spin all four wheels to simulate motion
        float spinRotation = wheelSpinSpeed * Time.deltaTime;
        frontLeftWheel.Rotate(spinRotation, 0, 0, Space.Self);
        frontRightWheel.Rotate(spinRotation, 0, 0, Space.Self);
        rearLeftWheel.Rotate(spinRotation, 0, 0, Space.Self);
        rearRightWheel.Rotate(spinRotation, 0, 0, Space.Self);
    }
}