using UnityEngine;
using System.Collections;
using UFOsouji.GameSystem;


namespace UFOsouji.Player
{
	/** ボードを操作する為のクラス */
	public class BoardController : MonoBehaviour
	{
		
		/** 前に進んでいるならtrue(こちらのほうが力は強い) */
		private bool isGoForward = false;
		/** 後ろに進んでいるならtrue */
		private bool isGoBackward = false;
		/** 上昇中ならtrue */
		private bool isGoUp = false;
		/** 下降中ならtrue */
		private bool isGoDown = false;
		/** 加速中ならtrue */
		private bool isBoost = false;
		
		/** ボードの向きを計算する際に必要 */
		private const float TARGET_RANGE = 3.0f;
		private float thetaXZ = 0.0f;
		private Vector3 targetPos = Vector3.zero;
		
		/** 初期化処理 */
		void Start()
		{
			// ボードの最初の向きを決定する *
			thetaXZ = this.transform.rotation.eulerAngles.y;
		}
		
		/** 更新処理 */
		void Update()
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
			if (Input.GetKey(KeyCode.O))
			{
				isDebug = true;
			}
			if (Input.GetKey(KeyCode.P))
			{
				isDebug = false;
			}
			getInput();
			move();
		}
		
		/** 移動処理 */
		private void move()
		{
			if (isGoForward)
			{
				this.rigidbody.AddForce(this.transform.forward * 5.0f * speedFix * ((isBoost) ? 2.0f : 1.0f));
				this.GetComponent<Board>().useOil(((isBoost) ? 2.0f : 1.0f));
			}
			if (isGoBackward)
			{
				this.rigidbody.AddForce(this.transform.forward * -2.0f * speedFix);
				this.GetComponent<Board>().useOil(1.0f);
			}
			
			if (isGoUp)
			{
				this.rigidbody.AddForce(this.transform.up * 2.0f * speedFix);
				this.GetComponent<Board>().useOil(1.0f);
			}
			if (isGoDown)
			{
				this.rigidbody.AddForce(this.transform.up * -2.0f * speedFix);
				this.GetComponent<Board>().useOil(1.0f);
			}
			float x = this.transform.position.x + TARGET_RANGE * Mathf.Sin(thetaXZ / 180.0f * Mathf.PI);
			float z = this.transform.position.z + TARGET_RANGE * Mathf.Cos(thetaXZ / 180.0f * Mathf.PI);
			
			targetPos.Set(x, this.transform.position.y, z);
			this.transform.LookAt(targetPos);
		}
		
		/** デバッグモードならtrue */
		public bool isDebug = false;
		/** スピードの微調整 */
		public float speedFix = 1.0f;
		/** ヌンチャクの入力判定境界線 */
		public float nunchukOffsetX = 0.1f;
		public float nunchukOffsetY = 0.15f;
		/** 回転速度 */
		public float rotateSpeedY = 0.5f;
		public float rotateSpeedX = 0.5f;
		/** バランスボードの入力判定境界線 */
		public float balanceBoardOffset = 0.15f;
		
		/** コントローラからの入力 */
		private float[] wiiController = new float[6];
		
		/** ヌンチャクの初期状態 */
		public float[] firstNunchukPos = new float[2];
		
		/** バランスボードの初期状態 */
		public float firstWiiboardPos;
		
		/* 入力処理を行う */
		private void getInput()
		{
			// ゲームオーバーの時は操作を禁止する *
			if (!this.GetComponent<Board>().isControllable)
			{
				isGoForward = false;
				isGoBackward = false;
				isGoUp = false;
				isGoDown = false;
				return;
			}
			
			wiiController = WiiInputServer.getInstance().getInstruction();
			
			if (isDebug)
			{
				if (Input.GetKey(KeyCode.D))
				{
					thetaXZ += rotateSpeedY * ((isBoost) ? 2.0f : 1.0f);
				}
				if (Input.GetKey(KeyCode.A))
				{
					thetaXZ -= rotateSpeedY * ((isBoost) ? 2.0f : 1.0f);
				}
				isGoUp = Input.GetKey(KeyCode.Space);
				isGoDown = Input.GetKey(KeyCode.LeftShift);
				
				isGoForward = Input.GetKey(KeyCode.W);
				isGoBackward = Input.GetKey(KeyCode.S);
				
				// 加速できるならボタンを有効にする *
				if(this.GetComponent<Board>().canUseBoost())
				{
					isBoost = Input.GetKey(KeyCode.C);
				}
			}
			else
			{
				if (wiiController[2] > firstNunchukPos[1] + nunchukOffsetX)
				{
					thetaXZ += rotateSpeedY * ((isBoost) ? 2.0f : 1.0f);
				}
				if (wiiController[2] < firstNunchukPos[1] - nunchukOffsetX)
				{
					thetaXZ -= rotateSpeedY * ((isBoost) ? 2.0f : 1.0f);
				}
				
				isGoUp = (wiiController[3] < firstNunchukPos[0] - nunchukOffsetX);
				isGoDown = (wiiController[3] > firstNunchukPos[0] + nunchukOffsetX);
				
				isGoForward = (wiiController[0] > firstWiiboardPos + balanceBoardOffset);
				isGoBackward = (wiiController[0] < firstWiiboardPos - balanceBoardOffset);
				
				// 加速できるならボタンを有効にする *
				if(this.GetComponent<Board>().canUseBoost())
				{
					isBoost = (wiiController[4] == 1.0f);
				}
			}
			
			// (wiiController[4]==1.0f) … cボタンが押されている *
			// (wiiController[5]==1.0f) … zボタンが押されている *
			
		}
	}
}