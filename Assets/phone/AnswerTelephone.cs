using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerTelephone : MonoBehaviour {

	public GameObject character;
    public GameObject anchor;
	bool isWalkingTowards = false;
	bool standingNear = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = character.GetComponent<Animator>();
	}
	
	void Update () {
		if(isWalkingTowards)
		{
			AutoWalkTowards();
		}
	}

	void FixedUpdate()
	{
		AnimLerp();
	}

	void OnMouseDown()
	{
		if(!standingNear)
		{
			anim.SetFloat("speed",1);
			anim.SetBool("isWalking", true);
			isWalkingTowards = true;
			Controller.controlledBy = this.gameObject;
		}
		else
		{
			anim.SetBool("isAnswering", false);
			isWalkingTowards = false;
			Controller.controlledBy = null;
			standingNear = false;
		}
	}


    void AutoWalkTowards()
    {
		Vector3 targetDir;
		targetDir = new Vector3(anchor.transform.position.x - character.transform.position.x,
								0f,
								anchor.transform.position.z - character.transform.position.z);
    	
    	Quaternion rot = Quaternion.LookRotation(targetDir);
    	character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);

		//Debug.Log(Vector3.Distance(character.transform.position, anchor.transform.position));
		if(Vector3.Distance(character.transform.position, anchor.transform.position) < 0.1f)
		{
			anim.SetBool("isAnswering", true);
			anim.SetBool("isWalking", false);

			character.transform.rotation = anchor.transform.rotation;

			isWalkingTowards = false;
			standingNear = true;
		}
    }

    void AnimLerp()
    {
        if(!standingNear) return;

        if (Vector3.Distance(character.transform.position,anchor.transform.position) > 0.1f)
        {
            character.transform.rotation = Quaternion.Lerp(character.transform.rotation, 
                                                 anchor.transform.rotation, 
                                                 Time.deltaTime * 0.5f);
            character.transform.position = Vector3.Lerp(character.transform.position, 
                                              anchor.transform.position, 
                                              Time.deltaTime * 0.5f);
         }
         else
         {
            character.transform.position = anchor.transform.position;
            character.transform.rotation = anchor.transform.rotation;
         }
    }
}
