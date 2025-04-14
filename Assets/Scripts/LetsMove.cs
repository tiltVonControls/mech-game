using UnityEngine;

public class LetsMove : MonoBehaviour
{
    private Vector3 _velocity = Vector3.zero;                                           // Velocity will always start at 0 and can only be manipulated via "acceleration"
    private Vector3 _acceleration = new Vector3(16, 0, 16);    // Acceleration values for x and z axes
    private Vector3 _currentAcceleration = Vector3.zero;                                // This is the "acceleration" that will eventually be added to the existing velocity

    void Update()
    {
        // Check for input and set acceleration accordingly
        if (Input.GetKey(KeyCode.W))
        {
            _currentAcceleration.z = _acceleration.z; // Positive acceleration on z-axis
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _currentAcceleration.z = -_acceleration.z; // Negative acceleration on z-axis
        }
        else
        {
            _currentAcceleration.z = 0f; // Maintain velocity while no key is pressed (ie before key is pressed, and after it's released)
        }

        if (Input.GetKey(KeyCode.D))
        {
            _currentAcceleration.x = _acceleration.x; // Positive acceleration on x-axis
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _currentAcceleration.x = -_acceleration.x; // Negative acceleration on x-axis
        }
        else
        {
            _currentAcceleration.x = 0f; // Maintain velocity
        }

        // Update velocity based on current acceleration!
        _velocity += _currentAcceleration * Time.deltaTime;

        // Apply velocity to object position!
        transform.position += _velocity * Time.deltaTime;
    }
}
