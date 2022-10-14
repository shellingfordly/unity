using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int Hp = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp < 1) { return; };
        transform.Translate(Vector2.left * 4 * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "bullet")
        {
            Hp = 0;
            Debug.Log("bullet");
            // 销毁子弹
            Destroy(collision.gameObject);
            // 销毁自己
            Destroy(this.gameObject, 0.4f);
            // 销毁碰撞器
            Destroy(GetComponent<Collider2D>());
        }
    }
}
