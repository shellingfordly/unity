## API

### GameObject

#### 成员变量

#### 成员方法

- go.AddComponent<T>() 添加脚本
- go.CompareTag(tag) 是否为 tag 标签
- go.SetActive(bool) 激活状态
- go.SendMessage(Type, arg) 让 go 执行对应的函数
- go.BroadcastMessage(Type, arg) 自己和子对象上的函数
- go.SendMessageUpwards(Type, arg) 自己和父对象上的函数

```c#
var go = new GameObject();
go.AddComponent<Script>();
go.CompareTag("Player");
go.SetActive(true);
go.SendMessage("Func1", 100);
```

### Time

- timeScale 时间暂停
  - 0 停止
  - 1 正常
  - 2 倍数
- 帧间隔时间
  - deltaTime 游戏暂停时停止，本质受 scale 数值的影响；
  - unScaledDeltaTime 游戏暂停不影响，不受 scale 影响；
- 计时
  - time 游戏开始到目前的时间(单机使用)
  - unscaledTime 不受 scale 影响
- 物理帧间隔时间
  - fixedDeltaTime
  - fixedUnscaledDeltaTime
- 帧数
  - frameCount

### Transform

#### Vector3

1. 常用向量

- zero
- left
- right
- forward
- back
- up
- down

2. Distance

计算两点之间的距离

3. 位置

- transform.position 相对世界坐标系的位置
- localPosition 相对父级的位置

- 修改位置

不能只赋值单个 x,y,z 必须给一个点位

```c#
this.transform.position = new Vector3(0,1,0);
Vector3 pos = this.transform.localPosition;
pos.x = 10;
this.transform.localPosition = pos;
```

- 朝向

当物体发生旋转之后，此物体的坐标系发生变化，通过此物体的 forward，up，right 向量来获取物体的方向

```c#
this.transform.forward
this.transform.up
this.transform.right
```

#### 位移

- 路程 = 方向 \* 速度 \* 时间

```c#
// 在Update中修改当前位置
// 朝向自己的右向量每一帧以1单位的位移
transform.position += transform.right * 1 * Time.deltaTime;
```

- Translate(位移量, 相对坐标系)
  - 位移量 = 方向 \* 速度 \* 时间
  - 默认相对自己的坐标系

Vector3.forward 世界坐标系的面朝向

```c#
// 相对于世界坐标系移动
transform.Translate(Vector3.forward * 1 * Time.deltaTime, Space.World);

// 相对于自己的坐标系移动
// this.transform.forward 使用自己身上的面朝向向量
transform.Translate(this.transform.forward * 1 * Time.deltaTime, Space.World);
// Space.Self指定相对自己的坐标系
transform.Translate(Vector3.forward * 1 * Time.deltaTime, Space.Self);
```

#### 角度和旋转

1. 欧拉角

- eulerAngles 相对世界坐标系
- localEulerAngles 相对父对象坐标系

2. 旋转

Rotate(旋转角度, 相对坐标系)
Rotate(轴，旋转角度, 相对坐标系)

```c#
// 默认相对自己的坐标系
transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
// 相对世界坐标系
transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime, Space.World);
// 相对于某个设定轴旋转
transform.Rotate(Vector3.right, 10 * Time.deltaTime);
// 相对于世界坐标系的Vector3.right轴旋转
transform.Rotate(Vector3.right, 10 * Time.deltaTime, Space.World);
```

RotateAround(中心点, 轴, 角度)

```c#
transform.RotateAround(Vector3.zero, Vector3.right, 10 * Time.deltaTime);
```

#### 缩放和看向

1. 缩放

- lossyScale 相对于世界的缩放(只读)
- localScale 相对于父对象缩放

```c#
transform.localScale += Vector3.one * Time.deltaTime;
```

2. 看向

一直看向某个点位或者某个对象

LookAt(Vector3)
LookAt(GameObject)

```c#
transform.LookAt(Vector3.zero);
transform.LookAt(go);
```

#### 父子关系

1. 父对象

- transform.parent 父对象

```c#
transform.parent = null;
transform.parent = otherTransform;
transform.SetParent(null);
transform.SetParent(GameObject.Find("otherParent"));
```

- transform.SetParent(go, bool)

  - go 父对象
  - bool 是否保留世界坐标系的(位置/角度/缩放)信息

- transform.DetachChildren()

和第一层的子对象断绝关系

2. 查找子对象

- Find
  - 只在子对象中找
  - 能找到失活对象

```c#
Transform child = transform.Find(ChildName);
```

- transform.childCount 子对象数量
- transform.GetChild(index) 获取子对象
- transform.IsChildOf(Parent) 判断父对象
- transform.GetSiblingIndex() 获取自己作为子对象的 index
- transform.SetAsFirstSibling() 设置为第一个子对象
- transform.SetAsLastSibling() 设置为最后一个子对象
- transform.SetSiblingIndex(index) 设置子对象到index位置
