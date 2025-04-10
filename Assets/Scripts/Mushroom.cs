using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public GameObject spawnObject;
    public BoxCollider Area;


    //current time
    private float timer = 0.0f;
    private float spawnTime = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Counts up
        timer += Time.deltaTime;


        if (timer > spawnTime)
        {

            timer = 0.0f;
            SpawnObject();

        }

        //Check if its the right time to spawn the object

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "spawnObject" || other.tag == "Player1" || other.tag == "Player2" || other.tag == "SinglePlayer")
        {
            Destroy(spawnObject);
        }
    }
    void SpawnObject()
    {

        Bounds bounds = this.Area.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        this.transform.position = new Vector3(Mathf.Round(x), 0, Mathf.Round(z));
        Instantiate(spawnObject, this.transform.position, spawnObject.transform.rotation);

    }
}
