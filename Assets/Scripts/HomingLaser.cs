using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    private Transform _target;
    private Rigidbody2D _rb;
    private float _speed = 8f;
    private float _rotateSpeed = 500f;


    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Enemy").transform;



        _rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (_target !=null)
        {
            Vector2 direction = (Vector2)_target.position - _rb.position;
            direction.Normalize();

            float rotateAngle = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAngle * _rotateSpeed;

            _rb.velocity = transform.up * _speed;

          /*  if(transform.position.y >7f || transform.position.y <-7f || transform.position.x > 11.3f || transform.position.x < -11.3f)
            {
                if(transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }

                Destroy(this.gameObject);

            } */

        }
       
    }
}
