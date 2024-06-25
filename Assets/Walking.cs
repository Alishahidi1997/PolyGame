using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    Animator anim;
    float speed = 100f;
    public GameObject phone;
    float weight = 1f; 

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float translate = Input.GetAxis("Vertical") * speed / 50 * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        weight = anim.GetFloat("weight"); 
        
        transform.Translate(0, 0, translate);
        transform.Rotate(0, rotation, 0);
        if (translate != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
            anim.SetBool("Walking", false);
        if (Input.GetKeyDown(KeyCode.Space))
            OnAnimatorIK(0);
            //&& Vector3.Distance(phone.transform.position, transform.position) < 1f) 
            anim.SetTrigger("PickUp"); 
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.RightHand, phone.transform.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
    }
}
