using UnityEngine;
using System.Collections;
using UFOsouji.Area;

namespace UFOsouji.Trash
{
    /**
     * ゴミを指定量生成するためのクラス
     * */
    class TrashCreator : MonoBehaviour
    {
        /** ゴミの生成量 */
        public float TrashNum = 0.0f;
        /** ゴミの種類 */
        public GameObject[] prefab = null;

        /** 初期化処理 */
        void Start()
        {
            if (prefab == null || prefab.Length < 0) Debug.Log("プレハブが登録されていません");
            else createTrash(TrashNum);
        }

        /** 指定した数だけゴミを生成する */
        private void createTrash(float num)
        {
            for (int i = 0; i < (int)num; i++)
                loadTrash();

            GameObject.FindGameObjectWithTag("Scripts").GetComponent<City>().putTrash(TrashNum);
            // ゴミを作り終えたら死ぬ *
            Destroy(this.gameObject);
        }
        /** ランダムな位置にゴミを読み込む */
        private GameObject loadTrash()
        {
            GameObject trash = GameObject.Instantiate(prefab[Random.Range(0, prefab.Length)]) as GameObject;
            Vector3 pos = this.gameObject.transform.position;
            Vector3 size = this.transform.localScale;
            float x = Random.Range(pos.x - size.x / 2, pos.x + size.x / 2);
            float z = Random.Range(pos.z - size.z / 2, pos.z + size.z / 2);
            float y = pos.y;
            Vector3 v = new Vector3(x, y, z);
            trash.transform.localPosition = v;
            trash.transform.parent = GameObject.FindGameObjectWithTag("TrashParent").transform;
            return trash;
        }

    }
}