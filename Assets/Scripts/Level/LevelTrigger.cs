using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelTrigger : MonoBehaviour
{
    private Collider coll;
    public int levelNumber = 0;
    public float transitionTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.ChangeLevel(levelNumber, transitionTime);
        }
    }
}
