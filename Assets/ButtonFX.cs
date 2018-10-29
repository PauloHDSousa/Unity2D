using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour {

    public AudioSource myFx;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFX);
    }
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFX);
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
