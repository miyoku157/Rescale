using UnityEngine;
using System.Collections;


public class ThingsRescaleParameters : MonoBehaviour {

    public float sizeScale = 1;


    public float mass = 1;

    /**
        Ajouter d'autres trucs ici
    */

    public ThingsRescaleParameters()
    {
        sizeScale = 1;
        mass = 1;
    }
    public ThingsRescaleParameters(float _sizeScale, float _mass)
    {
        sizeScale = _sizeScale;
        mass = _mass;
    }
}
