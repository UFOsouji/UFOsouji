using UnityEngine;
using System.Collections;

namespace UFOsouji
{
	
	public class ResultGUI : VRGUI
	{
		private string clearRank = "A";
		
		
//		private float clearTime = 0.0f;
		private int score = 0;
		private int maxCombo = 0;
		private string clearType = "";
		
		public Vector2 basePos;
		
		
		
		public Texture panel;
		
		private GUIStyle resultData;
		private GUIStyle resultRank;
		private GUIStyleState styleColor = new GUIStyleState();
		
		public bool isDebug = false;
		
		void Start ()
		{
			
			styleColor.textColor = Color.white;
			
			resultData = new GUIStyle();
			resultData.fontSize = 18;   // フォントサイズの変更.
			resultData.fontStyle = FontStyle.Italic;
			resultData.normal = styleColor;
			
			resultRank = new GUIStyle();
			resultRank.fontSize = 100;   // フォントサイズの変更.
			resultRank.fontStyle = FontStyle.Bold;
			resultRank.normal = styleColor;
			
//			clearTime = PlayerPrefs.GetFloat ("ClearTime");
			score = PlayerPrefs.GetInt ("Score");
			maxCombo = PlayerPrefs.GetInt ("MaxCombo");
			clearType = PlayerPrefs.GetString ("Result");
			
			// ゲームオーバーならスコアなし 
			setResultData ();
			
			string[] ranks = {"Ｅ","Ｄ","Ｃ","Ｂ","Ａ","Ｓ"};
			
			
			
			//			/** Debug Data
			if (isDebug) {
//				clearTime = 100.0f;
				score = 3000;
				maxCombo = 999;
				clearType = "game_clear";
			}
			//			*/
			
			// クリアランクを設定 
			for(int i = 0;i < ranks.Length;i++)
			{
				if(i*1000 < score && score < (i+1)*1000+1)
				{
					clearRank = ranks[i];
				}
			}
			
		}
		
		// ゲームオーバーかゲームクリアーかによってデータを変更する 
		void setResultData()
		{
			if(clearType == null || clearType.Equals(""))
				return ;
			
			if(!clearType.Equals("game_clear"))
			{
				score = 0;
//				clearTime = 0.0f;
				maxCombo = 0;
			}
		}
		
		public void Update(){
			if(Input.GetKey (KeyCode.Escape)){
				Application.Quit();
			}
		}
		
		public override void OnVRGUI()
		{
			
			GUI.DrawTexture (new Rect(Screen.width/4, Screen.height/4, Screen.width/2 ,Screen.height/2),panel);
			
			GUI.Label (new Rect (Screen.width/2-basePos.x-50, Screen.height/2-basePos.y - 110, 400, 200), ""+score, resultData);
			GUI.Label (new Rect (Screen.width/2-basePos.x-50, Screen.height/2-basePos.y - 70, 400, 200), ""+maxCombo, resultData);
//			GUI.Label (new Rect (Screen.width/2-basePos.x-50, Screen.height/2-basePos.y - 34, 400, 200), ""+(int)((float)clearTime/60.0f) +":"+(int)((float)clearTime%60.0f), resultData);
			
			GUI.Label (new Rect (Screen.width/2-basePos.x+70, Screen.height/2-basePos.y - 95, 400, 200), ""+clearRank, resultRank);
		}
	}
	
}