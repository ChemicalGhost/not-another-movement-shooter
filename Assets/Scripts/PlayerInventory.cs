using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetory : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject weaponPlacement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // PickWeapon(player.GetComponent < CapsuleCollider);
    }

    void PickWeapon(Collision collision)
    {

        if (collision.gameObject.gameObject.layer == 7)
        {
            Debug.Log("Colliding WIth Weapon");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding WIth Weapon " + collision.gameObject.name);
        if (collision.gameObject.layer == 7)
        {
        }
    }
    private void OnCollisionExit(Collision collision)
    {

    }

    // private void OnCollisionStay(Collision collision)
    // {

    // }


}
