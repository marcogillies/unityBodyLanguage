using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	private Animator anim;
	public Transform lookAtTarget;
	public Transform [] lookAwayTargets;
	public float lookAtTime = 0.5f;
	public float lookAwayTime = 0.3f;
	public float transitionSpeed = 1f;

	public float timer = 0.0f;
	public Transform target;
	public Vector3 lookAtPos;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		int i = (int)Random.Range(0, lookAwayTargets.Length);
		//target = lookAwayTargets[i];
		target = lookAtTarget;
		lookAtPos = target.position;
		timer = lookAwayTime;
	}
	
	void OnAnimatorIK (){
		// timer -= Time.deltaTime;
		// if(timer <= 0f)
		// {
		// 	if(target == lookAtTarget)
		// 	{
		// 		int i = (int)Random.Range(0, lookAwayTargets.Length);
		// 		target = lookAwayTargets[i];
		// 		timer = lookAwayTime;
		// 	}
		// 	else
		// 	{
		// 		target = lookAtTarget;
		// 		timer = lookAtTime;
		// 	}
		// }
		lookAtPos = Vector3.Lerp(lookAtPos, target.position, transitionSpeed * Time.deltaTime);
        anim.SetLookAtPosition (lookAtPos);
        anim.SetLookAtWeight(1.0f,1.0f,1.0f,1.0f,0.0f); // global, body, head, eyes, clamp (where 0 is unrestrained / 1 is full clamp)
    }
}
