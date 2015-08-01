using UnityEngine;
using System.Collections;
using UFOsouji.Trash;
using UFOsouji.Player;
using UFOsouji.GameSystem;

namespace UFOsouji.Area
{
	public class City : MonoBehaviour
	{
		/** エリア名 */
		public string areaName = "工業区";
		/** エリア全体のゴミの量 */
		private float allTrashNum = 0.0f;
		/** 現在街にあるゴミの量 */
		private float nowTrashNum = 0.0f;
		/** ゴールする場所 */
		public GameObject goal;

		/** 新たにゴミを置く時に現在の量を増やす処理 */
		public void putTrash(float trashNum)
		{
			nowTrashNum += trashNum;
			if(nowTrashNum > allTrashNum)
			{
				allTrashNum += trashNum;
			}
		}

		/** ゴミを拾った時に現在の量から減らす処理 */
		public void pickTrash()
		{
			nowTrashNum-=(nowTrashNum>0)?1:0;
		}
		
		/** 現在のゴミの量を取得する */
		public float getNowTrashNum()
		{
			return nowTrashNum;
		}
		/** ゴミの総量を取得する */
		public float getAllTrashNum()
		{
			return allTrashNum;
		}
		
		/** 街の発展度を取得する */
		public float getRank()
		{
			return (allTrashNum - nowTrashNum) / (allTrashNum/5.0f) + 1.0f;
		}
		
		/** ひとつ前の発展度 */
		private float oldRank = 1.0f;
		/** 次にボードがレベルアップするときは？ */
		private float nextBoardLevelUp = 1.0f;
		/** ゴール表示した */
		private bool isMadeGoal = false;
		/** 更新処理 */
		void Update ()
		{
			// 発展度が上がるたびに,ボードの性能を上げる *
			if((int)oldRank < (int)getRank())
			{
				if(getRank() >= nextBoardLevelUp){
					GameObject.FindGameObjectWithTag("Player").GetComponent<Board>().levelUp();
					nextBoardLevelUp += 2.0f;
				}
				/** ゴールを出現させる */
				if(!isMadeGoal &&  (int)getRank() >= this.GetComponent<GameManager>().clearRank )
				{
					goal.SetActive(true);
					GameObject.FindGameObjectWithTag("Finish").SetActive(true);
					// 次のステージに行くかゴールに行くかで鳴らす音を変える *
					if(GameObject.FindGameObjectWithTag("Scripts").GetComponent<GameManager>().nextScene.Equals("Result"))
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<SoundOperator>().goal();
					}else
					{
						GameObject.FindGameObjectWithTag("Player").GetComponent<SoundOperator>().nextStage();
					}
					isMadeGoal = true;
				}

				GameObject.FindGameObjectWithTag("Scripts").GetComponent<CityController>().checkGrowBuilding(getRank());
			}
			oldRank = getRank();
			// ボードの性能を上げる処理ここまで *
		}
	}
}