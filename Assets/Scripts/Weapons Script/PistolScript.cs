using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    [SerializeField] GameObject muzzlePoint;
    [SerializeField] int damagePerBullet = 13;
    [SerializeField] int ammoSize = 12;
    [SerializeField] int fireRate = 2;
    [SerializeField] bool isEquipped = false;

    [SerializeField] GameObject weaponPlacement;

    LineRenderer lineRenderer;

    [SerializeField] Camera cameraa;
    void Start()
    {
        // Get the Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();
        // Ensure the Line Renderer has 2 positions (start and end)
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isEquipped)
        {
            PlaceWeaponInHand();
            Fire();
        }
        else
        {
            Debug.Log("CAN'T BE FIRED");
        }
    }

    void Fire()
    {

        if (Input.GetMouseButton(0))
        {
            PointToCrosshair();
            Debug.Log("GUN FIRED!!!");
        }

    }

    void PointToCrosshair()
    {
        Vector3 rayOriginPos = muzzlePoint.transform.position;
        Vector3 rayDirection = cameraa.transform.forward;
        Ray cameraRay = cameraa.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, 100.0f))

        {
            // RenderLine(cameraRay.origin, hitInfo.point);

            // Calculate direction for the second ray
            Vector3 secondRayDirection = (hitInfo.point - rayOriginPos).normalized;

            // Cast the second ray from the gun towards the first hit point
            if (Physics.Raycast(rayOriginPos, secondRayDirection, out RaycastHit weaponHitInfo, hitInfo.distance))
            {

                Debug.Log("Hit SOMETHING" + hitInfo.point + hitInfo.distance);
                RenderLine(rayOriginPos, weaponHitInfo.point);
                // Debug.DrawRay(rayOriginPos, secondRayDirection, Color.red);

            }
            else
            {
                Debug.Log("NOTHING");
                RenderLine(rayOriginPos, weaponHitInfo.point);
                // Debug.DrawRay(rayOriginPos, secondRayDirection, Color.green);

            }

        }
        else
        {
            // Optional: Extend the line to a maximum distance if no hit
            Vector3 maxDistancePoint = rayOriginPos + rayDirection * 100.0f;
            // RenderLine(rayOriginPos, maxDistancePoint);
            Debug.DrawRay(rayOriginPos, maxDistancePoint, Color.green);
        }
    }

    void RenderLine(Vector3 lineOrign, Vector3 lineDestination)
    {
        lineRenderer.SetPosition(0, lineOrign);
        lineRenderer.SetPosition(1, lineDestination);
        // lineRenderer.sharedMaterial.SetColor("_Color", Color.green);
    }


    void PlaceWeaponInHand()
    {
        GetComponent<Transform>().SetParent(weaponPlacement.transform, true);
        GetComponent<Transform>().transform.position =  weaponPlacement.transform.position;
    }

    public void SetWeaponEquipState(bool option)
    {
        isEquipped = option;
    }
    public bool GetWeaponEquipState()
    {
        return isEquipped;
    }


}
