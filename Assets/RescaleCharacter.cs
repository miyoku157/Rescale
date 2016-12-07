using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RescaleCharacter : MonoBehaviour {

	public float rescaleRange = 300f;
    public float transitionTime=1.0f;
    public int index;
    public PlayerScaleParameters[] playerScaleParameters;

    bool inSizeTransition;
    int previousScaleParametersIndex;
    float transitionBeginTime;

	void Start () {
        //inSizeTransition = false;
        gameObject.GetComponent<FirstPersonController>().m_WalkSpeed=3;

	}
	
	// Update is called once per frame
	void Update () {

        //UpdateSize();



        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!inSizeTransition)
                ScaleUp();

        } else if (Input.GetKeyDown(KeyCode.K))
        {
            if (!inSizeTransition)
                ScaleDown();
        }

        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           

		
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 20)) 

			{
				ObjectController controller = hit.transform.gameObject.GetComponent<ObjectController>();

				if (controller != null)
				{
					controller.ScaleDown();
				}
			}
		
        } else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 20)) 
				
            {
                ObjectController controller = hit.transform.gameObject.GetComponent<ObjectController>();

                if (controller != null)
                {
                    controller.ScaleUp();
                }
            }
        }
    }
    IEnumerator setSize()
    {
        float transitionPercent = 0.0f;
        float targetFov = 0.0f;
        Vector3 targetScale = new Vector3();
        while (transitionPercent < 1.0) {

            float t=Time.time;
            transitionPercent = (Time.time - transitionBeginTime) / 1.0f;
            targetScale = new Vector3(playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale);
            targetFov = playerScaleParameters[index].fieldOfView;
            Vector3 previousScale = new Vector3(playerScaleParameters[previousScaleParametersIndex].sizeScale, playerScaleParameters[previousScaleParametersIndex].sizeScale, playerScaleParameters[previousScaleParametersIndex].sizeScale);
            float previousFov = playerScaleParameters[previousScaleParametersIndex].fieldOfView;

            Camera.main.fieldOfView = Mathf.Lerp(previousFov, targetFov, transitionPercent);
            gameObject.transform.localScale = Vector3.Lerp(previousScale, targetScale, transitionPercent);
            yield return new WaitForEndOfFrame();
        }
        Camera.main.fieldOfView = targetFov;
        gameObject.transform.localScale = targetScale;
		inSizeTransition = false;

    }
    /*void UpdateSize()
    {
        if (inSizeTransition)
        {
            float transitionPercent = (Time.time - transitionBeginTime) / transitionTime;

            Vector3 targetScale = new Vector3(playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale);
            float targetFov = playerScaleParameters[index].fieldOfView;

            if (transitionPercent >= 1.0)
            {
                Camera.main.fieldOfView = targetFov;
                gameObject.transform.localScale = targetScale;
                inSizeTransition = false;
                return;
            }

            Vector3 previousScale = new Vector3(playerScaleParameters[previousScaleParametersIndex].sizeScale, playerScaleParameters[previousScaleParametersIndex].sizeScale, playerScaleParameters[previousScaleParametersIndex].sizeScale);
            float previousFov = playerScaleParameters[previousScaleParametersIndex].fieldOfView;

            Camera.main.fieldOfView = Mathf.Lerp(previousFov, targetFov, transitionPercent);
            gameObject.transform.localScale = Vector3.Lerp(previousScale, targetScale, transitionPercent);
        }
    }*/

    void ScaleUp()
    {
        if (index == playerScaleParameters.Length - 1)
            return;
        inSizeTransition = true;
        previousScaleParametersIndex = index;
        transitionBeginTime = Time.time;
        index++;
        setScale();
    }

    void ScaleDown()
    {
        if (index == 0)
            return;
        inSizeTransition = true;
        previousScaleParametersIndex = index;
        transitionBeginTime = Time.time;
        index--;
        setScale();

    }

    void setScale()
    {

        gameObject.GetComponent<Rigidbody>().mass = playerScaleParameters[index].mass;
        FirstPersonController controller = gameObject.GetComponent<FirstPersonController>();
        controller.m_JumpSpeed = playerScaleParameters[index].jumpHeight;
        controller.m_WalkSpeed = playerScaleParameters[index].speed;
        StartCoroutine("setSize");
        /**
            d'autres trucs a faire ici
        */
    }
}
