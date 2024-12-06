using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PickupScript : MonoBehaviour
{

    public GameObject player;
    public Transform holdPos;
    public Camera cam;

    private bool buttonPressed = false;
    private bool canPickUp = false;

    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up

    private GameObject current;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObj == null) //if currently not holding anything
        {
            //perform raycast to check if player is looking at object within pickuprange
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                //make sure pickup tag is attached
                if (hit.transform.gameObject.tag == "PickupObject")
                {
                    current = hit.transform.gameObject;
                    canPickUp = true;
                }
            }
            else
            {
                current = null;
                canPickUp=false;
            }
        }
        else
        {

            DropObject();
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //if (buttonPressed)
        //{
        //    buttonPressed = false;
        //}
        //else
        //{
        //    buttonPressed = true;
        //}

        if (canPickUp)
        {
            //pass in object hit into the PickUpObject function
            PickUpObject(current);
        }
        

        Debug.Log("im here atleast");
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            if (Input.GetKeyDown(KeyCode.Mouse0)) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                ThrowObject();
            }

        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            //heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
  
    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(cam.transform.forward * throwForce);
        heldObj = null;

    }

}
