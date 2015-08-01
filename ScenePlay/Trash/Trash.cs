using UnityEngine;
using System.Collections;
using UFOsouji.GameSystem;
using UFOsouji.Player;
using UFOsouji.Area;

namespace UFOsouji.Trash
{
	public class Trash : MonoBehaviour
	{
		public bool hasOil = false;
		/** 燃料に変換するときの量 */
		public float oilPoint = 700.0f;
		/** これに触れているなら引き寄せられる */
		private GameObject vacuumerPoint = null;
		/** これに触れると消える */
		private GameObject vacuumer = null;
		/** 初期化処理 */
		void Start()
		{
			vacuumer = GameObject.FindGameObjectWithTag("Vacuumer");
			vacuumerPoint = GameObject.FindGameObjectWithTag("VacuumerPoint");
			if (vacuumer == null || vacuumerPoint == null)
			{
				Debug.Log("Tagが見つかりませんでした.");
			}
		}

		void Update()
		{
			if (Input.GetKey(KeyCode.G))
			{
				this.gameObject.transform.LookAt(vacuumer.transform.position);
				this.rigidbody.AddForce(this.transform.forward * 50.0f);
			}
		}
		
		/** 吸い込まれる～ */
		void OnTriggerStay(Collider other)
		{
			if (other.gameObject.tag.Equals(vacuumerPoint.tag))
			{
				this.gameObject.transform.LookAt(vacuumer.transform.position);
				this.rigidbody.AddForce(this.transform.forward * 50.0f);
			}
		}
		
		/** 何かに触れたときをタグで判定 */
		void OnTriggerEnter(Collider other)
		{
			// 掃除機に吸い込まれたら *
			if (vacuumer != null && other.gameObject.tag.Equals(vacuumer.tag))
			{
				// 得点を追加する *
				ScoreManager.getInstance().pickTrash();
				// 街からゴミを取り除く *
				GameObject.FindGameObjectWithTag("Scripts").GetComponent<City>().pickTrash();
				// 燃料に変換する *
				if(hasOil)
				{
					GameObject.FindGameObjectWithTag("Player").GetComponent<Board>().plusOil(oilPoint);
				}
				// オブジェクトを削除する *
				Destroy(this.gameObject);
			}
			
			// 奈落の底に落ちていったら *
			if (other.gameObject.tag.Equals("TrashParent"))
			{
				Destroy(this.gameObject);
			}
		}
	}
}