using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevelSwitch : MonoBehaviour
{
    // Camera Positions for each level
    public Transform[] cameraPositions;
    public float transitionTime = 1.0f;
    private float currentTransitionTime = 0.0f;


    private Transform startPos;
    private Transform endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (endPos == null)
            return;

        currentTransitionTime += Time.deltaTime;
        float normTime = Mathf.Clamp(currentTransitionTime / transitionTime, 0.0f, 1.0f);
        transform.position = Vector3.Lerp(startPos.position, endPos.position, normTime);
    }

    public void GoToLevel(int level)
    {
        if(level > cameraPositions.Length || level < 0)
        {
            Debug.LogError("Level out of bounds!");
            return;
        }

        if(endPos != null)
        {
            startPos = endPos;
        }

        endPos = cameraPositions[level];

        if (startPos != endPos)
        {
            currentTransitionTime = 0.0f;
        }
    }
}
