using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackInput : MonoBehaviour {
    
    public P_WeaponEquip PlayerWeaponEquip;
    public Animator Anim;
    // Use this for initialization
    void Start () {
        PlayerWeaponEquip = GetComponent<P_WeaponEquip>();
        Anim = GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerWeaponEquip.WeaponEquipped){

            if (Input.GetButton("Fire1"))
            {
                Anim.SetTrigger("Attack1");
            }

            if (Input.GetButton("Fire2"))
            {
                Anim.SetTrigger("Attack2");
            }
        }
	}
}
