using UnityEngine;
using System.Collections;

namespace UFOsouji.GameSystem
{
	public class SoundOperator : MonoBehaviour
	{
		/** 台詞 */
		public GameObject[] sounds;	
		/** プレイ中のBGM */
		public GameObject PlayBGM = null;
		/** ゲームオーバー時のBGM */
		public GameObject DeadBGM = null;
		/** 建物が生えてくるときの音 */
		public GameObject GOGOGO = null;

		/** 加速の説明をする */
		private bool canBoost = false;
		/** ワープゲートの説明をする */
		private bool canGoal = false;
		/** ワープゲートの説明をする2 */
		private bool canGoNext = false;

		/** 初期化処理 */
		void Start ()
		{
			PlayBGM.GetComponent<AudioSource> ().Play ();
			playVoice (1);
			startTime = Time.time;
		}
		
		private float startTime = 0.0f;
		private float waitTime = 0.0f;
		private int index = 1;
		/** 操作説明 */
		private void StartVoice()
		{
			if(index < 8 && !sounds[index].GetComponent<AudioSource>().isPlaying && Time.time - startTime > waitTime)
			{
				index++;
				playVoice(index);
				startTime = Time.time;
			}
			switch(index)
			{
			case 1: waitTime = 12.0f; break;
			case 2: waitTime = 6.0f; break;
			case 3: waitTime = 7.0f; break;
			case 4: waitTime = 6.0f; break;
			case 5: waitTime = 5.0f; break;
			case 6: waitTime = 8.0f; break;
			case 7: waitTime = 5.0f; break;
			case 8: waitTime = 7.0f; break;
			default:
				break;
			}

		}
		
		void Update ()
		{
			StartVoice ();

			if(canBoost)
			{
				if(!sounds[9].GetComponent<AudioSource>().isPlaying)
				{
					canBoost = false;
					playVoice(10);
				}
			}
			
			if(canGoNext)
			{
				if(!sounds[11].GetComponent<AudioSource>().isPlaying)
				{
					canGoNext = false;
					playVoice(12);
				}
				
			}

			if(canGoal)
			{
				if(!sounds[13].GetComponent<AudioSource>().isPlaying)
				{
					canGoal = false;
					playVoice(14);
				}
				
			}
		}
		
		/** 建物が生えてくるときの音再生する */
		public void startGrow()
		{
			GOGOGO.GetComponent<AudioSource> ().Play ();
		}
		/** 建物が生えてくるときの音をとめる */
		public void stopGrow()
		{
			GOGOGO.GetComponent<AudioSource> ().Stop ();
		}

		/** 加速の説明をする */
		public void boost()
		{
			playVoice(9);
			canBoost = true;
		}

		/** ワープゲートの説明をする */
		public void goal()
		{
			playVoice(13);
			canGoal = true;
		}

		/** ワープゲートの説明をする2 */
		public void nextStage()
		{
			playVoice(11);
			canGoNext = true;
		}

		/** ゲームオーバー時の音を鳴らす
		  * 0:燃料切れ 1:時間切れ
		  */
		public void gameOver(int type)
		{
			// 登録外のゲームオーバーが来たら *
			if(type < 0 || 2 < type)
			{
				Debug.Log("そのようなタイプは登録されていません");
			}
			// ゲームオーバー時の音楽を鳴らす *
			PlayBGM.GetComponent<AudioSource> ().Stop ();
			DeadBGM.GetComponent<AudioSource> ().Play ();

			for(int i = 0;i < sounds.Length;i++)
			{
				sounds[i].GetComponent<AudioSource> ().Stop();
			}

			// 燃料切れ *
			if(type == 0)
			{
				playVoice(16);
			}
			// 時間切れ *
			if(type == 1)
			{
				playVoice(17);
			}
		}

		/** 指定した番号のせりふを鳴らす */
		private void playVoice(int index)
		{
			/** 配列の外なら何もしない */
			if(index < 0 || sounds.Length <= index)
			{
				return;
			}
			/** せりふが登録されていないなら何もしない */
			if(sounds[index] == null)
			{
				return;
			}
			/** ゲームオーバーのときは再生しない */
			if(sounds[16].GetComponent<AudioSource> ().isPlaying || sounds[17].GetComponent<AudioSource> ().isPlaying)
			{
				return;
			}
			Debug.Log ("Play!");
			sounds[index].GetComponent<AudioSource> ().Play ();
		}
	}
}