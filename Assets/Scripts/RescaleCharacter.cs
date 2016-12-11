using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RescaleCharacter : MonoBehaviour {


	public bool isAlive = true;
	public bool wasAlive = true;
	public float energyCost = 10f;
	public float baseEnergyRegen = 1f;
	public float lightEnergyRegen = 1f;
	public LayerMask mask;
	public float rescaleRange = 300f;
	public float transitionTime=1.0f;
	public int index;
	public PlayerScaleParameters[] playerScaleParameters;

	public RectTransform _energyBar = null;

	public Text deathByCrushingText = null;

	public float maxEnergy = 100f;

	private float energy;
	private float energyTimer;

	private bool isInLight = false;

	bool inSizeTransition;
	int previousScaleParametersIndex;
	float transitionBeginTime;

	bool contactWithDynamic = false;

	bool contactRight,contactLeft,contactTop,contactBottom,contactFront,contactBack;

	public float contactOffset = 0.1f;

	private FirstPersonController fpc = null;



	void Start () {

		fpc = gameObject.GetComponent<FirstPersonController> ();

		deathByCrushingText.enabled = false;

		inSizeTransition = false;

		//gameObject.GetComponent<FirstPersonController>().m_WalkSpeed=3;

		energy = maxEnergy;

		isInLight = false;

		fpc.m_GravityMultiplier = playerScaleParameters[index].grav;

		fpc.m_JumpSpeed = playerScaleParameters[index].jumpHeight;
		fpc.m_WalkSpeed = playerScaleParameters[index].speed;
		fpc.lethalFallHeight = playerScaleParameters [index].lethalFallHeight;
		float tscale = playerScaleParameters [index].sizeScale;

		Camera.main.fieldOfView = playerScaleParameters[index].fieldOfView;
		gameObject.transform.localScale = new Vector3(tscale, tscale, tscale);
		inSizeTransition = false;

		//setScale ();


	}


	// Update is called once per frame
	void Update () {

		ProximityCheck ();

		//Debug.Log ("contact with dynamic : " + contactWithDynamic);


		isAlive = fpc.isAlive;

		if (wasAlive && !isAlive) {

			PlayerDeath ();
		}

		if (!wasAlive && isAlive) {
			PlayerUnDeath ();
		}



		wasAlive = isAlive;

		UpdateEnergy();



		if (Input.GetKeyDown (KeyCode.I)) {
			if (index != playerScaleParameters.Length - 1 && !inSizeTransition && energy - energyCost >= 0f) {
				ScaleUp ();
				energy -= energyCost;
			}

		} else if (Input.GetKeyDown (KeyCode.K)) {
			if (index != 0 && !inSizeTransition && energy - energyCost >= 0f) {
				ScaleDown ();
				energy -= energyCost;
			}

		} else if (Input.GetKeyDown (KeyCode.Mouse0)) {


			if (energy - energyCost >= 0f) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;


				if (Physics.Raycast (ray, out hit, rescaleRange, mask)) {
					Debug.Log ("Hit Object: " + hit.collider.gameObject.name);

					ObjectController controller = hit.transform.gameObject.GetComponent<ObjectController> ();

					if (controller != null) {
						bool test = controller.TryScaleDown ();
						if(test) energy -= energyCost;
					}
				}
			}

		} else if (Input.GetKeyDown (KeyCode.Mouse1)) {
			if (energy - energyCost >= 0f) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, rescaleRange, mask)) {
					Debug.Log ("Hit Object: " + hit.collider.gameObject.name);

					ObjectController controller = hit.transform.gameObject.GetComponent<ObjectController> ();

					if (controller != null) {
						bool test = controller.TryScaleUp ();
						if(test) energy -= energyCost;
					}
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



	private void PlayerDeath(){

		deathByCrushingText.enabled = true;

	}

	private void PlayerUnDeath(){

		deathByCrushingText.enabled = false;

	}

	void ScaleUp()
	{

		inSizeTransition = true;
		previousScaleParametersIndex = index;
		transitionBeginTime = Time.time;
		index++;
		setScale();
	}

	void ScaleDown()
	{

		inSizeTransition = true;
		previousScaleParametersIndex = index;
		transitionBeginTime = Time.time;
		index--;
		setScale();

	}

	void setScale()
	{

		fpc.m_GravityMultiplier = playerScaleParameters[index].grav;

		fpc.m_JumpSpeed = playerScaleParameters[index].jumpHeight;
		fpc.m_WalkSpeed = playerScaleParameters[index].speed;
		fpc.lethalFallHeight = playerScaleParameters [index].lethalFallHeight;

		StartCoroutine("setSize");
		/**
            d'autres trucs a faire ici
        */
	}

	void UpdateEnergy(){

		energy += ((isInLight)? lightEnergyRegen : baseEnergyRegen) * Time.deltaTime;

		if (energy > maxEnergy) {
			energy = maxEnergy;
		}

		_energyBar.sizeDelta = new Vector2(20,200*(float)energy/maxEnergy);
	}

	void OnTriggerStay(Collider other){


		if(other.CompareTag("Light")){

			isInLight = true;
		}


	}



	void OnTriggerExit(Collider other){

		if(other.CompareTag("Light")){

			isInLight = false;
		}

	}

	void ProximityCheck(){

		contactRight = false;
		contactLeft = false;
		contactTop = false;
		contactBottom = false;
		contactFront = false;
		contactBack = false;

		RaycastHit hit;

		float contactDistance = contactOffset + 0.5f * fpc.GetComponent<CharacterController> ().height * transform.localScale.y;

		if (Physics.Raycast (transform.position, -transform.up, out hit, contactDistance, mask)) {

			contactBottom = true;
		}

		if (Physics.Raycast (transform.position, transform.up, out hit, contactDistance, mask)) {

			contactTop = true;
		}

		contactDistance = contactOffset + fpc.GetComponent<CharacterController> ().radius * transform.localScale.x;

		if (Physics.Raycast (transform.position, transform.forward, out hit, contactDistance, mask)) {

			contactFront = true;
		}

		if (Physics.Raycast (transform.position, -transform.forward, out hit, contactDistance, mask)) {

			contactBack = true;
		}

		contactDistance = contactOffset + fpc.GetComponent<CharacterController> ().radius * transform.localScale.x;

		if (Physics.Raycast (transform.position, -transform.right, out hit, contactDistance, mask)) {

			contactLeft = true;
		}

		if (Physics.Raycast (transform.position, transform.right, out hit, contactDistance, mask)) {

			contactRight = true;
		}

		if (contactRight && contactLeft) {

			fpc.isAlive = false;
		}





	}




}
