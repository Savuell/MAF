using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCamera : MonoBehaviour
{
    [SerializeField] private List<Material> outline = new List<Material>();
    [HideInInspector] public List<Material> saveMaterial = null;
    private int outlineMode;
    private RaycastHit hit;
    private GameObject hitObject, saveHitObject, canvas;
    private bool rayCast;
    private int k;
    void FixedUpdate()
    {
        rayCast = Physics.Raycast(transform.position, transform.forward, out hit, 5, LayerMask.GetMask("CameraRaycast"));
        if (rayCast)
        {
            switch (hit.collider.tag)
            {
                case "OutlineOrange": if (!ShowCarDetails.isShowing) outlineMode = 0; else outlineMode = -1; break;
                case "OutlineDetails": if (ShowCarDetails.isShowing) outlineMode = 0; else outlineMode = -1; break;
                case "OutlineGreen": if (!ShowCarDetails.isShowing) outlineMode = 1; else outlineMode = -1; break;
                default: outlineMode = -1; break;
            }
            if (outlineMode != -1)
            {
                hitObject = hit.collider.transform.parent.GetChild(0).gameObject;
                if (saveHitObject != null && hitObject != saveHitObject) ReturnMaterial();
                canvas = hit.collider.transform.parent.GetChild(1).gameObject;
                canvas.SetActive(true);
                if (saveMaterial.Count == 0)
                {
                    saveMaterial = new List<Material>();
                    FindAllChildren(hitObject, true);
                }
                saveHitObject = hitObject;
            }
            else if (saveHitObject != null && hitObject == saveHitObject) ReturnMaterial();
        }
        else if (saveHitObject && saveMaterial.Count>0) ReturnMaterial();

    }
    void ReturnMaterial()
    {
        k = 0;
        FindAllChildren(saveHitObject, false);
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
    private int FindAllChildren(GameObject obj, bool set)
    {
        int i;
        for (i = 0; i < obj.transform.childCount; i++)
        {
            if (FindAllChildren(obj.transform.GetChild(i).gameObject, set) > 0) continue;
            if (set)
            {
                MeshRenderer mesh = obj.transform.GetChild(i).GetComponent<MeshRenderer>();
                saveMaterial.Add(mesh.material);
                mesh.material = outline[outlineMode];
            }
            else
            {
                obj.transform.GetChild(i).GetComponent<MeshRenderer>().material = saveMaterial[k];
                k++;
            }
        }
        return i;
    }
}
