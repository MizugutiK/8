using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPM : MonoBehaviour
{
    public GameObject enemy;	 	//　エネミーのオブジェクトを入れる変数を用意します
    private Vector2 enePos;		 //　現在のポジションを格納する変数を用意します
    private Vector3 targetPos;		 //　Playerオブジェクトのポジションを格納する変数を用意します
    private float rad;			 //　向かう方向のラジアンを格納する変数を用意します
    public float speed;		 //　エネミーの移動速度(かける値)を格納する変数を用意します
    public float addSpeed;       //　再セット時に足される速度を入れる変数を用意します

    void Start()
    {
        //　変数targetPosにプレイヤーオブジェクトをの位置データを探して入れます
        targetPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        //　目標となるオブジェクトのｙ座標の差とx座標の差を用いて、その2つの線の作る角度のラジアンを求めます
        rad = Mathf.Atan2(targetPos.y - enemy.transform.position.y, targetPos.x - enemy.transform.position.x);
    }
    private void Update()
    {
        enePos = enemy.transform.position;          //　変数enePosに現在のエネミーの位置を入れます 
        enePos.x += speed * Mathf.Cos(rad);         //　変数enePosのx値に「x軸方向の大きさ×speed」を入れます 
        enePos.y += speed * Mathf.Sin(rad);           //　変数enePosのx値に「y軸方向の大きさ×speed」を入れます 
        enemy.transform.position = enePos;          //　現在の位置に、計算して得られたposition値を入れます

        //　もし、エネミーオブジェクトのｙ座標が-6.0ｆ以下になったら・・
        if (enemy.transform.position.y < -9.0f)
        {
            enemy.gameObject.SetActive(true);   	  //　（消えている場合を想定して）このオブジェクトをScene から消します
            speed += addSpeed; 			 //　エネミーの速度に変数addSpeedの値を足して変数に戻します
            ResetEnemy();			 //　ResetEnemy()メソッドに飛びます
        }
    }
    public void ResetEnemy()
    {
        //　x軸の値を（－５.0～５.0のランダムな値）、 y軸の値を（－1.0～6.0のランダムな値）とする新しいポジションを与えます
        enemy.gameObject.transform.position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-1.0f, 7.0f));
        //　その値を変数setPosに格納します
        enePos = enemy.gameObject.transform.position;
        //同じようにPlayerの位置を格納します
        targetPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        if (targetPos != null)            //　もし、プレイヤーがいたら・・（null　は変数に「何も入ってない」ことを示します）
        {
            rad = Mathf.Atan2(targetPos.y - enemy.transform.position.y, targetPos.x - enemy.transform.position.x);
            enePos = enemy.transform.position;          //　変数enePosに現在のエネミーの位置を入れます 
            enePos.x += speed * Mathf.Cos(rad);         //　変数enePosのx値に「x軸方向の大きさ×speed」を入れます 
            enePos.y += speed * Mathf.Sin(rad);           //　変数enePosのx値に「y軸方向の大きさ×speed」を入れます 
            enemy.transform.position = enePos;          //　現在の位置に、計算して得られたposition値を入れます
        }
        else return;
    }
}