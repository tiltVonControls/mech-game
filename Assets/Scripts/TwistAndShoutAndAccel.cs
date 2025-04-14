using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TwistAndShoutAndAccel : MonoBehaviour
{
    [SerializeField] KeyCode RotateLeftKey;

    [SerializeField] KeyCode RotateRightKey;

    [SerializeField] KeyCode MovementPositiveKey;

    [SerializeField] KeyCode MovementNegativeKey;
    public Rigidbody Rigidbody;
    public float Acceleration;
    public float TurnRate;

    private float Speed;
    private float Angle;

    private void Update()
    {
        //Updated, at long last mostly workable, movement script! Many thanks to JCatSandwichTS!
        // Get user inputs.
        Angle += GetRotation();
        Speed += GetAcceleration();

        // Rotate the body to match the new direction.
        transform.rotation = Quaternion.Euler(0, Angle * Mathf.Rad2Deg, 0);

        // Convert the new angle to a unit vector, then apply speed.
        //Rigidbody.linearVelocity = new Vector3(Mathf.Cos(Angle + Mathf.PI / 2), 0, Mathf.Sin(Angle + Mathf.PI / 2)) * Speed;
        
        //Many thanks to James Dupuis for help with this fix!
        Rigidbody.linearVelocity = Speed * gameObject.transform.forward;



    }

    private float GetRotation()
    {
        float diff = 0f;

        if (Input.GetKey(RotateLeftKey))
        {
            diff -= 1;
        }

        if (Input.GetKey(RotateRightKey))
        {
            diff += 1;
        }

        return diff * TurnRate * Time.deltaTime;
    }

    private float GetAcceleration()
    {
        float diff = 0f;

        if (Input.GetKey(MovementPositiveKey))
        {
            diff += 1;
        }

        if (Input.GetKey(MovementNegativeKey))
        {
            diff -= 1;
        }

        // If cancelling momentum, give more authority.
        if (Mathf.Sign(diff) != Mathf.Sign(Speed))
        {
            diff *= 2;
        }

        return diff * Acceleration * Time.deltaTime;
    }
}


//using UnityEngine;

//public class TwistAndShoutAndAccel : MonoBehaviour
//{
//    private Vector3 _velocity = Vector3.zero; // Velocity starts at 0 and is manipulated via "acceleration"

//    [SerializeField] private Vector3 _acceleration; // Acceleration values for x and z axes
//    private Vector3 _currentAcceleration = Vector3.zero; // This is the "acceleration" that will be added to the velocity
//    [SerializeField] private KeyCode LeftRotateKey;
//    [SerializeField] private KeyCode RightRotateKey;
//    [SerializeField] private float rotationSpeed; // Speed of rotation

//    void Update()
//    {
//        // ACCELERATION:
//        // Check for input and set acceleration accordingly
//        if (Input.GetKey(KeyCode.W))
//        {
//            _currentAcceleration = transform.forward * _acceleration.z; // Acceleration along the forward direction
//        }
//        else if (Input.GetKey(KeyCode.S))
//        {
//            _currentAcceleration = -transform.forward * _acceleration.z; // Acceleration along the backward direction
//        }
//        else
//        {
//            _currentAcceleration = Vector3.zero; // Maintain velocity while no key is pressed
//        }

//        // ROTATION:
//        // Calculate rotation amount using rotationSpeed set in the project
//        float rotationAmount = rotationSpeed * Time.deltaTime;

//        if (Input.GetKey(LeftRotateKey)) // Rotate left
//        {
//            transform.Rotate(new Vector3(0, 1, 0), -rotationAmount);
//        }

//        if (Input.GetKey(RightRotateKey)) // Rotate right
//        {
//            transform.Rotate(new Vector3(0, 1, 0), rotationAmount);
//        }

//        // Update velocity based on current acceleration
//        _velocity += _currentAcceleration * Time.deltaTime;

//        // Apply velocity to object position
//        transform.position += _velocity * Time.deltaTime;
//    }
//}