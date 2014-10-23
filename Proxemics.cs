using UnityEngine;
using System.Collections;

public class Proxemics : MonoBehaviour {

	public float distance = 1.5f;
	public float fastWalkDistanceMultiplier = 4.0f;
	public float tolerance = 0.5f;
	public float angleTolerance = 5f;
	public float speedTolerance = 0.5f;
	public float walkingSpeed = 0.5f;
	public float fastWalkingSpeed = 1.5f;
	private bool moving = false;
	private float speed = 0f;

	public Transform target;
	private Animator anim;
	private AnimatorSetup animSetup;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		// Creating an instance of the AnimatorSetup class and calling it's constructor.
		animSetup = new AnimatorSetup(anim);

		angleTolerance *= Mathf.Deg2Rad;
	}

	void OnAnimatorMove ()
	{
		// The gameobject's rotation is driven by the animation's rotation.
		transform.rotation = anim.rootRotation;
		transform.position = anim.rootPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float realTolerance = tolerance;
		if(moving){
			realTolerance *= 0.5f;
		}
		float minDistance = distance - realTolerance;
		float maxDistance = distance + realTolerance;
		float fastWalkDistance = distance + fastWalkDistanceMultiplier*realTolerance;
		Vector3 myPos = transform.position - Vector3.Project(transform.position, transform.up);
		Vector3 targetPos = target.position - Vector3.Project(target.position, transform.up);
		//Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);
		//Vector3 targetPos = new Vector3(target.position.x, 0f, target.position.z);

		float dist = Vector3.Distance(myPos, targetPos);
		float angle = FindAngle(transform.forward, targetPos - myPos, transform.up);
		if(angle > Mathf.PI){
			angle -= Mathf.PI;
		}
		Debug.Log(myPos + " " + targetPos);
		//Debug.Log("angle: " + angle);
		Debug.Log("dist: " + dist);
		Vector3 difference = targetPos - myPos;

		moving = false;

		//float speed = 0f;
		speed = speed * 0.7f;
		if(speed < 0.1f){
			speed = 0.0f;
		}
		//Debug.Log(transform.forward);

		if(dist > maxDistance)
		{
			float spd = walkingSpeed;
			if (dist > fastWalkDistance){
				spd = fastWalkingSpeed;
			}
			speed = spd*Vector3.Project(difference, transform.forward).magnitude;
			moving = true;
		}
		if(dist < minDistance)
		{
			speed = -walkingSpeed*3.0f*Vector3.Project(difference, transform.forward).magnitude;
			moving = true;
		}
		if(Mathf.Abs(angle) < angleTolerance)
		{
			transform.LookAt(transform.position + difference);
			angle = 0f;
		}
		if(Mathf.Abs(speed) < speedTolerance)
		{
			speed = 0f;
		}
		Debug.Log("speed " + speed);
		// Call the Setup function of the helper class with the given parameters.
		animSetup.Setup(speed, angle);
	}

	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;
		
		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle(fromVector, toVector);
		
		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross(fromVector, toVector);

		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		
		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;
		
		return angle;
	}
}
