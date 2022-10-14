using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 血量
    public int Hp = 1;
    // 动画控制器
    private Animator ani;
    // 刚体
    private Rigidbody2D rbody;
    // 输出轴
    private int Horizontal;
    private int Vertical;
    private SpriteRenderer sr;
    private bool isGround = false;
    // 计时器
    private float timer = 0;
    // 子弹预设体
    public GameObject BulletPre;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp <= 0)
        {
            return;
        }
        // 水平轴
        if (Input.GetKey(KeyCode.D))
        {
            Horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Horizontal = -1;
        }
        else
        {
            Horizontal = 0;
        }
        // 垂直轴
        if (Input.GetKey(KeyCode.W))
        {
            Vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vertical = -1;
        }else
        {
            Vertical = 0;
        }
        // 通过水平轴和垂直轴控制人物移动
        // 同步动画控制器中的垂直轴
        ani.SetFloat("Vertical", Vertical);
        // 移动
        if (Horizontal != 0) {
            ani.SetBool("IsRun", true);
            transform.Translate(Vector2.right * 5f * Time.deltaTime * Horizontal);
            sr.flipX = Horizontal > 0 ? false : true;
        }else {
            ani.SetBool("IsRun", false);
        }
        // 跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGround==true) {
            ani.SetBool("IsJump", true);
            rbody.AddForce(Vector2.up * 300);
        }

        timer += Time.deltaTime;
        // 发射子弹
        if (Input.GetKey(KeyCode.U)) {
            if(timer > 0.2f) {
                timer = 0;
                Shoot();
            }
        }
    }

    void Shoot() {
        // 声音
        AudioManager.Instance.PlaySound("gun_m");
        // 角度
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        // 位置
        Vector3 position = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.8f, transform.position.z);

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            if (Horizontal > 0 && Vertical > 0)
            {
                rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (Horizontal > 0 && Vertical < 0)
            {
                rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (Vertical > 0)
            {
                rotation = Quaternion.Euler(0, 0, 90);
            }
        }
        else {
            position.x = position.x - 1.8f;
            if (Horizontal < 0 && Vertical > 0)
            {
                rotation = Quaternion.Euler(0, 0, 135);
            }
            else if (Horizontal < 0 && Vertical < 0)
            {
                rotation = Quaternion.Euler(0, 0, -135);
            }
            else if (Vertical > 0)
            {
                rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (Horizontal < 0) {
                rotation = Quaternion.Euler(0, 0,180);
            }
        }
        
        GameObject.Instantiate(BulletPre, position, rotation);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") {
            isGround = true;
            ani.SetBool("IsJump", false);
            AudioManager.Instance.PlaySound("Jump");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = false;
            ani.SetBool("IsJump", true);
        }
    }
}
