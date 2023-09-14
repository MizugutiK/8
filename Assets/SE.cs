using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    public AudioClip snd01;             //鳴らす音データを格納する変数を用意します
    private AudioSource audioS;    //AudioSourceを格納する変数を用意します

    void Start()
    {
        //＊　スタート時に、Scene内にある”SoundSOurce”という名前のオブジェクトを探して、そこのAudioSourceコンポネントを持ってきます
        audioS = GameObject.Find("SE").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other) //　当たった相手（other）のデータを取り込みます
    {
        if (other.gameObject.tag == "Player") //　もし相手(other)のtagが”Bullet”ならば
        {
            audioS.PlayOneShot(snd01); //　PlayOneShot命令で、snd01に格納された音を一回鳴らします
        }
    }
}