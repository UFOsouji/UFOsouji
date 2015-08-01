using UnityEngine;
using System.Collections;
using UFOsouji.Player;
using UFOsouji.Area;

namespace UFOsouji.GameSystem
{
	public class UIMediator : MonoBehaviour
	{
		private Board board = null;
		private City city = null;
		private GameManager gm = null;
		void Start ()
		{
			board = GameObject.FindGameObjectWithTag ("Player").GetComponent<Board> ();
			city = this.GetComponent<City>();
			gm = this.GetComponent<GameManager>();
		}
		
		void Update ()
		{
//			Debug.Log (timeLimit() + "," + rank() + "," + oilMax() + "," + nowOil() + "," + cityName() + "," + trashMax() + "," + nowTrash());
		}
		
		/** ゴミの最大量 */
		public float trashMax()
		{
			return city.getAllTrashNum();
		}
		/** 現在のゴミの量 */
		public float nowTrash()
		{
			return city.getNowTrashNum();
		}
		/** 残り時間 */
		public float timeLimit()
		{
			return gm.getRemainingTime();
		}
		/** 最大の時間 */
		public float maxTime()
		{
			return gm.timeLimit;
		}
		/** 現在の街の発展度 */
		public int rank()
		{
			return (int)city.getRank();
		}
		/** 燃料の最大量 */
		public float oilMax()
		{
			return board.OIL_MAX;
		}
		/** 残りの燃料 */
		public float nowOil()
		{
			return board.getOil();
		}
		/** 街の名前 */
		public string cityName()
		{
			return city.areaName;
		}
	}
}
