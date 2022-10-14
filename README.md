# unity

## 基础

### 生命周期

- Update 每一帧都调用一次
- LateUpdate Update 执行完后调用，每一帧都调用一次
- FixedUpdate 每隔一段时间执行一次
- Awake 脚本初始化，执行一次
- Start 脚本启动时执行(在 Update 之前、Awake 之后执行)
- OnDestroy 脚本销毁时调用
- OnGUI 绘制游戏界面函数，每一帧执行多次
- OnCollisionEnter 碰撞时执行
- OnCollisionStay 持续碰撞
- OnCollisionExit 结束碰撞
- OnTriggerEnter 触发器执行
- OnTriggerStay 持续触发
- OnTriggerExit 结束触发
- OnMouseDown GUI Element 或 Collider(碰撞器) 上按下时执行
- OnMouseOver GUI 元素或碰撞器上经过时执行
- OnMouseEnter 鼠标进入时执行，只执行一次
- OnMouseExit 鼠标离开目标时执行
- OnMouseUp 鼠标释放时执行
- OnMouseDrag 拖动对象时执行

### GameObject

- 获取当前组件对象 GameObject

```c#
// this可省略
gameObject = this.gameObject
```

- 通过名字获取其他组件对象 GameObject

```c#
GameObject test = GameObject.Find("Test")
```

- 通过标签获取 GameObject

```c#
GameObject test = GameObject.FindWithTag("Enemy")

// 激活状态
test.SetActive(false)
```

### 组件

- Line Renderer 线段渲染器
- Trail Renderer 轨迹渲染器
- Rigidbody 刚体
- Hinge Joint 铰链关节
- Spring Joint 弹簧关节
- Fixed Joint 固定关节

```c#
// 获取transform组件
Debug.Log(transform)

// 获取其他组件
BoxCollider bc = GetComponent<BoxCollider>()

// 获取当前物体的子物体身上的组件
GetComponentChildren<CapsuleCollider>(bc)

// 获取父物体身上的组件
GetComponentParent<BoxCollider>()

// 添加(声音)组件
gameObject.AddComponent<AudioSource>()
```

### 预设体

- 获取预设体

```c#
public class Empty: MonoBehaviour
{
  public GameObject Prefab;
}
```

- 通过预设体实例化游戏物体

```c#
public class Empty: MonoBehaviour
{
  void Start()
  {
    GameObject go = Instantiate(Prefab)
    // 销毁
    Destroy(go)
  }
}
```

### Time

- Time.time 游戏开始到目前的时间
- Time.timeScale 时间缩放值（加速/减速）
- Time.fixedDeltaTime 固定时间间隔
- Time.deltaTime 上一帧带这一帧的游戏时间（1/60，1/120/1/144）

### Application

- Application.dataPath 当前文件路径（只读，将会加密压缩）
- Application.persistentDataPath 持久化文件路径（系统分配路径，游戏写文件时使用）
- Application.streamingAssetsPath 名叫 StreamingAssets 的文件夹路径（只读，配置文件）
- Application.temporaryCachePath 临时文件夹
- Application.runInBackground 是否后台运行
- Application.OpenURL('http://xxx.xxx.com') 打开 url
- Application.Quit() 退出游戏

### SceneManager 场景

- SceneManager

  - LoadScene('MyScene') 跳转场景
  - sceneCount 已加载场景个数
  - CreateScene("newScene") 创建新场景数量
  - UnloadSceneAsync(newScene) 销毁场景

```c#
// 场景叠加，新加载的场景和旧场景合并
SceneManager.LoadScene('MyScene', LoadSceneMode.Additive)
```

- 获取当前场景
  - name 场景名称
  - isLoaded 是否加载
  - path 路径
  - buildIndex 索引

```c#
Scene scene = SceneManager.GetActiveScene();
scene.name
scene.isLoaded
scene.path
scene.buildIndex
// 获取所有根游戏物体对象
GameObject[] gos = scene.GetRootGameObject();
gos.Length
```

### 异步加载场景

- operation.allowSceneActivation 是否自动跳转，默认为 true
- operation.progress 加载进度值，0 - 0.9

```c#
public class Async: MonoBehaviour
{
  AsyncOperation operation;

  void Start()
  {
    // 调用协程
    StartCoroutine(loadScene());
  }

  // 协程方法异步加载场景
  IEnumerator loadScene()
  {
    operation = SceneManager.LoadSceneAsync()
    // 场景加载完不自动挑战，默认为true自动跳转
    operation.allowSceneActivation = false
    yield return operation // 固定返回
  }

  void Update()
  {
    // 加载进度值，0 - 0.9
    operation.progress
  }
}
```

### transform

#### 属性

- position 相对世界的位置信息
- localPosition 相对父物体的位置信息
- rotation 旋转(四元素)
- localRotation
- eulerAngles 旋转(欧拉角)
- localEulerAngles
- localScale 缩放
- 方向向量
  - forward 前方
  - right 右方
  - up 上方

#### 方法

- LookAt(点位) 看向某个位置
- Rotate(旋转轴，速度) 旋转
- RotateAround(围绕参照物，旋转轴，速度) 绕某个物体旋转
- Translate(方向\*速度) 移动

```c#
void Update()
{
  // 物体时刻看向 000 点位置
  transform.LookAt(Vector3.zero)
  // 旋转，每一帧旋转一度
  transform.Rotate(Vector3.up, 1);
  // 绕某个物体旋转
  transform.Rotate(Vector3.zero, Vector3.up, 5)
  // 移动
  transform.Translate(Vector3.forward * 0.1f)
}
```

#### 父子关系

- parent.gameObject 父物体
- childCount 子物体个数
- DetachChildren() 解除子物体的父子关系
- Find('child') 获取某个子物体
- GetChild(0) 获取第一个子物体
- son.IsChildOf(parent) 是否是父子关系
- son.SetParent(parent) 设置父物体

```c#
// 父物体的 GameObject 对象
GameObject parentGameObject = transform.parent.gameObject;
// 子物体数量
transform.childCount;
// 解除
transform.DetachChildren();
Transform trans = transform.Find('child');
trans = transform.GetChild(0);
// 判断 trans 是否是 transform 的子物体
bool trans = trans.IsChildOf(transform)
// 设置 transform 为 trans 的父物体
trans.SetParent(transform)
```

### 监听键鼠

- 0 左键
- 1 右键
- 1 滚轮

```c#
void Update()
{
  if(Input.getMouseButtonDown(0)){
    // 按下了鼠标左键
  }
  if(Input.getMouseButton(0)){
    // 持续（按住）了鼠标左键
  }
  if(Input.getMouseButtonUp(0)){
    // 抬起了鼠标左键
  }
  if(Input.GetKeyDown(KeyCode.A)){
  }
  if(Input.GetKey(KeyCode.A)){
  }
  if(Input.GetKeyUp(KeyCode.A)){
  }
}
```

### 虚拟轴

```c#
void Update()
{
  // 水平轴
  float horizontal = Input.GetAxis("Horizontal");
  // 垂直轴
  float vertical = Input.GetAxis("vertical");
  // 虚拟按键
  Input.GetButtonDown('Jump') // 空格
  Input.GetButton('Jump')
  Input.GetButtonUp('Jump')
}
```

### 触摸事件

- Input.multiTouchEnabled 是否开启多点触摸
- Input.touches[] 触摸对象数组
  - touch.position 触摸位置
  - touch.phase 触摸阶段
    - TouchPhase.Began 开始触摸
    - TouchPhase.Moved 移动
    - TouchPhase.Stationary 静止
    - TouchPhase.Ended 结束
    - TouchPhase.Canceled 打断/取消

```c#
void Start()
{
  // 开启多点触摸
  Input.multiTouchEnabled = true
}
void Update()
{
  // 判断 单点触摸
  if(Input.touchCount == 1){
    // 触摸对象
    Touch touch = Input.touches[0]
    touch.position
    switch(touch.phase)
    {
      case TouchPhase.Began:
        break;
      case TouchPhase.Moved:
        break;
      case TouchPhase.Stationary:
        break;
      case TouchPhase.Ended:
        break;
      case TouchPhase.Canceled:
        break;
    }
  }
}
```

### 声音

- Play 播放
- Stop 停止
- isPlaying 是否正在播放
- Pause 暂停
- UnPause 继续
- PlayOneShot 播放一次

```c#
public class Audio: MonoBehaviour
{
  public AudioClip music;
  public AudioClip se;

  // 播放器组件
  private AudioSource player;

  void Start()
  {
    player = GetComponent<AudioSource>();
    // 设定播放的音频片段
    player.clip = music;
    player.loop = true; // 循环
    player.volume = 0.5f;  // 音量
    player.Play(); // 播放
    player.Stop(); // 停止
    player.isPlaying // 是否正在播放
    player.Pause(); // 暂停
    player.UnPause(); // 继续
    player.PlayOneShot(se) // 播放一次
  }
}
```

### 视频

与音频一样的 api

```c#
using UnityEngine.Video;

public class Audio: MonoBehaviour
{
  private VideoPlayer player;

  void Start()
  {
    player = GetComponent<VideoPlayer>();
  }
}
```

### 角色控制器

```c#
public class Audio: MonoBehaviour
{
  private CharacterController player;

  void Start()
  {
    player = GetComponent<CharacterController>();
  }

  void Update()
  {
    // 水平轴
    float horizontal = Input.GetAxis("Horizontal");
    // 垂直轴
    float vertical = Input.GetAxis("Vertical");
    // 创建一个方向向量
    Vector3 dir = new Vector3(horizontal, 0, vertical)
    // (有重力)向某个方向移动
    player.SimpleMove(dir * 2)
  }
}
```

### 碰撞

```c#
public class Fire: MonoBehaviour
{
  // 创建一个爆炸预设体
  public GameObject Prefab;

  // 碰撞时触发此函数
  // collision 碰撞到的物体
  private void OnCollisionEnter(Collision collision)
  {
    // 创建一个爆炸物体
    Instantiate(Prefab, transform.position, Quaternion.identity)
    // 摧毁自身
    Destroy(gameObject)
  }

}
```

### 触发器

```c#
public class Fire: MonoBehaviour
{
  // other 进入出发的物体
  private void OnTriggerEnter(Collision other)
  {
     GameObject door = GameObject.Find("Door")
     if(door != null)
     {
      door.SetActive(false)
     }
  }
}
```

### 射线

```c#
public class Ray: MonoBehaviour
{
  void Update()
  {
    // 创建射线
    Ray ray = new Ray(Vector3.zero, Vector3.up)
    // 创建射线
    Ray ray = Camera.main.ScreenPointRay(Input.mousePosition);
    // 声明一个碰撞类
    RaycastHit hit;
    // 碰撞检测
    bool res = Physics.Raycast(ray, out hit);
    // 移动物体
    transform.position = hit.point
  }
}
```

### 动画

- Rig
  - 旧版
  - 泛型
  - 人形

### 动力

- IK 是什么

```c#
public class Audio: MonoBehaviour
{
  private Transform target;

  private void OnAnimatorIK(int layerIndex)
  {
    // 设置头部IK 权重
    animator.SetLookAtWeight(1);
    // 看向目标位置
    animator.SetLookAtPosition(target.position)
    // 设置右手IK权重
    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1)
    // 旋转权重
    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1)
    // 设置右手IK
    animator.SetIKPosition(AvatarIKGoal.RightHand,target.position)
    // 设置右手旋转IK
    animator.SetIKRotation(AvatarIKGoal.RightHand,target.rotation)
  }
}
```
