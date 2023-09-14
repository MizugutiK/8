using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DHP1 : MonoBehaviour
{
    public Text scoreText; //Text用変数
    private int score = 0; //スコア計算用変数
    public int points;                                            	//　エネミーのHPを設定します
    GameObject gameCtrl;		 //　gameControlを入れたobjectを入れる変数を用意します
    GM script;			 //　GoalManager型の変数scriptを用意します
    S scriptShooting;		 //　 Shooting型の変数scriptShootingを用意します
    public ParticleSystem explosion;	 //　爆発パーティクルを格納する変数を用意します

    public Slider slider;                           // 　シーン内のスライダーを格納します
    public GameObject obj;                      //　出したいObjectを変数objに格納します

    //カウントアップ
    private float countup = 0.0f;
    //タイムリミット
    public float timeLimit = 5.0f;
    //時間を表示するText型の変数
    public Text timeText;

    // Update is called once per framepublic static float time;

    private void Start()
    {
        score = 0;
        obj.SetActive(false);                       //Strat時にobjに格納されたものを非表示にします

        gameCtrl = GameObject.Find("GO");          // 　GameControllerオブジェクトを探して入れます
        script = gameCtrl.GetComponent<GM>();                  //　 GameController内のGoalManagerメソッドを入れます        　　
        scriptShooting = gameCtrl.GetComponent<S>();    //　 GameController内のShootingメソッドを入れます

        slider.maxValue = points;            //　Sliderの最大値をエネミーの最大HPに合わせます
        slider.value = points;                  // 　Sliderのvalueの値をエネミーのHP（満タン時）にします
    }

    void Update()
    {
        scoreText.text = "イカ:" + score;
        countup += Time.deltaTime;
        //時間を表示する
        timeText.text = countup.ToString("f1") + "秒";

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
            script.OverFlag();           //　変数scriptのGoalFlag()メソッドに行きます
            scriptShooting.gameOn = false;      //　変数scriptShootingのgameOnフラグをfalseにします
            obj.SetActive(true);

            timeText.text = countup.ToString("0") + "秒";

            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "IKA") 	//当たった相手がPlayerのTagを持っていたならば・・
        {
            score =score+ 1;
            if (points <= 9)
            {
                points += 1;                    //変数pointsから1を引きます  　　
                slider.value = points;       //SliderのValueに現在の変数points内の値を入れます
            }
        }
        else return;


   
        return;
    }
}