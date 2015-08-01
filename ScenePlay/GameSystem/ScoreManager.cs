using UnityEngine;
using System.Collections;

namespace UFOsouji.GameSystem
{
	/**
     * 得点計算等の処理
     * */
	public class ScoreManager
	{
		/** ---------Singleton----------- */
		private static ScoreManager instance = null;
		private ScoreManager() {
			
			int score_ = PlayerPrefs.GetInt ("Score");
			int maxCombo_ = PlayerPrefs.GetInt ("MaxCombo");
			if(score_ != 0){
				score = score_;
			}
			if(maxCombo_ != 0){
				maxCombo = maxCombo_;
			}
		}
		public static ScoreManager getInstance()
		{
			if (instance == null)
				instance = new ScoreManager();
			return instance;
		}
		/** ----------------------------- */
		
		/** 得点 */
		private float score = 0.0f;
		/** 得点を取得する */
		public float getScore()
		{
			return score;
		}
		/** 街の発展度 */
		private int rank = 0;
		/** 発展度を設定する */
		public void setRank(int r)
		{
			rank = r;
		}
		
		/** ------------ゴミを拾った時の点数加算処理ここから------------ */
		/** 最後にゴミを取得したときの時間 */
		private float getScoreTime = 0.0f;
		/** 現在のコンボ数 */
		private int combo = -1;
		/** 最大コンボ数 */
		private int maxCombo = 0;
		/** 最大コンボ数を取得する */
		public int getMaxCombo()
		{
			return maxCombo;
		}
		/** ゴミを拾ったときの処理 */
		public void pickTrash()
		{
			float addPoint = rank * 10;
			if (combo == -1 || Time.time - getScoreTime < 5.0f)
			{
				combo++;
				getScoreTime = Time.time;
				if (maxCombo < combo)
				{// 最大コンボ数を更新する
					maxCombo = combo;
				}
			}
			else
			{
				combo = 0;
				getScoreTime = 0.0f;
			}
			
			score += addPoint + (float)combo * rank;
		}
		/** ------------ゴミを拾った時の点数加算処理ここまで------------ */
		
		
		
		/** ------------ゲームクリア時の得点計算処理ここから------------ */
		public void gameClear(float time)
		{
			float timeScore = 0.0f;
			timeScore = time * 10.0f;
			score += timeScore;
		}
		/** ------------ゲームクリア時の得点計算処理ここまで------------ */
		
	}
}
