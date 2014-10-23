using UnityEngine;
using System.Collections;

public class SequentialTransition : MonoBehaviour {

	public float currentPlaying = 0;
	public string parameterName;
	public int parameterId;
	private Animator anim;

	public AudioClip [] audioclips;

	// Use this for initialization
	void Start () {
		//currentPlaying = 0;
		anim = GetComponent<Animator>();
		parameterId = Animator.StringToHash(parameterName);
	}
	
	// void OnAudioSilence(){
	// 	currentPlaying += 1;
	// 	anim.SetInteger(parameterId, currentPlaying);
	// 	if (audio != null && currentPlaying < audioclips.Length){
	// 		if (audioclips[currentPlaying] != null){
	// 			audio.clip = audioclips[currentPlaying];
	// 			audio.Play();
	// 		}
	// 	}
	// }

	// Update is called once per frame
	void Update () {
		if((int)currentPlaying > 0)
			anim.SetInteger(parameterId, (int) currentPlaying);
	}
}
