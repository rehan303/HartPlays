using UnityEngine;
using System.Collections;

public class DragableObject : MonoBehaviour 
{
	private GameObject GameObject_toDrag;
	public Transform RestrictedPositionX;
	public Transform RestrictedPositionY;

	private Vector3 offset;
	private Vector3 GameObject_WorldPosition;
	private float Z_Dist;
	bool dragging = false;

	public bool isRotationEnabled = false;
	public bool ColliderSizeVariable = false;
	public float LerpSpeed=0.5f;

	int randomPowerUp;
	DogerPowerUpController DodgerPowerUpContoller;

	//

	public static int TapCount;
	public float MaxDubbleTapTime;
	float NewTime;

	//


	void Start()
	{
		DodgerPowerUpContoller = GameObject.Find ("Doger_PowerUpController").GetComponent<DogerPowerUpController>();
	}


	void Update () 
	{

		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) 
			{
				if (hit.collider.gameObject == this.gameObject) 
				{
					GameObject_toDrag = hit.collider.gameObject;
					Z_Dist = hit.transform.position.z - Camera.main.transform.position.z;
					GameObject_WorldPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Z_Dist);
					if(ColliderSizeVariable)
						GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1.2f,1.2f);
					GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
					offset = GameObject_toDrag.transform.position - GameObject_WorldPosition;
					dragging = true;
				} 
			} 
		}

		if (dragging) 
		{
			GameObject_WorldPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Z_Dist);
			GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
			if (GameObject_toDrag) 
			{
				if(isRotationEnabled)
				{
					RotateObject (GameObject_WorldPosition);
					GameObject_toDrag.transform.position =  Vector3.Lerp (transform.position, GameObject_WorldPosition + offset, LerpSpeed * Time.deltaTime);
				}
				else
				{
					GameObject_toDrag.transform.position =  GameObject_WorldPosition + offset;
				}
				Vector3 restricted_Position = GameObject_toDrag.transform.position;
				restricted_Position.x = Mathf.Clamp (restricted_Position.x, -RestrictedPositionX.position.x, RestrictedPositionX.position.x);
				restricted_Position.y = Mathf.Clamp (restricted_Position.y, -RestrictedPositionY.position.y, RestrictedPositionY.position.y);
				restricted_Position.z = Mathf.Clamp (restricted_Position.z, 0, 0);
				GameObject_toDrag.transform.position = restricted_Position;
			} 
			else 
			{
				if (dragging) 
				{
					if(ColliderSizeVariable)
						GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1.6f,1.6f);
					dragging = false;
					GameObject_toDrag = null;
				} 
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			if (dragging) 
			{
				if(ColliderSizeVariable)
					GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1.6f,1.6f);
				dragging = false;		
				GameObject_toDrag = null;
			} 
		}

		else
		{
			return;
		}

		#endif
		int touchCount = Input.touchCount;
		if (this.gameObject.name == "MainPlayer") {
			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Ended) {
					TapCount += 1;
				}

				if (TapCount == 1) {

					NewTime = Time.time + MaxDubbleTapTime;
					print ("Singel tap");
				} else if (TapCount == 2 && Time.time <= NewTime) {
					RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

					if (hit.collider == null) {
						//Whatever you want after a dubble tap    
						print ("Dubble tap");
						randomPowerUp = Random.Range (1, 5);
						if (HartPlayerRegistration.Isplayforpaid) {
							if (randomPowerUp == 1) {
								if (PlayerPrefs.GetInt ("DodgerBombCountForPaid") > 0)
									DodgerPowerUpContoller.BombPowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid") > 0)
										DodgerPowerUpContoller.InvinciblePowerUp ();
									else if (PlayerPrefs.GetInt ("DodgerSlowCountForPaid") > 0)
										DodgerPowerUpContoller.SlowDownPowerUp ();
									else if (DogerGameManager.isSliptEnabled) {					
										if (PlayerPrefs.GetInt ("DodgerMergeCountForPaid") > 0)
											DodgerPowerUpContoller.MergePowerUp ();
									}
								}
							} else if (randomPowerUp == 2) {
								if (PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid") > 0)
									DodgerPowerUpContoller.InvinciblePowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerBombCountForPaid") > 0)
										DodgerPowerUpContoller.BombPowerUp ();
									else if (PlayerPrefs.GetInt ("DodgerSlowCountForPaid") > 0)
										DodgerPowerUpContoller.SlowDownPowerUp ();
									else if (DogerGameManager.isSliptEnabled) {					
										if (PlayerPrefs.GetInt ("DodgerMergeCountForPaid") > 0)
											DodgerPowerUpContoller.MergePowerUp ();
									}
								}
							} else if (randomPowerUp == 3) {
								if (PlayerPrefs.GetInt ("DodgerSlowCountForPaid") > 0)
									DodgerPowerUpContoller.SlowDownPowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerBombCountForPaid") > 0)
										DodgerPowerUpContoller.BombPowerUp ();
									if (PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid") > 0)
										DodgerPowerUpContoller.InvinciblePowerUp ();
									else if (DogerGameManager.isSliptEnabled) {					
										if (PlayerPrefs.GetInt ("DodgerMergeCountForPaid") > 0)
											DodgerPowerUpContoller.MergePowerUp ();
									}
								}

							} else if (randomPowerUp == 4 && DogerGameManager.isSliptEnabled) {					
								if (PlayerPrefs.GetInt ("DodgerMergeCountForPaid") > 0)
									DodgerPowerUpContoller.MergePowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerBombCountForPaid") > 0)
										DodgerPowerUpContoller.BombPowerUp ();
									if (PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid") > 0)
										DodgerPowerUpContoller.InvinciblePowerUp ();
									else if (PlayerPrefs.GetInt ("DodgerSlowCountForPaid") > 0)
										DodgerPowerUpContoller.SlowDownPowerUp ();
							
								}
							} else {
								if (PlayerPrefs.GetInt ("DodgerBombCountForPaid") > 0)
									DodgerPowerUpContoller.BombPowerUp ();
								if (PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid") > 0)
									DodgerPowerUpContoller.InvinciblePowerUp ();
								else if (PlayerPrefs.GetInt ("DodgerSlowCountForPaid") > 0)
									DodgerPowerUpContoller.SlowDownPowerUp ();
							}
						} else {
							if (randomPowerUp == 1) {
								if (PlayerPrefs.GetInt ("DodgerBombCount") > 0)
									DodgerPowerUpContoller.BombPowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerInvincibleCount") > 0)
										DodgerPowerUpContoller.InvinciblePowerUp ();
									else if (PlayerPrefs.GetInt ("DodgerSlowCount") > 0)
										DodgerPowerUpContoller.SlowDownPowerUp ();
									else if (DogerGameManager.isSliptEnabled) {					
										if (PlayerPrefs.GetInt ("DodgerMergeCount") > 0)
											DodgerPowerUpContoller.MergePowerUp ();
									}
								}
							} else if (randomPowerUp == 2) {

								if (PlayerPrefs.GetInt ("DodgerInvincibleCount") > 0)
									DodgerPowerUpContoller.InvinciblePowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerBombCount") > 0)
										DodgerPowerUpContoller.BombPowerUp ();
									else if (PlayerPrefs.GetInt ("DodgerSlowCount") > 0)
										DodgerPowerUpContoller.SlowDownPowerUp ();
									else if (DogerGameManager.isSliptEnabled) {					
										if (PlayerPrefs.GetInt ("DodgerMergeCount") > 0)
											DodgerPowerUpContoller.MergePowerUp ();
									}
								}
							} else if (randomPowerUp == 3) {
								if (PlayerPrefs.GetInt ("DodgerSlowCount") > 0)
									DodgerPowerUpContoller.SlowDownPowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerBombCount") > 0)
										DodgerPowerUpContoller.BombPowerUp ();
									if (PlayerPrefs.GetInt ("DodgerInvincibleCount") > 0)
										DodgerPowerUpContoller.InvinciblePowerUp ();
									else if (randomPowerUp == 4 && DogerGameManager.isSliptEnabled) {					
										if (PlayerPrefs.GetInt ("DodgerMergeCount") > 0)
											DodgerPowerUpContoller.MergePowerUp ();
									}
								}

							} else if (randomPowerUp == 4 && DogerGameManager.isSliptEnabled) {					
								if (PlayerPrefs.GetInt ("DodgerMergeCount") > 0)
									DodgerPowerUpContoller.MergePowerUp ();
								else {
									if (PlayerPrefs.GetInt ("DodgerBombCount") > 0)
										DodgerPowerUpContoller.BombPowerUp ();
									if (PlayerPrefs.GetInt ("DodgerInvincibleCount") > 0)
										DodgerPowerUpContoller.InvinciblePowerUp ();
									else if (PlayerPrefs.GetInt ("DodgerSlowCount") > 0)
										DodgerPowerUpContoller.SlowDownPowerUp ();

								}
							} else {
								if (PlayerPrefs.GetInt ("DodgerBombCount") > 0)
									DodgerPowerUpContoller.BombPowerUp ();
								if (PlayerPrefs.GetInt ("DodgerInvincibleCount") > 0)
									DodgerPowerUpContoller.InvinciblePowerUp ();
								else if (PlayerPrefs.GetInt ("DodgerSlowCount") > 0)
									DodgerPowerUpContoller.SlowDownPowerUp ();
							}
						}
						TapCount = 0;
					}
				}

			}
			if (Time.time > NewTime) {
				TapCount = 0;
			}
		}


		#if !UNITY_EDITOR

		if (touchCount == 1) 
		{
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) 
			{
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (touch.position), Vector2.zero);
				if (hit.collider != null) 
				{
					if (hit.collider.gameObject.CompareTag ("Player")) 
					{
						GameObject_toDrag = hit.collider.gameObject;
						Z_Dist = hit.transform.position.z - Camera.main.transform.position.z;
						GameObject_WorldPosition = new Vector3 (touch.position.x, touch.position.y, Z_Dist);
						if(ColliderSizeVariable)
							GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1f,1f);
						GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
						offset = GameObject_toDrag.transform.position - GameObject_WorldPosition;
						dragging = true;
					}
				}
			}

			if ( touch.phase == TouchPhase.Moved && dragging) 
			{
				if(GameObject_toDrag)
				{
					GameObject_WorldPosition = new Vector3 (touch.position.x, touch.position.y, Z_Dist);
					GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);

					if(isRotationEnabled)
					{
						RotateObject (GameObject_WorldPosition);
						GameObject_toDrag.transform.position =  Vector3.Lerp (transform.position, GameObject_WorldPosition + offset, LerpSpeed * Time.deltaTime);
					}
					else
					{
						GameObject_toDrag.transform.position =  GameObject_WorldPosition + offset;
					}

					Vector3 restricted_Position = GameObject_toDrag.transform.position;

					restricted_Position.x = Mathf.Clamp(restricted_Position.x, -RestrictedPositionX.position.x, RestrictedPositionX.position.x);
					restricted_Position.y = Mathf.Clamp(restricted_Position.y, -RestrictedPositionY.position.y, RestrictedPositionY.position.y);

					restricted_Position.z = Mathf.Clamp(restricted_Position.z, 0, 0);
					GameObject_toDrag.transform.position = restricted_Position;
				}
				else 
				{
					if (dragging) 
					{
						if(ColliderSizeVariable)
							GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1.6f,1.6f);// size big...
						GameObject_toDrag = null;
						dragging = false;	

					}
				}
			}

			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) 
			{
				if (dragging)
				{
					if(ColliderSizeVariable)
						GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1.6f,1.6f);// size big...
					GameObject_toDrag = null; 
					dragging = false;
				}
				else 
				{
					return;
				}
			}
		} 

		if (touchCount > 1 || touchCount ==2) 
		{
			if (dragging) 
			{
				if(ColliderSizeVariable)
					GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (1.6f, 1.6f);// size big...
				dragging = false;
				GameObject_toDrag = null;		
			}
		}

		#endif

				
	}
	public void RotateObject(Vector3 Pos)
	{		
		Vector3 vectorToTarget = (transform.position -Pos).normalized;
		float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		angle+=90f;
		Quaternion quaternion = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, quaternion, Time.deltaTime*10f);
	}
}