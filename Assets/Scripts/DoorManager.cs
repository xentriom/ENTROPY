using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    [SerializeField]
    private bool hoveringButton = false;


    public bool HoveringButton
    {
        get { return hoveringButton; }
        set { hoveringButton = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
