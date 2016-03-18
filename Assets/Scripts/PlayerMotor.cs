using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
	
	private Vector3 velocity = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	private Vector3 rotation = Vector3.zero;
	private Vector3 thrusterForce = Vector3.zero;

	[SerializeField]
	private float cameraRotationLimit = 85f;

	[SerializeField]
	private Camera cam;

	private Rigidbody rb;

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Gets a movement vector
	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}
	
	// Gets a rotation vector
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	// Gets a rotation vector for camera
	public void RotateCamera(float _cameraRotation)
	{
		cameraRotationX = _cameraRotation;
	}

    // Get a force vector for our thrusters
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    // Run every physics iteration
	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	// Perform movement based on velocity variable
	void PerformMovement()
	{
		if (velocity != Vector3.zero) 
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}

	    if (thrusterForce != Vector3.zero)
	    {
	        rb.AddForce(thrusterForce*Time.fixedDeltaTime, ForceMode.Acceleration);
	    }
	}

	// Perform Rotation

	void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if (cam != null)
		{
			// Set out rotation and clamp it
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			// Apply out rotation to the transform of our camera
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}




}
