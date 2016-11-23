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
        FirstPersonController controller = gameObject.GetComponent<FirstPersonController>();
        /**
            d'autres trucs a faire ici
        */
    }
}
