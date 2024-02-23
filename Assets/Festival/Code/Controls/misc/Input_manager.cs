using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnValueCHanged()
    {
        Debug.Log("OnValueCHanged");
    }

    public void OnEndEdit()
    {
        Debug.Log("OnEndEdit");
    }

    public void Select()
    {
        Debug.Log("Select");
    }

    public void Deselect()
    {
        Debug.Log("Deselect");
    }

}
