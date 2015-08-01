using UnityEngine;
using System.Collections;


namespace UFOsouji
{
	public class SoundController : MonoBehaviour {
	
		public GameObject sound1;
		public GameObject sound2;
	
		private bool switch1 = true;
//		private bool switch2 = true;

		void Start ()
		{
			sound1 = GameObject.Find("part1 edit_2");
			sound2 = GameObject.Find("nc30000");
		}
	
		void Update ()
		{
			if(switch1)
			{
				switch1 = false;
				sound1.audio.Play();
			}
			if(sound1 != null && !sound1.audio.isPlaying && !sound2.audio.isPlaying)
			{
				sound2.audio.Play();
				Destroy(sound1.gameObject);
			}
		}
	}
}
