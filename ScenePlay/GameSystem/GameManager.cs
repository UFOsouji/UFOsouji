using UnityEngine;
using System.Collections;
using UFOsouji.Player;
using UFOsouji.GameSystem;

namespace UFOsouji.GameSystem
{
	public class GameManager : MonoBehaviour
	{
		/** 次のシーンの名前 */
		public string nextScene = "Result";
		/** 帰還できるようになる発展度 */
		public int clearRank = 3;
		/** 制限時間 */
		public int timeLimit = 300;
		/** スタート時の時間 */
		private float startTime = 0.0f;
		/** ボードのクラス */
		private Board board;
		
		/** 初期化処理 */
		void Start ()
		{
			startTime = Time.time;
			board = GameObject.FindGameObjectWithTag("Player").GetComponent<Board>();
		}
		
		/** 更新処理 */
		void Update ()
		{
			checkGameOver ();			
		}
		
		/** ゲームの残り時間 */
		public float getRemainingTime()
		{
			float limit = timeLimit - (Time.time - startTime);
			return (limit > 0.0f)?limit:0.0f;
		}
		
		/** ゲームオーバーか否かの判断 */
		private void checkGameOver()
		{
			if(board.isControllable)
			{
				// ゲームの残り時間がなくなったら *
				if(getRemainingTime() <= 0.0f)
				{
					// ボードを操作不可能に *
					board.isControllable = false;
					// 終わりのサウンドを鳴らす *
					GameObject.FindGameObjectWithTag("Player").GetComponent<SoundOperator>().gameOver(1);
				}
				// ボードの残りの燃料がなくなったら *
				if(board.isEmptyOil())
				{
					// ボードを操作不可能に *
					board.isControllable = false;
					// 燃料が無くなったときのサウンドを鳴らす *
					GameObject.FindGameObjectWithTag("Player").GetComponent<SoundOperator>().gameOver(0);
				}
				// それ以外ならまだゲームは終わってない *
			}
		}
		/** ゲームをクリアしたときに呼ぶ処理,これを呼ぶとリザルト画面に飛ぶ */
		public void gameFinishRequest()
		{
			// ゲームクリアまでの時間は加点対象 *
			ScoreManager.getInstance ().gameClear (getRemainingTime());
			int score = (int)ScoreManager.getInstance().getScore();
			int maxCombo = ScoreManager.getInstance().getMaxCombo();
			
			// 次のシーンにデータを送る *
			PlayerPrefs.SetInt ("MaxCombo", maxCombo);
			PlayerPrefs.SetInt ("Score", score);
			
			Application.LoadLevel(nextScene);
		}
	}
}
