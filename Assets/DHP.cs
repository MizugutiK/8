using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DHP : MonoBehaviour
{
    public int points;                                            	//　エネミーのHPを設定します
    GameObject gameCtrl;		 //　gameControlを入れたobjectを入れる変数を用意します
    GM script;			 //　GoalManager型の変数scriptを用意します
    S scriptShooting;		 //　 Shooting型の変数scriptShootingを用意します
    public ParticleSystem explosion;	 //　爆発パーティクルを格納する変数を用意します

    public Slider slider;                           // 　シーン内のスライダーを格納します

    private void Start()
    {
        gameCtrl = GameObject.Find("GO");          // 　GameControllerオブジェクトを探して入れます
        script = gameCtrl.GetComponent<GM>();                  //　 GameController内のGoalManagerメソッドを入れます        　　
        scriptShooting = gameCtrl.GetComponent<S>();    //　 GameController内のShootingメソッドを入れます

        slider.maxValue = points;            //　Sliderの最大値をエネミーの最大HPに合わせます
        slider.value = points;                  // 　Sliderのvalueの値をエネミーのHP（満タン時）にします
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") 	//当たった相手がPlayerのTagを持っていたならば・・
        {
            points -= 1;                    //変数pointsから1を引きます
            explosion.transform.position = other.transform.position;   	 //explosionの場所にplayerの場所を入れます            　　
            explosion.Play();                //explosionエフェクトを再生します

            slider.value = points;		 //SliderのValueに現在の変数points内の値を入れます

        }
        if (points <= 0)
        {

            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "IKA") 	//当たった相手がPlayerのTagを持っていたならば・・
        {
            if (points <= 9)
            {
                points += 1;                    //変数pointsから1を引きます

                slider.value = points;       //SliderのValueに現在の変数points内の値を入れます
            } 
        }
        else return;


      
    }
}
