using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCamera : MonoBehaviour
{
    [SerializeField] private Material outline;
    [HideInInspector] public List<Material> saveMaterial = null;
    private RaycastHit hit;
    private GameObject hitObject, saveHitObject, canvas;
    private bool rayCast;
    void FixedUpdate()
    {
        rayCast = Physics.Raycast(transform.position, transform.forward, out hit, 2, LayerMask.GetMask("CameraRaycast"));
        if (rayCast)
        {
            hitObject = hit.collider.transform.parent.GetChild(0).gameObject;
            if (saveHitObject!=null && hitObject != saveHitObject) ReturnMaterial();
            canvas = hit.collider.transform.parent.GetChild(1).gameObject;
            canvas.SetActive(true);
            if (saveMaterial.Count == 0)
            {
                saveMaterial = new List<Material>();
                for (int i = 0; i < hitObject.transform.childCount; i++)
                {
                    MeshRenderer mesh = hitObject.transform.GetChild(i).GetComponent<MeshRenderer>();
                    saveMaterial.Add(mesh.material);
                    mesh.material = outline;
                }
            }
            saveHitObject = hitObject;
        }
        else if (saveHitObject && saveMaterial.Count>0) ReturnMaterial();

    }
    void ReturnMaterial()
    {
        for(int i=0;i<saveHitObject.transform.childCount;i++)
        {
            saveHitObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = saveMaterial[i];
        }
        canvas.SetActive(false);
        canvas = null;
        saveHitObject = null;
        saveMaterial.Clear();
    }
    private void OnDrawGizmos()
    {
        if (!rayCast) Gizmos.color = Color.red;
        else Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }
}
