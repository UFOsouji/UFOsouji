using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.S))
		{
			Application.LoadLevel("Play_b");
		}

	//	this.rigidbody.AddForce(this.transform.forward * 0.5f);
	}
	// プレイヤーがどこかに入ったとき, それがエリアならオブジェクトを登録する  
	void OnTriggerEnter(Collider other)
	{	
		Debug.Log ("NextScene");
		Application.LoadLevel("Play_b");
	}
}
