using System.Collections;
using UnityEngine;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class PickUp : MonoBehaviour
{
    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float _minObjectDistance = 2.5f;
    private const float _maxObjectDistance = 3.5f;
    private const float _minObjectHeight = 0.5f;
    private const float _maxObjectHeight = 3.5f;


    //Place where the object is gonna go once we grab it.
    public Transform grab;
    private bool grabbing;
    private bool buttonPressed;

    public void Update()
    {
      buttonPressed = Input.GetButtonDown("Grab");
    }


    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
      if (buttonPressed && !grabbing)
      {
        PickUpItem();
      }
      if (buttonPressed && grabbing)
      {
        LetItemGo();
      }
    }

    public void PickUpItem()
    {
      GetComponent<Rigidbody>().useGravity=false;
      this.transform.position=grab.position;
      this.transform.parent=GameObject.Find("Grab").transform;
    }

    public void LetItemGo()
    {
      this.transform.parent = GameObject.Find("Environment").transform;
      GetComponent<Rigidbody>().useGravity=true;
    }


}
