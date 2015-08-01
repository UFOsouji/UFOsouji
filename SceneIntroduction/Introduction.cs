using UnityEngine;
using System.Collections;

namespace UFOsouji.Introduction
{
	public class Introduction : MonoBehaviour
	{
		
		void Start () 
		{
			
			// 最初は成績なし *
			PlayerPrefs.SetInt("MaxCombo",0);
			PlayerPrefs.SetInt ("Score", 0);
		}
		
		public float boardSpeed = 2.4f;
		
		void Update ()
		{
			this.rigidbody.AddForce(this.transform.forward * boardSpeed);
		}
	}

}