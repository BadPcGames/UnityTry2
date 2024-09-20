using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPosition : MonoBehaviour
{
    private int angle = 0;
    [SerializeField]
    private int radius;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        changePosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            changePosition(-10);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            changePosition(10);
        }
    }
    private void changePosition(int change)
    {
        angle += change;
        position.x = -radius * Mathf.Cos((angle * Mathf.PI) / 180);
        position.z = radius * Mathf.Sin((angle * Mathf.PI) / 180);
        position.y = radius/2;
        transform.position = position;
        transform.rotation = Quaternion.Euler(20f, angle+90f, 0f); 
    }
}
