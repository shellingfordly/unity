# Contra Game

## PlayerManager

- SpriteRenderer 组件用于渲染**精灵**并控制其在 2D 和 3D 项目场景中的可视化效果

通过 Horizontal 控制水平轴的方向（左右），通过 Vertical 控制垂直轴的方向（上下）。

同时，将垂直轴的值 Vertical 同步到动画的 Vertical 变量，表现为控制人物跑动和持枪的动作。根据 Vertical 的值来改变人物是向上，向下打枪。

```c#
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
  // 精灵渲染器
  private SpriteRenderer sr;
  // 是否贴地
  private bool isGround = false;
  // 计时器
  private float timer = 0;
  // 子弹预设体
  public GameObject BulletPre;

  // Start is called before the first frame update
  void Start()
  {
    // 获取动画组件
    ani = GetComponent<Animator>();
    // 获取刚体组件
    rbody = GetComponent<Rigidbody2D>();
    // 获取精灵渲染器
    sr = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    // 如果血量少于0，不在更新
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
      // 播放跑步动画
      ani.SetBool("IsRun", true);
      // 向右移动
      transform.Translate(Vector2.right * 5f * Time.deltaTime * Horizontal);
      // 左右翻转
      sr.flipX = Horizontal > 0 ? false : true;
    }else {
      ani.SetBool("IsRun", false);
    }
    // 跳跃
    if (Input.GetKeyDown(KeyCode.Space) && isGround==true) {
      // 跳跃
      ani.SetBool("IsJump", true);
      // 向上
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

    // 生成子弹实例
    GameObject.Instantiate(BulletPre, position, rotation);
  }


  // 碰撞
  private void OnCollisionEnter2D(Collision2D collision)
  {
    // 如果碰到地面
    if (collision.collider.tag == "Ground") {
        isGround = true;
        ani.SetBool("IsJump", false);
        AudioManager.Instance.PlaySound("Jump");
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    // 如果 离开 地面
    if (collision.collider.tag == "Ground")
    {
      isGround = false;
      // 启动跳跃动画
      ani.SetBool("IsJump", true);
    }
  }
}
```

## BulletControl

```c#
public class BulletControl : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    // 过两秒自动销毁
    Destroy(gameObject, 2f);
  }

  // Update is called once per frame
  void Update()
  {
    // 自动向前移动
    transform.Translate(Vector2.right * 12 * Time.deltaTime);
  }
}
```

## EnemyControl

```c#
public class EnemyControl : MonoBehaviour
{
  public int Hp = 1;

  void Update()
  {
    if (Hp < 1) { return; };
    // 自动向左移动
    transform.Translate(Vector2.left * 4 * Time.deltaTime);
  }

  // 触发器2d检测：进入触发器
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // 触发了 子弹触发器
    if (collision.gameObject.tag == "Bullet")
    {
      Hp = 0;
      // 销毁子弹
      Destroy(collision.gameObject);
      // 播放死亡动画
      GetComponent<Animator>().SetBool("IsDie", true);
      // 销毁碰撞器
      Destroy(GetComponent<Collider2D>());
      // 销毁刚体
      Destroy(GetComponent<Rigidbody2D>());
      // 销毁自己
      Destroy(this.gameObject, 0.4f);
    }
  }
}
```
