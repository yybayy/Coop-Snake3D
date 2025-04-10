using UnityEngine;

public class LightAdjuster : MonoBehaviour
{
    public Light myLight;



    // Color variables
    public bool changeColors = false;
    public float colorSpeed = 1.0f;
    public Color normal;
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;
    public bool repeatColor = false;

    float startTime;

    // Use this for initialization
    void Start()
    {
        myLight = GetComponent<Light>();
        startTime = Time.time;
        myLight.intensity = 1.63f;
    }

    // Update is called once per frame
    public void ColorEffect()
    {
        myLight.intensity = 5.63f;
        if (changeColors)
        {
            if (repeatColor)
            {
                float t = (Mathf.Sin(Time.time - startTime * colorSpeed));
                myLight.color = Color.Lerp(color1, color2, t);
                myLight.color = Color.Lerp(color2, color3, t);
                myLight.color = Color.Lerp(color3, color4, t);
                myLight.color = Color.Lerp(color4, color1, t);


            }
            else
            {
                float t = Time.time - startTime * colorSpeed;
                myLight.color = Color.Lerp(color1, color2, t);
                myLight.color = Color.Lerp(color2, color3, t);
                myLight.color = Color.Lerp(color3, color4, t);
                myLight.color = Color.Lerp(color4, color1, t);
            }
        }
    }
    public void Normal()
    {
        myLight.intensity = 1.63f;
        myLight.color = normal;
    }
}