using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    GameObject blast;
    // Start is called before the first frame update
    void Start()
    {
        blast = transform.Find("blast").gameObject;
        blast.SetActive(false);
    }
    public void ShowBlast()
    {
        blast.SetActive(true);
    }


}
