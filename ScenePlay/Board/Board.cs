using UnityEngine;
using System.Collections;
using UFOsouji.GameSystem;

namespace UFOsouji.Player
{
	/**
      * ボードの情報を持つスクリプト
      * ・燃料
      * ・性能
      * の情報を持つ
      */
	public class Board : MonoBehaviour
	{
		/** ボードの性能レベル(最大3) */
		private int level = 1;
		
		/** 最大レベル */
		private const int MAX_LEVEL = 3;
		
		/** 加速が使えるようになるレベル */
		private const int BOOST_IMPLEMENT_LEVEL = 3;
		
		/** 燃料の変換効率を改善するレベル */
		private const int OIL_EFFICIENT_UP_LEVEL = 3;
		
		/** 燃料の変換効率(通常は1.0f) */
		public float oilEfficientRate = 1.2f;
		
		/** レベルを上げる処理 */
		public void levelUp()
		{
			if (level < MAX_LEVEL)
			{
				if(level == BOOST_IMPLEMENT_LEVEL-1)
				{
					GameObject.FindGameObjectWithTag ("Player").GetComponent<SoundOperator> ().boost ();
				}
				level++;
			}
		}
		
		/** ブーストが使用可能か */
		public bool canUseBoost()
		{
			return (level >= BOOST_IMPLEMENT_LEVEL);
		}
		
		/** 操作可能か否か */
		public bool isControllable = true;
		
		/** ------ボードの燃料に関する処理ここから------ */
		/** 燃料の最大値 */
		public float OIL_MAX = 10000;
		/** 現在の燃料 */
		private float oil;
		/** 残りの燃料を取得する */
		public float getOil()
		{
			return oil;
		}
		/** 燃料が０以下ならtrue */
		public bool isEmptyOil()
		{
			return (oil <= 0.0f);
		}
		/** 燃料を使用する(0未満にはならない) */
		public void useOil(float x)
		{
			oil = (0.0f <= oil - x) ? oil - x : 0.0f;
		}
		/** 燃料を継ぎ足す(燃料の最大値は超えない) */
		public void plusOil(float x)
		{
			// 燃料増加量を増加する *
			if (level >= OIL_EFFICIENT_UP_LEVEL)
			{
				x *= oilEfficientRate;
			}
			oil = (oil + x <= OIL_MAX) ? (oil + x) : OIL_MAX;
		}
		/** ------ボードの燃料に関する処理ここまで------ */
		
		/** 初期化処理 */
		void Start()
		{
			oil = OIL_MAX;
		}
		
		// ワープゲートに入ったなら,次のステージへ行くかゲームクリアかをする*
		void OnCollisionEnter(Collision other)
		{
			if(other.gameObject.tag.Equals("Finish"))
			{
				GameManager gm = null;
				gm = GameObject.FindGameObjectWithTag("Scripts").GetComponent<GameManager>();
				if(gm != null)
				{
					gm.gameFinishRequest();
				}
			}
		}
	}
}
