using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RescaleCharacter : MonoBehaviour {

    // Use this for initialization
    float[] size = { 0.125f, 0.25f, 0.5f, 1 };
    int index = 3;
	void Start () {
        gameObject.GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ScaleUp();
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            ScaleDown();
        }
	}

    void ScaleUp()
    {
        index++;
        this.gameObject.transform.localScale = new Vector3(size[index], size[index], size[index]);
        Camera.main.fieldOfView += (index + 1) * 5;
        
    }

    void ScaleDown()
    {
        index--;
        this.gameObject.transform.localScale = new Vector3(size[index], size[index], size[index]);
        Camera.main.fieldOfView -= (index + 1) * 5;

    }
}
