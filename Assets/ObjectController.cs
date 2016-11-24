using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

    public ThingsRescaleParameters[] thingsRescaleParameter;
    int index=1;
    Vector3 baseScale;
    // Use this for initialization
    void Start () {
        baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void ScaleUp()
    {
        if (index == thingsRescaleParameter.Length - 1)
            return;
        index++;
        setScale();
    }

    public void ScaleDown()
    {
        if (index == 0)
            return;
        index--;
        setScale();

    }
    void setScale()
    {
        if (index == 1)
        {
            this.gameObject.transform.localScale = baseScale;
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(transform.localScale.x * thingsRescaleParameter[index].sizeScale, transform.localScale.y * thingsRescaleParameter[index].sizeScale, transform.localScale.z * thingsRescaleParameter[index].sizeScale);
        }// this.gameObject.GetComponent<Rigidbody>().mass = playerScaleParameters[index].mass;
    }
}
