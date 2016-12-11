using UnityEngine;
using System.Collections;

public class PlayerScaleParameters : ThingsRescaleParameters
{


    public float fieldOfView = 70;

    public float speed = 1;

    public float jumpHeight = 1;

	public float lethalFallHeight = 1;

	public float grav = 1;

    /**
        Ajouter d'autres trucs ici
    */

    public PlayerScaleParameters():base()
    {
        fieldOfView = 70;
        speed = 1;
        jumpHeight = 1;
		lethalFallHeight = 1f;
		grav = 1;
    }
	public PlayerScaleParameters(float _sizeScale, float _fov,float _grav, float _speed, float _jmp, float _fall):base(_sizeScale, _grav)
    {
        fieldOfView = _fov;
        speed = _speed;
        jumpHeight = _jmp;
		lethalFallHeight = _fall;
		grav = _grav;
    }
}