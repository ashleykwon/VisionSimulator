using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static VisionDropdown;


public class ObjectDropdown : MonoBehaviour
{

    // int visionVal;

    // For Passthrough mode
    public GameObject applePrefab;
    public GameObject dataPoint;
    public TMPro.TMP_Dropdown visionModeDropdown;
    public int SelectedMethodID;
    public Material RGBAppleMaterial;

    void Start()
    {
        
        RGBProtanopiaSpawner();
        RGBSpawner();
        TritanopiaSpawner();
        SelectedMethodID = 0;

        // Instantiate apples
        GameObject RGBApple = Instantiate(applePrefab);
        // GameObject ProptanopiaApple = Instantiate(applePrefab);
        // GameObject TritanopiaApple = Instantiate(applePrefab);

        // Assign positions to apples
        RGBApple.transform.position = new Vector3(500, -100, 800);
        // ProptanopiaApple.transform.position = new Vector3(0, -100, 800);
        // TritanopiaApple.transform.position = new Vector3(-500, -100, 800);

        // Make apples bigger
        RGBApple.transform.localScale = new Vector3(1000f, 1000f, 1000f);
        // ProptanopiaApple.transform.localScale = new Vector3(1000f, 1000f, 1000f);
        // TritanopiaApple.transform.localScale = new Vector3(1000f, 1000f, 1000f);

        //Get each apple's material
        RGBAppleMaterial = RGBApple.GetComponent<MeshRenderer>().sharedMaterial;
        // Material ProptanopiaAppleMaterial = ProptanopiaApple.GetComponent<MeshRenderer>().sharedMaterial;
        // Material TritanopiaAppleMaterial = TritanopiaApple.GetComponent<MeshRenderer>().sharedMaterial;

        //Assign different shader
        
        //TritanopiaAppleMaterial.SetInt("ChosenMethod", 4);

    }

    public void ChangeMaterial()
    {
        SelectedMethodID = visionModeDropdown.value;
        RGBAppleMaterial.SetInt("ChosenMethod", SelectedMethodID);
    }

    public static void RGBSpawner()
    {  
        for (int x = 0; x <= 255; x+=50)
        {
            for (int y = 0; y <= 255; y+=50)
            {
                for (int z = 0; z <= 255; z+=50)
                {
                    GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    dataPoint.GetComponent<Renderer>().material.color = new Color(x/255f, y/255f, z/255f, 1f);
                    dataPoint.transform.position = new Vector3(x, y, z+800);
                    dataPoint.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
        }
    }

    public static void RGBProtanopiaSpawner()
    {
        for (int x = 0; x <= 255; x+=50)
        {
            for (int y = 0; y <= 255; y+=50)
            {
                for (int z = 0; z <= 255; z+=50)
                {
                    GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    Vector3 RGB = new Vector3(x/255f, y/255f, z/255f);
                    Vector3 LMS = VisionDropdown.RGBToLMS(RGB);
                    Vector3 protanopiaRGB = VisionDropdown.LMSToRGB(VisionDropdown.ProtanopiaSimulator(LMS));

                    dataPoint.GetComponent<Renderer>().material.color = new Color(protanopiaRGB[0], protanopiaRGB[1], protanopiaRGB[2], 1f);
                    dataPoint.transform.position = new Vector3(x-500, y, z+800);
                    dataPoint.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
        }
    }

    public static void DeuteranopiaSpawner()
    {
        for (int x = 0; x <= 255; x+=50)
        {
            for (int y = 0; y <= 255; y+=50)
            {
                for (int z = 0; z <= 255; z+=50)
                {
                    GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    Vector3 RGB = new Vector3(x/255f, y/255f, z/255f);
                    Vector3 LMS = VisionDropdown.RGBToLMS(RGB);
                    Vector3 deuteranopiaRGB = VisionDropdown.LMSToRGB(VisionDropdown.DeuteranopiaSimulator(LMS));

                    dataPoint.GetComponent<Renderer>().material.color = new Color(deuteranopiaRGB[0], deuteranopiaRGB[1], deuteranopiaRGB[2], 1f);
                    dataPoint.transform.position = new Vector3(x+500, y, z+800);
                    dataPoint.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
        }
    }

    public static void TritanopiaSpawner()
    {
        for (int x = 0; x <= 255; x+=50)
        {
            for (int y = 0; y <= 255; y+=50)
            {
                for (int z = 0; z <= 255; z+=50)
                {
                    GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    Vector3 RGB = new Vector3(x/255f, y/255f, z/255f);
                    Vector3 XYZ = VisionDropdown.RGBToXYZ(RGB);
                    Vector3 LMS = VisionDropdown.XYZToLMS(XYZ);

                    Vector3 tritanopiaLMS = VisionDropdown.TritanopiaSimulator(LMS);
                    float cieX = tritanopiaLMS[0]*1.9102f - 1.1121f*tritanopiaLMS[1] + 0.2019f*tritanopiaLMS[2];
                    float cieY = 0.3710f*tritanopiaLMS[0] + 0.6291f*tritanopiaLMS[1];
                    float cieZ = tritanopiaLMS[2];
                    Vector3 tritanopiaRGB = new Vector3((3.2404f*cieX - 1.5371f*cieY - 0.4985f*cieZ), 
                    (-0.9693f*cieX + 1.8760f*cieY + 0.0416f*cieZ), 
                    (0.0556f*cieX - 0.2040f*cieY + 1.0572f*cieZ));
                    dataPoint.GetComponent<Renderer>().material.color = new Color(tritanopiaRGB[0], tritanopiaRGB[1], tritanopiaRGB[2], 1f);
                    
                    
                    dataPoint.transform.position = new Vector3(x+500, y, z+800);
                    dataPoint.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
        }
    }

    public static void MonochromacySpawner()
    {
        for (int x = 0; x <= 255; x+=50)
        {
            for (int y = 0; y <= 255; y+=50)
            {
                for (int z = 0; z <= 255; z+=50)
                {
                    GameObject dataPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    Vector3 RGB = new Vector3(x/255f, y/255f, z/255f);
                    Vector3 monochromacyRGB = VisionDropdown.MonochromacySimulator(RGB);

                    dataPoint.GetComponent<Renderer>().material.color = new Color(monochromacyRGB[0], monochromacyRGB[1], monochromacyRGB[2], 1f);
                    dataPoint.transform.position = new Vector3(x-500, y, z+800);
                    dataPoint.transform.localScale = new Vector3(10f, 10f, 10f);
                }
            }
        }
    }

}
