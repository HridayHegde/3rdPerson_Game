using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WeaponEquip : MonoBehaviour {
    public P_MoveInput PlayerMove;
    public Animator animator;
    public bool WeaponEquipped;
    public Weapon CurrentWeapon;
    
    // Use this for initialization
    void Start () {
        PlayerMove = GetComponent<P_MoveInput>();
        animator = PlayerMove.Anim;
        PlayerMove.blockRotPlayer = WeaponEquipped;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponEquipped = !WeaponEquipped;
            PlayerMove.blockRotPlayer = WeaponEquipped;
        }
        

        
        if (CurrentWeapon == Weapon.Axe) {
            animator.SetBool("Equipped_Axe", WeaponEquipped);
        }
        else if (CurrentWeapon == Weapon.Sword)
        {
            animator.SetBool("Equipped_Sword", WeaponEquipped);
        }
		
	}

    public enum Weapon
    {
        Axe,
        Sword
    };
}
