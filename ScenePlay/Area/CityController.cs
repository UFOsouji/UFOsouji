using UnityEngine;
using System.Collections;
namespace UFOsouji.Area
{
	public class CityController : MonoBehaviour
	{
		/** 成長するタイプの建物 */
		private GameObject[] buildings;

		public void Start()
		{
			buildings = GameObject.FindGameObjectsWithTag ("InteractiveAreaObject");
		}

		/** 建物の成長を監視する */
		public void checkGrowBuilding(float rank)
		{
			for(int i = 0;i < buildings.Length;i++)
			{
				if(buildings[i] == null)
					continue;
				if(buildings[i].GetComponent<InteractiveAreaObject>().isGrowed())
					continue;
				if(buildings[i].GetComponent<InteractiveAreaObject>().growRank == rank)
				{
					buildings[i].GetComponent<InteractiveAreaObject>().growObject();
				}
			}
		}
	}
}
