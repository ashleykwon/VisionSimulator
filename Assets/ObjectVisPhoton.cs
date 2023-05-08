using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectDropdown;
using UnityEngine.UI;
using TMPro;

public class ObjectVisPhoton : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject dataPoint;
    public TMPro.TMP_Dropdown visionModeDropdown;
    int SelectedMethodID;
    public Material RGBAppleMaterial;
    public List<GameObject> dataPointsInScene = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        //ObjectDropdown.RGBSpawner();
       

        // Instantiate apples
        GameObject RGBApple = Instantiate(applePrefab);
        //Debug.Log(visionModeDropdown.value);

        
        // Assign positions to apples
        RGBApple.transform.position = new Vector3(500, 100, 800);


        // Make apples bigger
        RGBApple.transform.localScale = new Vector3(2000f, 2000f, 2000f);


        //Get each apple's material
        RGBAppleMaterial = RGBApple.GetComponent<MeshRenderer>().sharedMaterial;
      
        // Spawn spheres and add them to the list of game objects
        for (int x = 0; x <= 255; x+=50)
        {
            for (int y = 0; y <= 255; y+=50)
            {
                for (int z = 0; z <= 255; z+=50)
                {
                    GameObject dataPointInstance = Instantiate(dataPoint);
                    dataPointsInScene.Add(dataPointInstance);

                    dataPointInstance.GetComponent<Renderer>().material.color = new Color(x/255f, y/255f, z/255f, 1f);
                    //Debug.Log(dataPointInstance.GetComponent<Renderer>().material.color);
                    dataPoint.transform.position = new Vector3(x, y, z+800);
                    dataPoint.transform.localScale = new Vector3(10f, 10f, 10f);

                    dataPointInstance.transform.position = new Vector3(x, y, z+800);
                    dataPointInstance.transform.localScale = new Vector3(10f, 10f, 10f);

                    
                }
            }
        }
        
    }


    public void ChangeMaterial()
    {
        SelectedMethodID = visionModeDropdown.value;
        // Debug.Log("Dropdown Changed");
        // Debug.Log(SelectedMethodID);
        RGBAppleMaterial.SetInt("_ChosenMethod", SelectedMethodID);

        // Get all spheres (dataPoints) in the scene
        // for (int i = 0; i < dataPointsInScene.Count; i++)
        // {
        //     // Modify data point object color
        //     dataPointsInScene[0].GetComponent<MeshRenderer>().sharedMaterial.SetInt("_ChosenMethod", SelectedMethodID);
        // }
        if (SelectedMethodID == 0) //Normal Vision
        {
            for (int i = 0; i < dataPointsInScene.Count; i++)
            {

                Vector3 dataPointPos = dataPointsInScene[i].transform.position;

                // Modify data point object color
                float R = dataPointPos[0]/255f;
                float G = dataPointPos[1]/255f;
                float B = (dataPointPos[2]-800f)/255f;

                dataPointsInScene[i].GetComponent<Renderer>().material.color = new Color(R, G, B, 1f);
            }
        }
        else if (SelectedMethodID == 1) //Deuteranoptia
        {
            for (int i = 0; i < dataPointsInScene.Count; i++)
            {

                Vector3 dataPointPos = dataPointsInScene[i].transform.position;

                // Modify data point object color
                float R = dataPointPos[0]/255f;
                float G = dataPointPos[1]/255f;
                float B = (dataPointPos[2]-800f)/255f;

                Vector3 RGB = new Vector3(R, G, B);
                Vector3 LMS = VisionDropdown.RGBToLMS(RGB);
                Vector3 deuteranopiaRGB = VisionDropdown.LMSToRGB(VisionDropdown.DeuteranopiaSimulator(LMS));

                dataPointsInScene[i].GetComponent<Renderer>().material.color = new Color(deuteranopiaRGB[0], deuteranopiaRGB[1], deuteranopiaRGB[2], 1f);
            }
        }

        else if (SelectedMethodID == 2) //Monochromacy
        {
            for (int i = 0; i < dataPointsInScene.Count; i++)
            {
                Vector3 dataPointPos = dataPointsInScene[i].transform.position;
                
                // Modify data point object color
                float R = dataPointPos[0]/255f;
                float G = dataPointPos[1]/255f;
                float B = (dataPointPos[2]-800f)/255f;

                Vector3 RGB = new Vector3(R, G, B);
                Vector3 LMS = VisionDropdown.RGBToLMS(RGB);
                Vector3 monochromacyRGB = VisionDropdown.MonochromacySimulator(RGB);

                dataPointsInScene[i].GetComponent<Renderer>().material.color = new Color(monochromacyRGB[0], monochromacyRGB[1], monochromacyRGB[2], 1f);
            }
        }

        else if (SelectedMethodID == 3) // Proptanopia
        {
            for (int i = 0; i < dataPointsInScene.Count; i++)
            {
                Vector3 dataPointPos = dataPointsInScene[i].transform.position;
                
                // Modify data point object color
                float R = dataPointPos[0]/255f;
                float G = dataPointPos[1]/255f;
                float B = (dataPointPos[2]-800f)/255f;

                Vector3 RGB = new Vector3(R, G, B);
                Vector3 LMS = VisionDropdown.RGBToLMS(RGB);
                Vector3 protanopiaRGB = VisionDropdown.LMSToRGB(VisionDropdown.ProtanopiaSimulator(LMS));

                dataPointsInScene[i].GetComponent<Renderer>().material.color = new Color(protanopiaRGB[0], protanopiaRGB[1], protanopiaRGB[2], 1f);
            }
        }

        else if (SelectedMethodID == 4)
        {
            for (int i = 0; i < dataPointsInScene.Count; i++)
            {
                Vector3 dataPointPos = dataPointsInScene[i].transform.position;
                
                // Modify data point object color
                float R = dataPointPos[0]/255f;
                float G = dataPointPos[1]/255f;
                float B = (dataPointPos[2]-800f)/255f;

                Vector3 RGB = new Vector3(R, G, B);
                Vector3 XYZ = VisionDropdown.RGBToXYZ(RGB);
                Vector3 LMS = VisionDropdown.XYZToLMS(XYZ);

                Vector3 tritanopiaLMS = VisionDropdown.TritanopiaSimulator(LMS);
                float cieX = tritanopiaLMS[0]*1.9102f - 1.1121f*tritanopiaLMS[1] + 0.2019f*tritanopiaLMS[2];
                float cieY = 0.3710f*tritanopiaLMS[0] + 0.6291f*tritanopiaLMS[1];
                float cieZ = tritanopiaLMS[2];
                Vector3 tritanopiaRGB = new Vector3((3.2404f*cieX - 1.5371f*cieY - 0.4985f*cieZ), 
                (-0.9693f*cieX + 1.8760f*cieY + 0.0416f*cieZ), 
                (0.0556f*cieX - 0.2040f*cieY + 1.0572f*cieZ));
                dataPointsInScene[i].GetComponent<Renderer>().material.color = new Color(tritanopiaRGB[0], tritanopiaRGB[1], tritanopiaRGB[2], 1f);
            }
        }

        
        
        // Access their materials
        // Change their colors

    }
}
