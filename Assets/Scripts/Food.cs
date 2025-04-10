using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider Area;


    
    // Start is called before the first frame update
    private void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void RandomPosition()
    {
        Bounds bounds = this.Area.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        this.transform.position = new Vector3(Mathf.Round(x), 0, Mathf.Round(z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag =="Player2")
        {
            RandomPosition();
        }
    }
}
