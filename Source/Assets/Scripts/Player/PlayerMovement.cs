using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Judėjimo greičiai ir mygtukai

    float speed = 5f;
    float runSpeed = 10f;
    float rotationSpeed = 5f;
    string mFrontBack = "Vertical";
    string mLeftRight = "Horizontal";
    string mMouseX = "Mouse X";
    string mMouseY = "Mouse Y";
    string mRunning = "Shift";

    private CorePlayer core;

    // Judėjimas

    private float _xMovement;
    private float _zMovement;

    private Vector3 _moveHorizontal;
    private Vector3 _moveVertical;
    private Vector3 _velocity;

    // Pasisukimas

    private float _yRotation;

    private Vector3 _rotation;

    // Pavertimas

    private float _xTilt;

    private Vector3 _tilt;

    private void Start()
    {
        core = GetComponent<CorePlayer>();
    }

    void Update()
    {
        // Judėjimo apskaičiavimas
        _xMovement = Input.GetAxisRaw(mLeftRight);
        _zMovement = Input.GetAxisRaw(mFrontBack);

        // Inputu vertimas i vektorius
        _moveHorizontal = transform.right * _xMovement;
        _moveVertical = transform.forward * _zMovement;

        // Galutinis judejimo skaiciavimas
        if (Input.GetButton(mRunning))
        {
            _velocity = (_moveHorizontal + _moveVertical).normalized * runSpeed;
        }
        else
        {
            _velocity = (_moveHorizontal + _moveVertical).normalized * speed;
        }
        //Judesio aktyvavimas
        core.Movement(_velocity);

        // Pasisukimo apskaičiavimas
        _yRotation = Input.GetAxisRaw(mMouseX);

        _rotation = new Vector3(0f, _yRotation, 0f) * rotationSpeed;

        //Pasisukimo aktyvavimas
        core.Rotation(_rotation);

        //Pavertimo apskaičiavimas
        _xTilt = Input.GetAxisRaw(mMouseY);

        _tilt = new Vector3(_xTilt, 0f, 0f) * rotationSpeed;

        // Pavertimas aktyvavimas

        core.Tilt(_tilt);
    }
}
