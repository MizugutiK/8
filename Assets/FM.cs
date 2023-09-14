using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FM : MonoBehaviour
{ 
    private Vector2 targetPos;           //　Vector2型の変数targetPosを用意します
public float power;              //　float型の変数 powerを用意します
public bool xDir;               //　ｘ方向に限定して動かしたいフラブ　bool型の変数 xDirを用意します
public bool yDir;               //　y方向に限定して動かしたいフラブ　bool型の変数 yDirを用意します
private Vector2 oriPos;         //　始まった時のプレイヤーオブジェクトの位置を格納する変数 oriPosを用意します

private void Start()
{
    if (xDir == true)                //　もし変数xDirにチェックが入れられたら・・
    {
        oriPos.y = gameObject.transform.position.y;  //　はじまりの位置のｙ座標の値を変数oriPosのｙ値に入れます
    }

    if (yDir == true)                //　もし変数yDirにチェックが入れられたら・・
    {
        oriPos.x = gameObject.transform.position.x;  //　はじまりの位置のx座標の値を変数oriPosのx値に入れます
    }
}
    void Update()
    {
        //　マウスカーソルの位置をワールド座標に変換して、変数targetPosに入れます
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (xDir == true)			 //　ｘDirにチェックがある場合は・・
        {
            targetPos.y = oriPos.y; 		 //　targetPosのｙ軸の値に、oriPosのy軸の値を入れます
        }

        if (yDir == true)			 //　ｙDirにチェックがある場合は・・
        {
            targetPos.x = oriPos.x;		 //　targetPosのｙ軸の値に、oriPosのx軸の値を入れます
        }

        transform.position = Vector2.Lerp(transform.position, targetPos, power);

    }
}
