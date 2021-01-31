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
    public Animator headAnim;
    public Transform LaserOrigin;
    public Transform LaserImpact;

    public float minBlinkTime = 1.0f;
    public float maxBlinkTime = 10.0f;
    private float blinkTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        laserLine = GetComponent<LineRenderer>();
        blinkTimer = Random.Range(minBlinkTime, maxBlinkTime);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeadPosition();

        if(Input.GetButton("Fire1"))
        {
            Fire();
            headAnim.SetBool("isLasering", true);
            Head.transform.GetChild(2).gameObject.SetActive(true);
            blinkTimer = Random.Range(minBlinkTime, maxBlinkTime);
        }
        else
        {
            blinkTimer -= Time.deltaTime;   
            laserLine.enabled = false;
            headAnim.SetBool("isLasering", false);
            Head.transform.GetChild(2).gameObject.SetActive(false);
            LaserImpact.gameObject.SetActive(false);
            laserLine.positionCount = 2;
        }

        if(blinkTimer <= 0.0f)
        {
            headAnim.SetTrigger("blink");
            blinkTimer = Random.Range(minBlinkTime, maxBlinkTime);
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
        Ray laserRay;

        if (Head.transform.localScale.x > 0)
             laserRay = new Ray(startPos, LaserOrigin.right);
        else
            laserRay = new Ray(startPos, -LaserOrigin.right);

        RaycastHit hit;

        if (Physics.Raycast(laserRay, out hit, maxRange))
        {
            GameObject go = hit.collider.gameObject;
            Vector3 endPos = hit.point;
			Vector3 dir = (startPos - endPos).normalized;
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
                laserLine.positionCount = 2;
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
        Vector3 startPos = Head.transform.position;
        Vector3 mousePos = mouseRay.GetPoint(distance);
        mousePos.z = startPos.z;
        //Vector3 targetPos = (mousePos - startPos);
        //Debug.DrawLine(LaserOrigin.position, mousePos);

        //// Get Angle in Radians
        //float AngleRad = Mathf.Atan2(targetPos.y - Head.transform.position.y, targetPos.x - Head.transform.position.x);
        //// Get Angle in Degrees
        //float AngleDeg = (180 / Mathf.PI) * AngleRad;
        //// Rotate Object
        //Head.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        mousePos = Input.mousePosition;
        mousePos.z = startPos.z; //The distance between the camera and object
        Vector3 targetPos = Camera.main.WorldToScreenPoint(startPos);
        mousePos.x = mousePos.x - targetPos.x;
        mousePos.y = mousePos.y - targetPos.y;
        //Debug.DrawLine(Head.transform.position, mousePos);
        //Debug.DrawLine(startPos, mousePos, Color.red);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Head.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle - 11.5f)));

        print(LaserOrigin.transform.rotation.eulerAngles.z);
        //Debug.DrawLine(LaserOrigin.transform.position, mousePos);


        if (angle < -90.0f || angle > 90.0f)
        {
            Vector3 scale = Head.transform.localScale;
            scale.x = -1.0f;
            Head.transform.localScale = scale;
            Vector3 angles = Head.transform.rotation.eulerAngles;
            angles.z += 191.5f;
            Head.transform.rotation = Quaternion.Euler(angles);

            //LaserOrigin.transform.rotation = Quaternion.Euler(angles);
        }
        else
        {
            Vector3 scale = Head.transform.localScale;
            scale.x = 1.0f;
            Head.transform.localScale = scale;
            Vector3 angles = LaserOrigin.transform.rotation.eulerAngles;
            angles = LaserOrigin.transform.rotation.eulerAngles;

            //LaserOrigin.transform.rotation = Quaternion.Euler(angles);
        }
    }
}
