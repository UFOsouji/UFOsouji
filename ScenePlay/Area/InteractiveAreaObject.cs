using UnityEngine;
using System.Collections;
using UFOsouji.GameSystem;

namespace UFOsouji.Area
{
	public class InteractiveAreaObject : MonoBehaviour
	{
		
		/** 成長するタイミング */
		public float growRank = 1.0f;
		/** 成長する時間 */
		public float growTime = 10.0f;
		/** 成長後のY座標 */
		public float growedPosY = 0.0f;
		/** 成長している最中か否か */
		private bool isGrowing = false;
		/** 成長し終わったものか否か */
		private bool growed = false;
		/** 成長し終わったならtrue */
		public bool isGrowed(){
			return growed;
		}
		/** 成長を開始したときの時間 */
		private float oldTime;
		
		/** 成長時につけるパーティクル */
		public GameObject ps;

		/** 初期化処理 */
		void Start()
		{
			if(!(0<growRank && growRank <= 5))
			{
				growRank = 1;
			}
			defaultPos = transform.position;
			// パーティクルや音を停止しておく *
			if(ps != null)ps.GetComponent<ParticleSystem> ().Stop ();
		}
		
		
		
		/** 更新処理 */
		void Update()
		{
			if(isGrowing)
			{
				upliftObject();
			}
		}
		
		/** オブジェクトを変化させる.外からはこのメソッドを呼び出す */
		public void growObject()
		{
			oldTime = Time.time;
			isGrowing = true;
			if(ps != null)ps.GetComponent<ParticleSystem> ().Play ();
			GameObject.FindGameObjectWithTag ("Player").GetComponent<SoundOperator> ().startGrow ();
		}
		
		/** オブジェクトを生やす */
		private Vector3 defaultPos;
		private void upliftObject()
		{
			float deltaTime = Time.time - oldTime;
			transform.position = new Vector3 (defaultPos.x, Mathf.Lerp(defaultPos.y,growedPosY,deltaTime/growTime), defaultPos.z);
			if(growTime < deltaTime)
			{
				isGrowing = false;
				growed = true;
				if(ps != null)ps.GetComponent<ParticleSystem> ().Stop ();
				GameObject.FindGameObjectWithTag ("Player").GetComponent<SoundOperator> ().stopGrow ();
			}
		}
	}

}