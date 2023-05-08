using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectDropdown;
using UnityEngine.UI;
using TMPro;

public class ObjectVisPhoton : MonoBehaviour
{
    public GameObject applePrefab;
    public TMPro.TMP_Dropdown visionModeDropdown;
    int SelectedMethodID;
    public Material RGBAppleMaterial;


    // Start is called before the first frame update
    void Start()
    {
        ObjectDropdown.RGBSpawner();
        ObjectDropdown.DeuteranopiaSpawner();
        ObjectDropdown.MonochromacySpawner();

        //visionModeDropdown = GetComponent<Dropdown>();

        // Instantiate apples
        GameObject RGBApple = Instantiate(applePrefab);
        // GameObject DeuteranopiaApple = Instantiate(DeuteranopiaBasil);
        // GameObject MonochromacyApple = Instantiate(MonochromacyBasil);
        Debug.Log(visionModeDropdown.value);

        

        // Component[] components = RGBApple.GetComponents<Component>();
        // Debug.Log(components[0]);
        //Debug.Log(FindObjectOfType<GameObject>());
        
        // Assign positions to apples
        RGBApple.transform.position = new Vector3(500, -100, 800);
        // DeuteranopiaApple.transform.position = new Vector3(0, -100, 800);
        // MonochromacyApple.transform.position = new Vector3(-500, -100, 800);

        // Make apples bigger
        RGBApple.transform.localScale = new Vector3(1000f, 1000f, 1000f);
        // DeuteranopiaApple.transform.localScale = new Vector3(1000f, 1000f, 1000f);
        // MonochromacyApple.transform.localScale = new Vector3(1000f, 1000f, 1000f);

        //Get each apple's material
        RGBAppleMaterial = RGBApple.GetComponent<MeshRenderer>().sharedMaterial;
        // Material DeuteranopiaAppleMaterial = DeuteranopiaApple.GetComponent<MeshRenderer>().sharedMaterial;
        // Material MonochromacyAppleMaterial = MonochromacyApple.GetComponent<MeshRenderer>().sharedMaterial;

        // //Assign different shader
        // DeuteranopiaAppleMaterial.SetInt("_ChosenMethod", 1);
        // MonochromacyAppleMaterial.SetInt("_ChosenMethod", 2);
    }


    public void ChangeMaterial()
    {
        SelectedMethodID = visionModeDropdown.value;
        // Debug.Log("Dropdown Changed");
        // Debug.Log(SelectedMethodID);
        RGBAppleMaterial.SetInt("_ChosenMethod", SelectedMethodID);
    }
}
