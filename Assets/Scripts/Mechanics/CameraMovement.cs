using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform; // The player to follow
    public float minXValue;           // Minimum X boundary
    public float maxXValue;           // Maximum X boundary
    public float followSpeed = 5f;    // Speed of camera movement

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not set on CameraMovement.");
        }
    }

    private void Update()
    {
        if (!playerTransform) return;

        // Get the current camera position
        Vector3 targetPosition = transform.position;

        // Update the X position with clamping
        targetPosition.x = Mathf.Clamp(playerTransform.position.x, minXValue, maxXValue);
        targetPosition.y = Mathf.Clamp(playerTransform.position.y,-200,200);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}