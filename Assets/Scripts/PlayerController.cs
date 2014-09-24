using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpStrenght = 8;
	public GUIText CountText;
	public GUIText WinText;
	private int count;
	public int isFalling;

	void Start(){
		count = 0;
		setCountTekst();
		WinText.text = "";
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);

		rigidbody.AddForce(movement * speed * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.Space) && isFalling == 0)
		{
			rigidbody.velocity = Vector3.up * jumpStrenght;
			/*Vector3 tempJumpMovement = new Vector3(0,jumpStrenght,0);
			rigidbody.AddForce(tempJumpMovement);/**/
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Ground")
			isFalling = 0;

		if (other.gameObject.tag == "Pick Up")
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			setCountTekst();
		}

		if(other.gameObject.tag == "South Wall")
			other.gameObject.renderer.enabled = false;
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Ground")
			isFalling = 1;
		if(other.gameObject.tag == "South Wall")
			other.gameObject.renderer.enabled = true;
	}

	void setCountTekst(){
		CountText.text = "Count: " + count.ToString();
		if(count >= 10)
			WinText.text = "You Win!";
	}
}
