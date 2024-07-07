using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCarDetails : MonoBehaviour
{
    [SerializeField] private List<GameObject> descriptionColliders;
    [SerializeField] private List<GameObject> rama = new List<GameObject>();
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private List<Material> transparentMaterials = new List<Material>();
    private List<MeshRenderer> meshes = new List<MeshRenderer>();

    public static bool isShowing;
    private bool showed;
    private void Start()
    {
        LoadMaterials();
    }
    void Update()
    {
        if(isShowing&&!showed)
        {
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.material = transparentMaterials.Find(x => (x.name + " (Instance)") == mesh.material.name );
            }
            foreach(GameObject obj in descriptionColliders) obj.SetActive(false);
            showed = true;
        }
        if (!isShowing && showed)
        {
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.material = materials.Find(x => (x.name + " (Instance)") == mesh.material.name);
            }
            foreach (GameObject obj in descriptionColliders) obj.SetActive(true);
            showed = false;
        }
    }
    private void LoadMaterials()
    {
        foreach(GameObject part in rama)
        {
            CheckPart(part);
        }
    }
    private void CheckPart(GameObject part)
    {
        if (part.transform.childCount > 0)
        {
            for (int i = 0; i < part.transform.childCount; i++) 
            {
                CheckPart(part.transform.GetChild(i).gameObject);
            }
        }
        else meshes.Add(part.GetComponent<MeshRenderer>());
    }
}
