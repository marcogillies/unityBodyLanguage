using UnityEngine;
using System.Collections;

public class SpeechResponse : MonoBehaviour {

	public MicInput micInput;
	private Animator anim;

	public float timer = 0.0f;
	public float silenceTime = 10.0f;
	public float silenceThreshold = 1.0f;
	public float smoothedVolume = 0.0f;
	public float smoothing = 0.5f;
	public float resetTimer = 0.0f;

	//public GameObject listener;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		smoothedVolume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("speech response " + micInput);
		if (micInput != null) 
		{
			Debug.Log (micInput.loudness);
			smoothedVolume = smoothing*smoothedVolume + (1.0f - smoothing)*micInput.loudness;
			anim.SetFloat("Speech", smoothedVolume);
			
			if(resetTimer > 0.9 || micInput.loudness > silenceThreshold){
				timer = 0.0f;
			} else{
				timer += Time.deltaTime;
			}
			anim.SetFloat("SilenceTime", timer);
			//if (timer > silenceTime){
			//	timer = 0.0f;
			//	if(listener != null){
			//		listener.SendMessage("OnAudioSilence");
			//	}
			//}
		}
	}
}
