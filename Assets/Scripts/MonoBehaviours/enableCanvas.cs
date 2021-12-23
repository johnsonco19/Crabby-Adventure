using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enableCanvas : MonoBehaviour
{
    //The text you want to enable
    public Text ESText;
    public Text StartText;

    //The image you want to enable
    public Image ESImage;
    public Image StartImage;


    //The ShellObjects being referenced
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;

    // Enemy objects to be turned off
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;

    // Start is called before the first frame update
    void Start()
    {

        // Invokes method to keep start canvas on screen for 5 seconds
        Invoke("endstart", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (item1.activeSelf == false && item2.activeSelf == false && item3.activeSelf == false && item4.activeSelf == false && item5.activeSelf == false)
        {
            //Activate the canvas
            ESText.enabled = true;
            ESImage.enabled = true;

            Destroy(enemy1);
            Destroy(enemy2);
            Destroy(enemy3);
            Destroy(enemy4);
            Destroy(enemy5);
            Destroy(enemy6);

        }
    }

    void endstart()
    {
        StartText.enabled = false;
        StartImage.enabled = false;
    }
}
