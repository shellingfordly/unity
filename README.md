# unity

## 基础

### 生命周期

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
