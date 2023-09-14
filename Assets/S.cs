using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S : MonoBehaviour
{
	public GameObject ballObj;          // 　生成したいPrefab
	private Vector3 clickPos;               // 　マウスのカーソル位置座標
	public float rapid;                                 //　ボールを出せるようになるまでの時間　float型の変数を用意します
	private float oriRapid;                         //　元のrapidに入れられていた値を格納しておく変数　 float型の変数を用意します
	public bool gameOn;                         //   ゲームが進行中か終わってるか、の2択のフラグ
	public float speed;                     //    飛ばす力の大きさの値です 
	public Transform canonPos;  //　 弾が出る場所の座標
	public GameObject playerObj;    //　 Playerのobjectを入れます
	private float degree;       //　回転角度（オイラー角、一般的な普通の角°で表す）
	public float rotSpeed;      //　回頭のスピードを入れる変数　0.8fくらいがよい
	private float oriRotSpeed;  //   もとの回転速度を格納する変数を用意します

	private void Start()
	{
		oriRapid = rapid;               //editorでrapidに入れた値をoriRapidに格納します
		gameOn = true;                  //gameOnをtrueにします
		oriRotSpeed = GetComponent<S>().rotSpeed;   // 開始時にインスペクターに入れたrotSpeed数値を入れます
	}
	void Update()
	{	if (playerObj != null)
				{
					playerObj.SetActive(true);
		if (gameOn == true)
		{
			rapid -= 0.05f;               //oriRapidから0.05を引きます
			if (rapid <= 0)          //もしoriRapidの値が０以下になったら、マウスボタンを押した時に弾が出るようになります
			{
				clickPos = Input.mousePosition;          // Vector3でマウスの位置座標を取得する
				clickPos.z = 10.0f;                                   // そこでに適当な値を入れておきます

				//マウスカーソルの方向に本体を向けるスクリプト
				Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(clickPos);   //　カーソル位置をゲームスクリーンの座標にします
				Vector3 bulletDir = Vector3.Scale(mouseWorldPos - canonPos.position, new Vector3(1, 1, 0)).normalized;
				//　 変数bulletDir　 muzzleとカーソルの位置を、ｘ、ｙ方向ベクトル化して入れます
				degree = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
				//さらに、bulletDir（muzzleとカーソル間の）ベクトルの向かう「角度」を計算します

				if (mouseWorldPos.y >= canonPos.position.y)        // マウスカーソルのｙの座標がcanonPosよりも（砲台の中心位置より）上ならば・・
				{
					rotSpeed = oriRotSpeed;      //　もとのrotSpeed（砲塔回転速度）を入れて戻します
				}
				//プレイヤーオブジェクトのオイラー角（ｘ、ｙ、ｚ）に、現在の角度から向かう場所の角度（degreeの値）まで毎フレームごとに徐々に動かします
				playerObj.transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(playerObj.transform.eulerAngles.z, degree - 90, Time.deltaTime * rotSpeed));

				//プレイヤーオブジェクト（砲塔）傾きから、弾の飛ぶ方向を求めます
				float canonRad = playerObj.transform.eulerAngles.z * Mathf.Deg2Rad;        // canonの傾きからラジアンを求めます
				Vector3 canonAngle = new Vector3(Mathf.Sin(-canonRad), Mathf.Cos(canonRad), 0).normalized;//canonRadの大きさから方向ベクトルを求めます

				float cos = Mathf.Cos(canonRad);    //求められたcanonRadの値から、Sin関数の値を計算します　

				if (cos > 0.5f)             //　もし、変数sinの値が0.2fより大きければ・・
				{
					rotSpeed = oriRotSpeed;         //　変数rotSpeedに、始めの値を入れます
					if (Input.GetMouseButtonDown(0))                // マウスで左クリック("0"が左クリックに初期設定してあります)したら・・
					{
						GameObject ball = Instantiate(ballObj, canonPos.position, ballObj.transform.rotation);
						//　muzzleの位置にボールを生成します
						ball.GetComponent<Rigidbody2D>().AddForce(canonAngle * speed);//　ボールにrigidbody2Dをいれて、canonAngleの方向に力を加えます		
						
						rapid = oriRapid;            //　rapidに元のoriRapidの値を入れて戻します　
					}
				}

				else if (cos >=-0.5f)               //　もし、変数sinの値が0.2f以下ならば・・
				{
					rotSpeed = 0f;              //　砲塔の回転スピードを０にします（回転スピードを０にして、動きを止める）
				}
			
				}
			}
		}
		else return;
	}
}
