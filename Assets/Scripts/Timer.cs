using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class Timer : MonoBehaviour
{
    public Text textTime;
    public int countdown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    // Update is called once per frame
    IEnumerator CountdownToStart()
    {
        while(countdown > 0)
        {
            textTime.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        textTime.text = "START!";
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(0.5f);
        Destroy(textTime);
    }

 

}
