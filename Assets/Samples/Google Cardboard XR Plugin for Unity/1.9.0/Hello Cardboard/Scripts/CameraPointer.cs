//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    public Transform grab;
    public float throwForce;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        if (Input.GetButtonDown("Grab"))
        {
          RaycastHit hit;
          if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
          {
              // GameObject detected in front of the camera.
              if (_gazedAtObject == null)
              {
                  // New GameObject.
                  PickUp(hit.transform.gameObject);
                }
              }
              else
              {
              // No GameObject detected in front of the camera.
              ThrowIt();
              }
        }
        if (_gazedAtObject != null)
        {

        }

    }

    public void PickUp(GameObject pickUpObject)
    {
      Rigidbody objectRigidbody=pickUpObject.GetComponent<Rigidbody>();
      if(objectRigidbody)
      {
        objectRigidbody.useGravity=false;
        objectRigidbody.velocity=Vector3.zero;
        objectRigidbody.angularVelocity=Vector3.zero;
        objectRigidbody.transform.parent=GameObject.Find("Grab").transform;
        _gazedAtObject = pickUpObject;
        objectRigidbody.transform.position = grab.position;
      }

    }

    public void ThrowIt()
    {
      Rigidbody objectRigidbody=_gazedAtObject.GetComponent<Rigidbody>();
      objectRigidbody.transform.parent =null;
      objectRigidbody.useGravity=true;
      objectRigidbody.AddForce(this.transform.forward*throwForce);
      _gazedAtObject = null;
    }
}
