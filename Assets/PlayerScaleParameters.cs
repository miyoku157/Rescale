using UnityEngine;
using System.Collections;

public class PlayerScaleParameters : ThingsRescaleParameters
{


    public float fieldOfView = 70;

    public float speed = 1;

    public float jumpHeight = 1;

    /**
        Ajouter d'autres trucs ici
    */

    public PlayerScaleParameters():base()
    {
        fieldOfView = 70;
        speed = 1;
        jumpHeight = 1;
    }
    public PlayerScaleParameters(float _sizeScale, float _fov,float _mass, float _speed, float _jmp):base(_sizeScale, _mass)
    {
        fieldOfView = _fov;
        speed = _speed;
        jumpHeight = _jmp;
    }
}