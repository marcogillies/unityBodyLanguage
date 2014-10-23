using UnityEngine;
using System.Collections;

public class RandomTransition : MonoBehaviour {

	public int animationLayer = 0;
	public int numAnimations;
	public string parameterName;
	public int parameterId;
	public bool motionOn = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		parameterId = Animator.StringToHash(parameterName);
		int newAnimation = (int) Random.Range(0, numAnimations);
        anim.SetInteger(parameterId, newAnimation);
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo animstateinfo = anim.GetCurrentAnimatorStateInfo(animationLayer);
		if(animstateinfo.normalizedTime >= 0.95f)
        {
        	if(motionOn == true)
        	{
	        	int newAnimation = (int) Random.Range(0,numAnimations);
	        	//Debug.Log("random number " + newAnimation);
	        	anim.SetInteger(parameterId, newAnimation);
        	} else {
        		anim.SetInteger(parameterId, 0);

        	}
        }
	}
}
