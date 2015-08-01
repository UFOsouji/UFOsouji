using UnityEngine;
using System.Collections;
using System.Threading;

namespace UFOsouji.GameSystem
{
	/**
     * コントローラからの入力を受け付けるクラス
     */
	public class WiiInputServer
	{
		/** ---------Singleton----------- */
		private static WiiInputServer instance = null;
		private WiiInputServer()
		{
			thread = new Thread(new ThreadStart(run));
			thread.Start();
		}
		public static WiiInputServer getInstance()
		{
			if (instance == null)
				instance = new WiiInputServer();
			return instance;
		}
		/** ----------------------------- */
		
		/** 受信したデータ */
		private string data = "";
		
		/** 受信したデータを加工して取得[前後,左右] */
		public float[] getInstruction()
		{
			float[] controller = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
			string msg = "";
			lock (data)
			{
				msg = data;
			}
			try
			{
				string[] msgs = msg.Split(',');
				for (int i = 0; i < controller.Length; i++)
				{
					controller[i] = float.Parse(msgs[i]);
				}
			}
			catch (System.Exception e)
			{
				if(e.Message.Equals("a")){}
				return new[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
			}
			return controller;
		}
		
		/** ---------------別のプログラムと通信する処理ここから--------------- */
		private Thread thread;			// データを受信するためのスレッド
		private bool terminate = false;	// 通信を切断するか否か
		private System.Net.Sockets.TcpClient tcp;
		private System.Net.Sockets.NetworkStream ns;
		/** 通信を切断する */
		public void Terminate()
		{
			terminate = true;
		}
		/** 別のプログラムと通信するための処理 */
		private void run()
		{
			//サーバーのホスト名とポート番号
			string host = "localhost";
			int port = 50001;
			
			//TcpClientを作成し、サーバーと接続する
			tcp = new System.Net.Sockets.TcpClient(host, port);
			
			//NetworkStreamを取得する
			ns = tcp.GetStream();
			Debug.Log("接続を開始します.");
			while (!terminate)
			{
				//受信したデータを文字列に変換
				string resMsg = "none";
				//サーバーから送られたデータを受信する
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				byte[] resBytes = new byte[256];
				do
				{
					//データの一部を受信する
					int resSize = ns.Read(resBytes, 0, resBytes.Length);
					//Readが0を返した時はサーバーが切断したと判断
					if (resSize == 0)
					{
						Debug.Log("サーバから切断されました.");
						break;
					}
					//受信したデータを蓄積する
					ms.Write(resBytes, 0, resSize);
				} while (ns.DataAvailable);
				//文字列をByte型配列に変換
				System.Text.Encoding enc = System.Text.Encoding.UTF8;
				resMsg = enc.GetString(ms.ToArray());
				ms.Close();
				lock (data)
				{
					data = resMsg;
				}
				Debug.Log(data);
			}
			//閉じる
			ns.Close();
			tcp.Close();
			Debug.Log("Thread を終了します.");
		}
		/** ---------------別のプログラムと通信する処理ここまで--------------- */
	}
}
