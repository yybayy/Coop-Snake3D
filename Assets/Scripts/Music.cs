using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource Countdown; 
    IEnumerator playSoundAfterTenSeconds()
    {
        yield return new WaitForSeconds(4);
        Countdown.Play();
    }

    // then elsewhere when you want to invoke it:

    void someOtherMethodInAMonoBehaviour()
    {
        StartCoroutine(playSoundAfterTenSeconds());
    }
}