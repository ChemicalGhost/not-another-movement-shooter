using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetory : MonoBehaviour
{
    // [SerializeField] GameObject weaponPlacement;
    [SerializeField] PistolScript pistolScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding WIth " + collision.gameObject.name);
        // if (Input.GetKey(KeyCode.E))
        // {
        //     pistolScript.SetWeaponEquipState(true);
        // }

    }
    // private void OnCollisionExit(Collision collision)
    // {

    // }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("STAY Colliding WIth " + collision.gameObject.name);
        if (Input.GetKey(KeyCode.E))
        {
            pistolScript.SetWeaponEquipState(true);
        }
    }


}
