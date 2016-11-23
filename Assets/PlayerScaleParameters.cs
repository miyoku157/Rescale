using UnityEngine;
using System.Collections;

public class PlayerScaleParameters : MonoBehaviour {

    public float sizeScale = 1;

    public float fieldOfView = 70;

    public float speed = 1;

    public float mass = 1;

    public float jumpHeight = 1;

    /**
        Ajouter d'autres trucs ici
    */

    public PlayerScaleParameters()
    {
        sizeScale = 1;
        fieldOfView = 70;
        speed = 1;
        mass = 1;
        jumpHeight = 1;
    }
    public PlayerScaleParameters(float _sizeScale, float _fov, float _speed, float _mass, float _jmp)
    {
        sizeScale = _sizeScale;
        fieldOfView = _fov;
        speed = _speed;
        mass = _mass;
        jumpHeight = _jmp;
    }
}