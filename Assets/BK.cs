using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BK : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")　//　もし相手(other)のtagが”Bullet”ならば
        {
            this.gameObject.SetActive(false);     //　このオブジェクトをScene から消します
        }
    }
}

