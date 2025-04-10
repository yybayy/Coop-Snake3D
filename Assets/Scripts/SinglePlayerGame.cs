using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerGame : MonoBehaviour
{
    Renderer ren;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector3 direction = Vector3.zero;
    private Vector3 input;
    public AudioSource collectAudio;
    public AudioSource drinkAudio;
    public bool invert = false;
    public float wait = 0.0f;
    public float MushroomTimer = 0.0f;
    public bool MushroomEffect = false;
    LightAdjuster LightColor;
    private void Start()
    {
        ResetState();
        ren = GetComponent<Renderer>();
        ren.enabled = true;
        LightColor = GameObject.FindGameObjectWithTag("Light").GetComponent<LightAdjuster>();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f && invert == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                input = Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                input = Vector3.back;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.z != 0f && invert == false)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                input = Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                input = Vector3.left;
            }
        }
        else if (direction.x != 0f && invert == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                input = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                input = Vector3.forward;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.z != 0f && invert == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                input = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                input = Vector3.right;
            }
        }
        if (MushroomEffect)
        {
            LightColor.ColorEffect();
            MushroomTimer += Time.deltaTime;
            if (MushroomTimer > 5)
            {
                MushroomTimer = 0.0f;
                MushroomEffect = false;
            }

        }
        if (!MushroomEffect)
        {
            LightColor.Normal();
        }

    }

    private void FixedUpdate()
    {
        if (wait > 3)
        {

            // Set the new direction based on the input
            if (input != Vector3.zero)
            {
                direction = input;
            }

            // Set each segment's position to be the same as the one it follows. We
            // must do this in reverse order so the position is set to the previous
            // position, otherwise they will all be stacked on top of each other.
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            // Move the snake in the direction it is facing
            // Round the values to ensure it aligns to the grid
            float x = Mathf.Round(transform.position.x) + direction.x;
            float z = Mathf.Round(transform.position.z) + direction.z;

            transform.position = new Vector3(x, 0.0f, z);
        }
        else
        {
            if (wait > 0 && wait < 0.5f)
            {
                ren.enabled = false;
            }
            else if (wait > 1 && wait < 1.5f)
            {
                ren.enabled = false;
            }
            else if (wait > 2 && wait < 2.5f)
            {
                ren.enabled = false;
            }
            else
            {
                ren.enabled = true;
            }

            wait += Time.deltaTime;
        }
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        invert = false;
    }

    public void ResetState()
    {
        direction = Vector3.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);
        invert = false;
        wait = 0.0f;
        MushroomEffect = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            SinglePlayerStats.Instance.addPointP1();
            collectAudio.Play();
            Grow();
        }
        else if (other.gameObject.CompareTag("Collider") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player"))
        {
            SinglePlayerStats.Instance.P1reset();
            ResetState();
        }
        else if (other.gameObject.CompareTag("Beer"))
        {
            Destroy(other.gameObject);
            drinkAudio.Play();
            invert = true;

        }
        else if (other.gameObject.CompareTag("Mushroom"))
        {
            collectAudio.Play();
            Destroy(other.gameObject);
            MushroomTimer = 0.0f;
            MushroomEffect = true;
        }

    }

}
