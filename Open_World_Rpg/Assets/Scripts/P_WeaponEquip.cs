using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WeaponEquip : MonoBehaviour {
    public P_MoveInput PlayerMove;
    public Animator animator;
    public bool WeaponEquipped;
    public Weapon CurrentWeapon;
    public GameObject[] WeaponObjects;
    public double Weapon_Dissolve_Control;
    public Material WeaponMat = null;
    // Use this for initialization
    void Start() {
        PlayerMove = GetComponent<P_MoveInput>();
        foreach (GameObject x in WeaponObjects)
        {

            //WeaponMat.Add(x.GetComponent<Material>());
            
        }
        animator = PlayerMove.Anim;
        PlayerMove.blockRotPlayer = WeaponEquipped;

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponEquipped = !WeaponEquipped;
            if(WeaponEquipped == true)
            {
                foreach(GameObject x in WeaponObjects)
                {
                    x.SetActive(true);
                }
                WeaponMat.SetFloat("_Dissolve", 1);
                StartCoroutine(lerpshader(0));

                
            }
            else
            {
                WeaponMat.SetFloat("_Dissolve", 0);
                StartCoroutine(lerpshader(1));
                foreach (GameObject x in WeaponObjects)
                {
                    x.SetActive(false);
                }

            }
            
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


    IEnumerator lerpshader(int x)
    {
        if(x == 1)
        {
            double i = 0;
            while (i <=1)
            {
                WeaponMat.SetFloat("_Dissolve", (float)i);
                yield return new WaitForSeconds(0.05f);
                i += 0.5;
            }
        }
        else if(x == 0)
        {
            double i = 1;
            while (i >= 0)
            {
                WeaponMat.SetFloat("_Dissolve", (float)i);
                yield return new WaitForSeconds(0.05f);
                i -= 0.1;
            }


        }
    }
}
