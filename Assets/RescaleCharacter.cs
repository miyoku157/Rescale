using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RescaleCharacter : MonoBehaviour {

    public float timeTransition;
    public int index;
    public PlayerScaleParameters[] playerScaleParameters;

	void Start () {
        gameObject.GetComponent<FirstPersonController>().m_WalkSpeed=3;

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newScale = new Vector3(playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale);
        float newFov=playerScaleParameters[index].fieldOfView;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,newFov,2.0f*Time.deltaTime);
        gameObject.transform.localScale = Vector3.Lerp(transform.localScale, newScale, 2.0f * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.I))
        {
            ScaleUp();
        }else if (Input.GetKeyDown(KeyCode.K))
        {
            ScaleDown();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit,200))
            {
                ObjectController controller=hit.transform.gameObject.GetComponent<ObjectController>();
                if (controller != null)
                {
                    controller.ScaleDown();
                }
            }
        }else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,200))
            {
                ObjectController controller = hit.transform.gameObject.GetComponent<ObjectController>();
                if (controller != null)
                {
                    controller.ScaleUp();
                }
            }
        }
    }

    

    void ScaleUp()
    {
        if (index == playerScaleParameters.Length - 1)
            return;
        index++;
        setScale();
    }

    void ScaleDown()
    {
        if (index == 0)
            return;
        index--;
        setScale();

    }

    void setScale()
    {
        
        
        gameObject.GetComponent<Rigidbody>().mass = playerScaleParameters[index].mass;
        FirstPersonController controller = gameObject.GetComponent<FirstPersonController>();
        controller.m_JumpSpeed = playerScaleParameters[index].jumpHeight;
        controller.m_WalkSpeed = playerScaleParameters[index].speed;
        timeTransition = 2.0f;
        /**
            d'autres trucs a faire ici
        */
    }
}
