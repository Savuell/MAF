using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPortCable : MonoBehaviour
{
    [SerializeField] private GameObject attachmentWorldPos;
    [SerializeField] private GameObject attachmentLocal;
    [SerializeField] private GameObject cablePartPrefab;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private float step;
    [SerializeField] private float n, m, k;
    private int partsCount;
    [SerializeField] private List<GameObject> cableParts = new List<GameObject>(); 
    void Update()
    {
        transform.forward = attachmentWorldPos.transform.position - transform.position;
        transform.eulerAngles = new(0, transform.eulerAngles.y, transform.eulerAngles.z);
        attachmentLocal.transform.position = attachmentWorldPos.transform.position;
        partsCount = (int)(attachmentLocal.transform.localPosition.z / step);
        if (partsCount > 600)
        {
            foreach(GameObject part in cableParts)
            {
                if (Random.Range(0, 6) != 1)
                {
                    GameObject particle;
                    if(Random.Range(0,9)==1) particle = Instantiate(particlesPrefab, part.transform.position, Quaternion.identity);
                    Destroy(part);
                }
                else part.transform.GetChild(0).GetComponent<Collider>().enabled = true;
            }
            Destroy(attachmentWorldPos.transform.parent.GetChild(0).gameObject);
            attachmentWorldPos.transform.parent = null;
            Destroy(this);
        }
        else SetCable();
    }
    void SetCable()
    {
        Vector3 pos = attachmentLocal.transform.localPosition;
        pos.y = -pos.y;
        m = (n * pos.z / pos.y) * (1 - Mathf.Sqrt(Mathf.Max(1 - pos.y / n, 0)));
        k = n / (m * m);
        int delta = Mathf.Abs(partsCount - cableParts.Count);
        if (cableParts.Count < partsCount) for (int i = 0; i < delta; i++) cableParts.Add(Instantiate(cablePartPrefab));
        if (cableParts.Count > partsCount) for (int i = 0; i < delta; i++)
            {
                Destroy(cableParts[cableParts.Count - 1]);
                cableParts.RemoveAt(cableParts.Count - 1);
            }
        int c = 0;
        foreach (GameObject part in cableParts)
        {
            float x = step * c;
            float y = k * (x - m) * (x - m) - n;
            part.transform.parent = transform;
            part.transform.localPosition = new(0, y, x);
            if (c > 0) part.transform.forward = cableParts[c - 1].transform.position - part.transform.position;
            c++;
        }
    }
}
