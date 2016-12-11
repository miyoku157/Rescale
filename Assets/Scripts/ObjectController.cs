using UnityEngine;
using System.Collections;

[ExecuteInEditMode]


public class ObjectController : MonoBehaviour {

	public enum type{regular, unstable , auto};

	public type cubeType;


    private float transitionTime;
	private int index;
	private int baseIndex;

	public int baseScale = 1; 


	public float autoScaleDelay = 1f;

	public float autoScaleTimer;



	private float instabilityDelay = 2f;

	private GameObject pivot;
	private GameObject insideCube;

	private Vector3 insideCubeBaseScale;

	private bool isUnstable;

	private bool isAuto;

	public int autoStep;



	private Vector3 offset = Vector3.zero;

	private Vector3 pivotPos = Vector3.zero;

	private Vector3 lastPos;
	private Vector3 targetPos;


    public ThingsRescaleParameters[] thingsRescaleParameter;

	private GameObject gameController;
 
    private bool inSizeTransition;
    private int previousScaleParametersIndex;
    private float transitionBeginTime;
	private float transitionEndTime;

	private bool shouldResetScale = false;

	private bool inEditor = true;

    // Use this for initialization
    void Start () {

		gameController = GameObject.FindWithTag ("GameController");


		instabilityDelay = gameController.GetComponent<GameController> ().unstableDelay;
		transitionTime = gameController.GetComponent<GameController> ().objectRescaleTime;



		pivot = transform.GetChild (0).gameObject;

		//insideCube = transform.GetChild (1).GetChild (0).gameObject;
		//insideCubeBaseScale = insideCube.transform.localScale;

		shouldResetScale= false;

		inSizeTransition = false;

		index = baseIndex;


		if (cubeType == type.regular) {

			isUnstable = false;
			isAuto = false;
		}
		else if (cubeType == type.unstable) {
			
			isUnstable = true;
			isAuto = false;
		}
		else if (cubeType == type.auto) {
		
			transitionTime = autoScaleDelay;

			isUnstable = false;
			isAuto = true;


			autoStep = 0;

		}




	}

	// Update is called once per frame
	void Update () {

		if (inEditor) {

			AdaptSizeInEditor ();

			if (gameController.GetComponent<GameController> ().gameHasStarted == true) {
				
				LateInit ();
			}

		} else {
			
			UpdateSize ();
		}

		if (isAuto) {

			if (autoStep !=0 && Time.time > autoScaleTimer + autoScaleDelay) {

				autoScaleTimer = Time.time;

				if(autoStep==1){
					
					TriggerScaleDown ();
					autoStep = 2;
				}

				else if(autoStep==2){

					TriggerScaleUp ();
					autoStep = 1;
				}
			}
		}

		if (Time.time > transitionEndTime + instabilityDelay) {

			if (shouldResetScale) {

				shouldResetScale = false;

				if (index > baseIndex) {
					TriggerScaleDown ();
				} else {
					TriggerScaleUp ();
				}

			}
			
		}


    }

	void LateInit(){

		inEditor = false;

		if (cubeType == type.auto) {


			TriggerScaleDown ();
			autoStep = 2;
			autoScaleTimer = Time.time;
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
		

	private void TriggerScaleUp(){

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

	private void TriggerScaleDown(){

	
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

    public bool TryScaleUp()
    {
		if (index == thingsRescaleParameter.Length - 1 || isAuto || inSizeTransition) {
			return false;
		}

	 else {

		TriggerScaleUp();

		return true;
		}

	}


    public bool TryScaleDown()
    {
		if (index == 0 || isAuto || inSizeTransition) {
			return false;
		} else {

			TriggerScaleDown();

			return true;

		}



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

				transitionEndTime = Time.time;
			
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
