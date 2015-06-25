using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour 
{
    public string animName = "";
	public Animation anim;

	void Awake()
	{
		anim = gameObject.GetComponent<Animation>();
	}	

	void OnGUI()
	{
		if (GUI.Button(new Rect(40, 40, 150, 50), "Shake"))
		{
            anim.Play(animName);
		}
	}
}
