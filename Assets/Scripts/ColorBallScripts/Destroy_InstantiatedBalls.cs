using UnityEngine;
using System.Collections;

public class Destroy_InstantiatedBalls : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Blue_Draggable" || other.gameObject.tag == "Green_Draggable" || other.gameObject.tag == "White_Draggable" || other.gameObject.tag == "Red_Draggable" || other.gameObject.tag == "Yellow_Draggable" || other.gameObject.tag == "Pink_Draggable" || other.gameObject.tag == "Gray_Draggable"  || other.gameObject.tag == "Parpel_Draggable" || other.gameObject.tag == "Brown_Draggable" || other.gameObject.tag == "SkyBlue_Draggable") 
		{
			RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (other.gameObject);
			Destroy (other.gameObject);
		}
		else if(other.gameObject.layer == 21)
		{
			Destroy (other.transform.parent.gameObject);
			Guns.instaintiatedBulletsOnScreen.Remove (other.transform.parent.gameObject);
		}
	}
}
