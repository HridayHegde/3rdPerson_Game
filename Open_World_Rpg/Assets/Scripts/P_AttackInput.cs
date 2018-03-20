using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackInput : MonoBehaviour {
    
    public P_WeaponEquip PlayerWeaponEquip;
    public Animator Anim;
    public bool Attacking;
    // Use this for initialization
    void Start () {
        PlayerWeaponEquip = GetComponent<P_WeaponEquip>();
        Anim = GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerWeaponEquip.WeaponEquipped){

            if (Input.GetButton("Fire1") && !Attacking)
            {
                Anim.SetTrigger("Attack1");
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.forward), 0.1f);
                Attacking = true;
            }

            if (Input.GetButton("Fire2") && !Attacking)
            {
                Anim.SetTrigger("Attack2");
                //Attacking= true;
            }
        }
	}

    public void AttackEnded()
    {
        Attacking = false;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.forward), 0.1f);

    }
}
