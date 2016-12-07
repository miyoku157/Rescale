using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class ObjectController : MonoBehaviour {

    private float transitionTime;
	private int index;
	private int baseIndex;

	public int baseScale = 1;

	private float instabilityDelay = 2f;

	private GameObject pivot;

	public bool isUnstable = true;

	private Vector3 offset = Vector3.zero;

	private Vector3 pivotPos = Vector3.zero;

	private Vector3 lastPos;
	private Vector3 targetPos;


    public ThingsRescaleParameters[] thingsRescaleParameter;

	private GameObject gameController;
 
    private bool inSizeTransition;
    private int previousScaleParametersIndex;
    private float transitionBeginTime;

	private bool shouldResetScale = false;

	private bool inEditor = true;

    // Use this for initialization
    void Start () {

		gameController = GameObject.FindWithTag ("GameController");


		instabilityDelay = gameController.GetComponent<GameController> ().delayBeforeScaleReset;

		transitionTime = gameController.GetComponent<GameController> ().objectScaleTransitionTime;

		pivot = transform.GetChild (0).gameObject;




		shouldResetScale= false;
	
        inSizeTransition = false;

		index = baseIndex;


	}

	// Update is called once per frame
	void Update () {

		if (inEditor) {

		

			AdaptSizeInEditor ();

			if (gameController.GetComponent<GameController> ().gameHasStarted == true) {
				
				inEditor = false;

			}

		} else {
			
			UpdateSize ();
		}

		if (Time.time > transitionBeginTime + instabilityDelay) {

			if (shouldResetScale) {

				shouldResetScale = false;

				if (index > baseIndex) {
					ScaleDown ();
				} else {
					ScaleUp ();
				}

			}
			
		}


    }


	void AdaptSizeInEditor(){

		baseIndex = baseScale - 1;

		index = baseIndex;

		float scale = thingsRescaleParameter [baseIndex].sizeScale;


		pivotPos = pivot.transform.position;
		offset = (transform.position- pivotPos ) * scale / transform.localScale.y;
		transform.localScale = new Vector3 (scale, scale, scale);
		transform.position = pivotPos + offset;
	

	}


    public void ScaleUp()
    {
        if (index == thingsRescaleParameter.Length - 1 || inSizeTransition)
            return;
        inSizeTransition = true;
		shouldResetScale = false;
        previousScaleParametersIndex = index;
        transitionBeginTime = Time.time;
		lastPos = transform.position;
		pivotPos = pivot.transform.position;
		offset = (transform.position - pivotPos)*3;
		targetPos = pivotPos + offset;
        index++;

        //setScale();

	}

    public void ScaleDown()
    {
        if (index == 0 || inSizeTransition)
            return;
        inSizeTransition = true;
		shouldResetScale = false;
        previousScaleParametersIndex = index;
        transitionBeginTime = Time.time;
		lastPos = transform.position;
		pivotPos = pivot.transform.position;
		offset = (transform.position - pivotPos)/3;
		targetPos = pivotPos + offset;
	
        index--;



        //setScale();



    }
    
    void UpdateSize()
    {
		
        if (inSizeTransition)
        {
            float transitionPercent = (Time.time - transitionBeginTime) / transitionTime;

			float tScale = thingsRescaleParameter [index].sizeScale;

			Vector3 targetScale = new Vector3(tScale,tScale,tScale);


            if (transitionPercent >= 1.0)
            {
                gameObject.transform.localScale = targetScale;
				gameObject.transform.position = targetPos;
			
                inSizeTransition = false;
				if (index != baseIndex) {

					shouldResetScale = isUnstable;
				
				}
                return;
            }

			float pScale = thingsRescaleParameter [previousScaleParametersIndex].sizeScale;

			Vector3 previousScale = new Vector3(pScale, pScale, pScale);
            
            gameObject.transform.localScale = Vector3.Lerp(previousScale, targetScale, transitionPercent);

			gameObject.transform.position = Vector3.Lerp(lastPos, targetPos, transitionPercent);

	
			//transform.position = pivotPos + offset;

		
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
