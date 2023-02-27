# Squirrel

## PlayerControl

```c#
public class PlayerControl : MonoBehaviour
{
  // 动画控制器
  private Animator ani;
  // 判断是否在地面
  private bool isGround;
  // 箱子控制器
  private BoxControl box;
  // 箱子的 Transform，player头顶的箱子
  private Transform boxTrans;

  void Start()
  {
    // 获取动画控制组件
    ani = GetComponent<Animator>();
    // 获取 player 的孩子物体 boxTrans
    boxTrans = transform.GetChild(0);
  }

  // Update is called once per frame
  void Update()
  {
    float horizontal = Input.GetAxis("Horizontal");
    // 移动
    if (horizontal != 0) {
      // 开始移动
      ani.SetBool("IsRun", true);
      // 修改 player 左右移动的方向
      transform.localScale = new Vector3(horizontal > 0 ? 2.3f : -2.3f, 2.3f, 2.3f);
      // 移动的速度
      transform.Translate(Vector3.right * Time.deltaTime * horizontal * 4);
    }
    else
    {
      // 站立
      ani.SetBool("IsRun", false);
    }

    // 当在地面上按下 W 时，处理跳跃
    if(Input.GetKeyDown(KeyCode.W) && isGround)
    {
      // 获取刚体，增加向上的力
      GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
    }

    // 按下空格时，捡起箱子或者扔出箱子
    if (Input.GetKeyDown(KeyCode.Space)) {
      // 当 player 下的 boxTrans 物体没有孩子，并且 box 实例存在时
      if (boxTrans.childCount == 0 && box != null)
      {
        // 将 box 的父组件设置为 player 的子组件 boxTrans
        box.transform.SetParent(boxTrans);
        // 位置归0，箱子将到 player 的头顶上
        box.transform.localPosition = Vector2.zero;
        // 设置 player 为 举箱子 动画
        ani.SetFloat("IsBox", 1f);
      }
      else if (boxTrans.childCount > 0) {
        // 调用 箱子控制器 调用 Move 方法，扔出箱子，实际是箱子自己移动
        boxTrans.GetComponentInChildren<BoxControl>().Move(2f);
        // 设置 player 为 正常 动画
        ani.SetFloat("IsBox", 0f);
      }
    }
  }

  // 碰撞
  private void OnCollisionEnter2D(Collision2D collision)
  {
    // 碰撞到地面时
    if (collision.collider.tag == "Ground") {
      // player 正在地面上
      isGround = true;
      // 跳跃动画 设置为 false
      ani.SetBool("IsJump", false);
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    // 离开地面时
    if (collision.collider.tag == "Ground")
    {
      // player 离开了地面
      isGround = false;
      // 跳跃动画 设置为 true
      ani.SetBool("IsJump", true);
    }
  }

  // 触发器
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // 触发进入了 箱子 物体
    if (collision.tag == "Box") {
      // 用 box 保存箱子组件实例
      box = collision.GetComponent<BoxControl>();
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    // 触发离开了 箱子 物体
    if (collision.tag == "Box")
    {
      // 清除 box
      box = null;
    }
  }
}

```

## BoxControl

```c#
public class BoxControl : MonoBehaviour
{
  // 移动速度，初识为 0
  public float Speed = 0;

  void Update()
  {
    // 自动移动
    transform.Translate(Vector2.right * Speed * 2 * Time.deltaTime);
  }

  // 移动方法，设置初识速度
  public void Move(float dir) {
    Speed = dir;
    // 3秒后销毁
    Destroy(gameObject, 3f);
  }
}
```
