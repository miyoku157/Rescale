using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RescaleCharacter : MonoBehaviour {


    public int index;
    public PlayerScaleParameters[] playerScaleParameters;

	void Start () {
        gameObject.GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
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
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd,out hit))
            {
                hit.transform.localScale = new Vector3(hit.transform.localScale.x / 2, hit.transform.localScale.y / 2, hit.transform.localScale.z / 2);
            }
        }else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                hit.transform.localScale = new Vector3(hit.transform.localScale.x * 2, hit.transform.localScale.y * 2, hit.transform.localScale.z * 2);
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
        this.gameObject.transform.localScale = new Vector3(playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale, playerScaleParameters[index].sizeScale);
        Camera.main.fieldOfView = playerScaleParameters[index].fieldOfView;
        this.gameObject.GetComponent<Rigidbody>().mass = playerScaleParameters[index].mass;
        FirstPersonController controller = gameObject.GetComponent<FirstPersonController>();
        controller.m_JumpSpeed = playerScaleParameters[index].jumpHeight;
        controller.m_WalkSpeed = playerScaleParameters[index].speed;
        /**
            d'autres trucs a faire ici
        */
    }
}
