using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

    public float transitionTime;
    public int index = 1;
    public ThingsRescaleParameters[] thingsRescaleParameter;
    
    Vector3 baseScale;
    bool inSizeTransition;
    int previousScaleParametersIndex;
    float transitionBeginTime;

    // Use this for initialization
    void Start () {
        inSizeTransition = false;
        baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSize();
    }
    public void ScaleUp()
    {
        if (index == thingsRescaleParameter.Length - 1 || inSizeTransition)
            return;
        inSizeTransition = true;
        previousScaleParametersIndex = index;
        transitionBeginTime = Time.time;
        index++;
        //setScale();
    }

    public void ScaleDown()
    {
        if (index == 0 || inSizeTransition)
            return;
        inSizeTransition = true;
        previousScaleParametersIndex = index;
        transitionBeginTime = Time.time;
        index--;
        //setScale();

    }
    
    void UpdateSize()
    {
        if (inSizeTransition)
        {
            float transitionPercent = (Time.time - transitionBeginTime) / transitionTime;

            Vector3 targetScale = new Vector3(thingsRescaleParameter[index].sizeScale, thingsRescaleParameter[index].sizeScale, thingsRescaleParameter[index].sizeScale);

            if (transitionPercent >= 1.0)
            {
                gameObject.transform.localScale = targetScale;
                inSizeTransition = false;
                return;
            }

            Vector3 previousScale = new Vector3(thingsRescaleParameter[previousScaleParametersIndex].sizeScale, thingsRescaleParameter[previousScaleParametersIndex].sizeScale, thingsRescaleParameter[previousScaleParametersIndex].sizeScale);
            
            gameObject.transform.localScale = Vector3.Lerp(previousScale, targetScale, transitionPercent);
        }
    }
    /*   void setScale()
       {
           if (index == 1)
           {
               this.gameObject.transform.localScale = baseScale;
           }
           else
           {
               this.gameObject.transform.localScale = new Vector3(transform.localScale.x * thingsRescaleParameter[index].sizeScale, transform.localScale.y * thingsRescaleParameter[index].sizeScale, transform.localScale.z * thingsRescaleParameter[index].sizeScale);
           }// this.gameObject.GetComponent<Rigidbody>().mass = playerScaleParameters[index].mass;
       }*/
}
