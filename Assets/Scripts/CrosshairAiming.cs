using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrosshairAiming : MonoBehaviour
{
    // [SerializeField] Sprite crosshairSprite;
    [SerializeField] GameObject rayOrigin;
    [SerializeField] Camera cameraa;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();
        // Ensure the Line Renderer has 2 positions (start and end)
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        SetUpRay();
    }

    void SetUpRay()
    {

        Vector3 ray = cameraa.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(rayOrigin.GetComponent<Transform>().position, cameraa.transform.forward, out RaycastHit hitInfo, 20.0f))
        // if (Physics.Raycast(rayOrigin.GetComponent<Transform>().position, rayOrigin.GetComponent<Transform>().forward, out RaycastHit hitInfo, 20.0f))
        {
            Debug.Log("Hit SOMETHING");
            // Debug.DrawLine(rayOrigin.GetComponent<Transform>().position, hitInfo.point, Color.red, 1.5f);
            RenderLine(rayOrigin.GetComponent<Transform>().position, hitInfo.point);

        }
        else
        {
            Debug.Log("NOTHING");
        }
    }

    void RenderLine(Vector3 lineOrign, Vector3 lineDestination)
    {
        lineRenderer.SetPosition(0, lineOrign);
        lineRenderer.SetPosition(1, lineDestination + cameraa.transform.forward);
    }
}
