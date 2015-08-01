using UnityEngine;
using System.Collections;

namespace UFOsouji.Title
{
	public class TitleScript : MonoBehaviour
	{
		public void Update()
		{
			if(Input.GetKey(KeyCode.A))
			{
				Application.LoadLevel("Introduction");
			}
		}
	}
}
