using UnityEngine;
using System.Collections;

public class grid : MonoBehaviour
{

    private Material lineMaterial;

    void Start()
    {
        MakeLines();


    }

    void MakeLines()
    {
        CreateLineMaterial();
        GameObject grid = new GameObject;
    
        Color mainColor = new Color(0f, 1f, 0f, 0.2f);
        Color subColor = new Color(0f, 0.5f, 0f, 0.2f);
        for (float x = 0.0f; x < 1.0f; x += 0.1f)
        {
            for (float y = 0.0f; y < 1.0f; y += 0.1f)
            {
                for (float z = 0.0f; z < 1.0f; z += 0.1f)
                {

                    //private GameObject prefab = Resources.Load("Prefabs/gridcell", GameObject);
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x, y, z);
                    cube.transform.localScale = new Vector3(0.1f, 0.002f, 0.002f);

                    cube.GetComponent<Renderer>().material = lineMaterial;
                   
                    cube.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 0.2f);
                    cube.transform.parent = grid;


                }
            }
        }
    }

    void loadBlenderGrid()
    {
        for (float x = 0.0f; x < 1.0f; x += 0.1f)
        {
            for (float y = 0.0f; y < 1.0f; y += 0.1f)
            {
                for (float z = 0.0f; z < 1.0f; z += 0.1f)
                {

                    //private GameObject prefab = Resources.Load("Prefabs/gridcell", GameObject);
                    GameObject temp = Instantiate(Resources.Load("gridcell"), new Vector3(x, y, z), Quaternion.identity) as GameObject;
                    temp.transform.localScale *= 0.1f;

                }
            }
        }
    }

    void CreateLineMaterial()
    {

        if (!lineMaterial)
        {
            //Shader shader = Shader.Find("Plane/No zTest");
            //lineMaterial = new Material(shader);
            //lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            //lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial = new Material(Shader.Find("Standard"));
            lineMaterial.color  = new Color(0f, 1f, 0f, 0.2f);
        }
    }



    
  
}