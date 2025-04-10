using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn;
    public BoxCollider Area;
    public AudioSource drinkAudio;



    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            drinkAudio.Play();
            Destroy(gameObject);
        }
    }
}
