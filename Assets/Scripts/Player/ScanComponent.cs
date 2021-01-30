using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanComponent : MonoBehaviour
{
    public LayerMask reflectionLayer;
    private LineRenderer laserLine;
    private float distance = 5.0f;
    
    public float maxRange = 20.0f;
    public int maxReflectionCount = 5;

    public GameObject Head;
    public Transform LaserOrigin;
    public Transform LaserImpact;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeadPosition();

        if(Input.GetButton("Fire1"))
        {
            Fire();
            Head.transform.GetChild(0).gameObject.SetActive(false);
            Head.transform.GetChild(1).gameObject.SetActive(true);
            Head.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            laserLine.enabled = false;

            Head.transform.GetChild(0).gameObject.SetActive(true);
            Head.transform.GetChild(1).gameObject.SetActive(false);
            Head.transform.GetChild(2).gameObject.SetActive(false);
            LaserImpact.gameObject.SetActive(false);
        }
    }

    private void Fire()
    {
        laserLine.enabled = true;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 startPos = LaserOrigin.transform.position;
        Vector3 mousePos = mouseRay.GetPoint(distance);
        mousePos.z = transform.position.z;
        laserLine.SetPosition(0, startPos);
        //Debug.DrawRay(startPos, (mousePos - startPos) * maxRange, Color.green);
        Ray laserRay = new Ray(startPos, mousePos - startPos);
        RaycastHit hit;

        if (Physics.Raycast(laserRay, out hit, maxRange))
        {
            GameObject go = hit.collider.gameObject;
            Vector3 endPos = hit.point;
			Vector3 dir = (LaserOrigin.transform.position - endPos).normalized;
			laserLine.SetPosition(1, endPos);
            InteractableObject io = go.GetComponent<InteractableObject>();
            if(io != null)
            {
                io.Interact(this.gameObject);
            }

            if (go.tag == "Reflection")
            {
                DrawReflectionPattern(endPos, Vector3.Reflect(-dir, hit.normal), 2);
            }
            else
            {
                LaserImpact.position = hit.point;
                LaserImpact.gameObject.SetActive(true);
            }
        }
        else
        {
            laserLine.positionCount = 2;
            laserLine.SetPosition(1, (mousePos - startPos) * maxRange);
            LaserImpact.gameObject.SetActive(false);
        }
    }

    private void DrawReflectionPattern(Vector3 position, Vector3 direction, int reflectionNumber)
    {
        if (reflectionNumber == maxReflectionCount)
        {
            return;
        }

        laserLine.positionCount = reflectionNumber + 1;
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRange))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
            laserLine.SetPosition(reflectionNumber, position);
            GameObject go = hit.collider.gameObject;
            InteractableObject io = go.GetComponent<InteractableObject>();
            if (io != null)
            {
                io.Interact(this.gameObject);
            }

            if (hit.collider.gameObject.tag == "Reflection")
            {
                DrawReflectionPattern(position, direction, reflectionNumber + 1);
            }
            else
            {
                LaserImpact.position = hit.point;
                LaserImpact.gameObject.SetActive(true);
            }
        }
        else
        {
            position += direction * maxRange;
            laserLine.SetPosition(reflectionNumber, position);
            LaserImpact.gameObject.SetActive(false);
        }

        //Debug.DrawLine(startingPosition, position, Color.blue);
    }

    private void UpdateHeadPosition()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 startPos = Head.transform.transform.position;
        Vector3 mousePos = mouseRay.GetPoint(distance);
        Vector3 targetPos = (mousePos - startPos);
        print(targetPos);

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(targetPos.y - Head.transform.position.y, targetPos.x - Head.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        Head.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
