using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class treeSpawner : MonoBehaviour
{
    [SerializeField]
    private int size;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;
    [SerializeField]
    private GameObject wood;
    [SerializeField]
    private GameObject leaves;


    private void Start()
    {
        Instantiate(wood, new Vector3(transform.position.x,transform.position.y,transform.position.z), new Quaternion());
    }
}














//Random.seed=seed;
//string result="make\n";
//for(int x = 0; x <=size; x++)
//{
//    int count = 42*(x);
//    for (int y = 0; y <=size;y++)
//    {
//        Vector3 el= vert[count];
//        Vector3 position = new Vector3(pos.z + el.z * 0.5f +15 , el.y * 0.5f, pos.x+ el.x * 0.5f -15);
//        makeTree(position);
//        //float hieght = (el.y - minY) / (maxY - minY);
//        //if (hieght > startOfTreeHieght && hieght < endOfTreeHieght)
//        //{
//        //    Vector3 position = new Vector3(pos.x + el.x * 0.5f, el.y*0.5f, pos.z + el.z * 0.5f);
//        //    makeTree(position);
//        //}
//        count++;
//        result += el.y.ToString() + " ";
//    }
//    result += "\n";
//}
//Debug.Log(result);