using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CombustibleController : MonoBehaviour
{
   private float _horizon;
   private float _vertical;
   public MirrorUIController mirrorUIController;
   public float speed;
   private void Update()
   {
      _horizon = Input.GetAxis("Horizontal");
      _vertical = Input.GetAxis("Vertical");
      Move();
   }

   private void Move()
   {
      if (mirrorUIController.canControl)
      {
         transform.position += new Vector3(_horizon, _vertical, 0) * (speed * Time.deltaTime);
      }
   }
}
