using UnityEngine;
using System.Collections;
using UFOsouji.GameSystem;

namespace UFOsouji.UI
{
	public class UIController : MonoBehaviour
	{
		/** 燃料バー */
		private GameObject oil = null;
		/** 残り時間 */
		private GameObject time = null;
		/** 発展度 */
		private GameObject rank = null;
		/** 情報を持つ */
		private UIMediator info = null;
		/** ランクの画像たち */
		public Texture[] ranks;

		/** バーの長さ */
		private float barLengthX;
		/**  燃料バーの位置修正 */
		private float offsetXOilBar = 0.0f;
		
		/**  残り時間バーの位置修正 */
		private float offsetXTimeBar = 0.0f;
		

		void Start ()
		{
			oil = GameObject.Find("OilBar");
			time = GameObject.Find("TimeBar");
			rank = GameObject.Find("Rank");
			// バーの長さ *
			barLengthX = oil.transform.localScale.x;

			// 燃料バーの処理 *
			offsetXOilBar = oil.transform.position.x;
			// 残り時間バーの処理 *
			offsetXTimeBar = time.transform.position.x;

			info = GameObject.FindGameObjectWithTag ("Scripts").GetComponent<UIMediator> ();

			if(ranks.Length != 5)
			{
				Debug.LogError("Ranksがちゃんと設定されて無いお");
			}
		}
		
		void Update ()
		{
			// ランク画像更新の処理 *
			rank.renderer.material.mainTexture = ranks [info.rank ()-1];

			// 燃料を表す処理 *
			updateBarLength (oil, offsetXOilBar, info.oilMax (), info.nowOil ());
			// 残り時間を表す処理 *
			updateBarLength (time, offsetXTimeBar, info.maxTime (), info.timeLimit ());

			
		}

		/** バーを変化させる処理 */
		private void updateBarLength(GameObject bar, float offsetXBar, float max, float now)
		{
			// 燃料バーを減らす処理 *
			float newBarLength = now / max * barLengthX;
			if(newBarLength != bar.transform.localScale.x)
			{
				if(bar.transform.localScale.x < newBarLength)
				{
					offsetXBar = (barLengthX - newBarLength) / 0.2f;
				}else if(bar.transform.localScale.x > newBarLength)
				{
					offsetXBar = (barLengthX - newBarLength) / -0.2f;
				}
				bar.transform.localScale = new Vector3(
					newBarLength,
					bar.transform.localScale.y,
					bar.transform.localScale.z
					);
				bar.transform.localPosition = new Vector3(
					offsetXBar,
					0.01f,
					0.0f
					);
			}
		}
	}

}