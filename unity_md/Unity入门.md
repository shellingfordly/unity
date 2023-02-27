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
- transform.SetSiblingIndex(index) 设置子对象到 index 位置

#### 坐标转换

1. 世界坐标系转换为本地坐标系

世界坐标系原点的**点的位置信息(x,y,z)**转换为相对本地坐标系原点的**点的位置信息**

```c#
// 受物体缩放影响
// 将点Vector3.forward的位置转换为相对于transform物体的位置信息
transform.InverseTransformPoint(Vector3.forward)
```

世界坐标系的**方向**转换为相对本地坐标系的**方向**

```c#
// 不受物体缩放影响
transform.InverseTransformDirection(Vector3.forward)
// 受物体缩放影响
transform.InverseTransformVector(Vector3.forward)
```

2. 本地坐标系转换为世界坐标

本地坐标系原点的某个**点的(x,y,z)**转换为相对世界坐标系原点的**点(x,y,z)**

```c#
// 受物体缩放影响
transform.TransformDirection(Vector3.forward)
```

本地坐标系的**方向**转换为相对世界坐标系的**方向**

```c#
// 不受物体缩放影响
transform.TransformDirection(Vector3.forward)
// 受物体缩放影响
transform.TransformVector(Vector3.forward)
```

### Input

#### 鼠标输入

1. Input.mousePosition

鼠标的坐标，屏幕的坐标原点在屏幕左下角，往右是 x 轴正方向，往上是 y 轴正方向；
返回值是 Vector3，只有 x 和 y 的值，z 一直为 0；

2. 鼠标事件

- 参数

  - 0 左键
  - 1 右键
  - 2 中键

- Input.GetMouseButtonDown(0) 按下
- Input.GetMouseButtonUp(0) 抬起
- Input.GetMouseButton(0) 长按

3. Input.mouseScrollDelta

中键滚动，返回 Vector 的值，y 为-1 往下滚，1 往上滚；

4. Input.AnyKey

任意键或者鼠标长按一直触发

5. Input.AnyKeyDown

任意键或者鼠标按下时触发

6. Input.inputString

这一帧键盘输入的值

#### 键盘输入

- Input.GetKeyDown(KeyCode.A) 按下
- Input.GetKeyUp() 抬起
- Input.GetKey() 长按

#### 默认轴输入

1. Input.GetAxis

通过返回 -1 ～ 0 ～ 1 来表示坐标轴的方法

- Input.GetAxis("Horizontal")

按 A/D 键时，可以用来控制左右移动

- Input.GetAxis("Vertical")

按 W/S 键时，可以用来控制上下移动

- Input.GetAxis("Mouse X")

鼠标横向移动，可以用来控制左右旋转

- Input.GetAxis("Mouse Y")

鼠标纵向移动，可以用来控制上下旋转

2. Input.GetAxisRaw

使用同上，只是返回值为 -1/0/1，没有中间值

#### 手柄相关

Input.GetButtonDown("Jump") 按下
Input.GetButtonUp() 抬起
Input.GetButton() 长按

#### 移动设备触摸

- Input.touchCount
- Input.touches
- Input.multiTouchEnabled 是否启动多点触控

```c#
if(Input.touchCount > 0){
  Touch t = Input.touches[0];
  // 位置
  t.position;
  // 相对上次的变化
  t.deltaPosition
}
```

#### 陀螺仪

- Input.gyro.enabled 是否开启陀螺仪
- Input.gyro.gravity 重力加速度向量
- Input.gyro.rotationRate 旋转速度
- Input.gyro.attitude 旋转四元素

### 屏幕

- Screen.currentResolution 屏幕分辨率
  - width
  - height
- Screen.width 当前运行窗口的宽度
- Screen.height 当前运行窗口的高度
- Screen.sleepTimeout 屏幕休眠模式
  - SleepTimeout.NeverSleep 不熄屏
  - SleepTimeout.SystemSetting
- Screen.fullScreen 是否全屏
- Screen.fullScreenMode 窗口模式

- Screen.autoRotateToLandscapeLeft 屏幕旋转(左横向)
- Screen.autoRotateToLandscapeRight 屏幕旋转(右横向)
- Screen.autoRotateToPortrait 屏幕旋转(纵向)
- Screen.autoRotateToPortraitUpsideDown 屏幕旋转(逆纵向)
- Screen.orientation = ScreenOrientation.Landscape 指定屏幕方向

- Screen.SetResolution(1920, 1080, false) 设置分辨率

### 摄像机

#### unity 属性

1. Clear Flags

- skybox 天空盒(3D)
- Solid Color 颜色填充(2D)
- Depth only 只画该层，背景透明

2. Culling Mask

选择渲染部分层级，可以指定只渲染对应层级的对象

3. Projection

- Perspective 透视模式

主要是做 3D 游戏，符合人眼看到的效果，近大远小，点光源

- Orthographic 正交模式

主要做 2D 游戏，没有近大远小的效果，平行光

4. 视口大小

- FOV Axis 视场角的轴

确定水平还是垂直方向的视口角度

- Field of view 摄像机视口大小

视口的角度，默认 60；当 FOV Axis 是垂直方向时，Field of view 设置摄像机垂直方向的角度；当是水平方向时，设置水平方向的角度

![视口](./assets/2023-02-27-14-04-26.png)

5. Physical Camera

> 物理摄像机

- Focal Length 焦距
- Sensor Type 传感器类型
- Sensor Size 传感器尺寸
- Lens Shift 透镜移位
- Gate Fit 闸门配合

6. Clipping Planes

裁剪平面距离，设置摄像机可见范围

7. Depth

渲染顺序上的深度，深度越高越后渲染，将会覆盖前面的摄像机的拍出来的图层；

设置 Clear Flags 为 Depth only 属性时，配合 Culling Mask 渲染层级属性，可以实现叠加渲染；

8. Target Texture

熏染纹理，可以把摄像机渲染到一张图上，主要用于制作小地图；

创建 Render Texture 文件设置给 Target Texture；

9. Occlusion Culling

是否启用剔除遮挡，被遮挡住的物体不会渲染

10. 不太重要的一些属性

- Viewport Rect

视口范围，屏幕上将绘制该摄像机视图的位置；

主要用于双摄像机游戏，0-1 相当于宽高百分比；

- Rendering Path 渲染路径
- HDR 是否允许高动态范围渲染
- MSAA 抗锯齿
- Allow Dynamic Resc 是否允许动态分辨率呈现
- Target Display 用于哪个屏幕，多屏幕平台游戏

#### 代码设置

1. 获取摄像机

- Camera.main 主摄像机
- Camera.allCamerasCount 摄像机数量
- Camera.allCameras 所有摄像机

- Camera.onPreCull 渲染相关委托事件

如果开启摄像机剔除处理(Occlusion Culling)选项，可以在剔除前做逻辑处理

- Camera.onPreRender 摄像机渲染前处理的委托
- Camera.onPostRender 摄像机渲染后处理的委托

2. 主要成员

- Camera.main.WorldToScreenPoint 世界坐标转屏幕坐标

转换后的 x/y 为屏幕坐标点位，z 表示物体离摄像机的直角距离

- Camera.main.ScreenToWorldPoint 屏幕坐标转世界坐标

屏幕坐标系只有 x 和 y，z 为 0；z 为 0 表示摄像机所在的点位的那个横切面；

当摄像机透视模式时，z 位 0 的横切面只有一个点，也就是摄像机所在的点位；

当设置 z 了时，屏幕坐标代表距离摄像机直角距离位置的横切面上的 x,y 点位；

```c#
Vector3 v = Input.mousePosition;
v.z = 10;
Camera.main.ScreenToWorldPoint(v);
```

### 光源

1. Type 光源类型

- Spot 聚光灯
  - Range 发光范围距离
  - Spot Angle 光锥角度
- Directional 环境光
- Point 点光源
- Area 面光源

2. Color 颜色
3. Intensity 光源亮度

4. Mode 光源模式

- Realtime 实时光源，每帧实时计算，效果好，性能消耗大
- Baked 烘焙光源，事先计算好，无法动态变化
- Mixed 混合光源，预先计算+实时运算

5. Shadow Type

- NoShadows 关闭阴影
- HardShadows 生硬阴影
- SoftShadows 柔和阴影

6. Cookie 投影遮罩

- Cookie Size 遮罩大小

7. Draw Halo 球形光环开关
8. Flare 耀斑

需要在摄像机上加 Flare Layer 组件才能在看到耀斑

9. Culling Mask 剔除遮罩层

决定哪些层的对象会受到该光源的影响

10. 不重要属性

- Indirect Multiplier 改变间接光的强度
  - 小于 1, 每次反射会时光变暗
  - 大于 1, 每次反射会时光变亮
- RealtimeShadows 阴影
  - Strength 阴影暗度 0-1 之间，越大越黑
  - Resolution 阴影贴图渲染分辨率，越高越逼真，消耗越高
  - Bias 阴影推离光源的距离
  - Normal Bias 阴影投射面沿法线收缩距离
  - Near Panel 渲染阴影的近裁剪面
- Render Mode 渲染优先级
  - Auto 运行时确定
  - Important 以像素质量位单位进行算然，效果逼真，消耗大
  - Not Important 以快速模式进行渲染

#### 光源设置

1. Environment 环境相关设置

- Skybox Material 天空盒材质
- Sun Source 太阳来源，默认使用场景中最亮的方向代表太阳
- Environment Lighting 环境光设置
  - Source 设置光源颜色
    - Skybox 天空盒材质作为环境光颜色
    - Gradient 可以为天空、地平线、地面单独选择颜色和他们之间混合
  - Intensity Multiplier 环境光亮度
  - Ambient Mode 全局光照模式，只有启用了实时全局烘培时才有用
    - Realtime 已弃用
    - Baked

2. OtherSettings

- Fog 雾开关
  - Color 雾颜色
  - Mode 雾计算模式
    - Linear 随距离线性增加
      - Start 离摄像机多远开始有雾
      - End 离摄像机多远完全遮挡
    - Exponential 随距离指数增加
      - Density 强度
    - Exponential Squared 随距离比指数更快的增加
      - Density 强度
- Halo Texture 光源周围围着光环的纹理
- Halo Strength 光环可见性
- Flare Fade Speed 耀斑淡出时间，最初出现之后淡出的时间
- Flare Strength 耀斑可见性
- Spot Cookie 聚光灯剪影纹理

### 碰撞检测

> 两个物体碰撞必须是两个碰撞盒，和一个刚体

#### 刚体

1. Mass 质量，默认为千克

质量越大惯性越大

2. Drag 空气阻力

影响对象受力移动时，所受到的空气阻力大小，0 表示没有阻力

3. Use Gravity 是否受重力影响

4. Angular Drag 扭矩阻力

影响对象受扭矩旋转时，对象旋转受到的空气阻力大小，0 表示没有阻力

5. is Kinematic

启用此选项，对象将不会被物理引擎驱动，简单说相当于没有刚体

6. **Interpolate** 插值运算，让刚体物体移动更平滑

- None 不应用插值运算
- Interpolate 根据前一帧的变换来平滑变换
- Extrapolate 根据下一帧的变换来平滑变换

7. **Collision Detection** 碰撞检测模式

> 用于方式快速移动的对象穿过其他对象而不检测碰撞的问题

> 性能消耗： Continuous Dynamic > Continuous Speculative > Continuous > Discrete

- Discrete 离散检测
- Continuous 连续检测
- Continuous Speculative 连续推测检测
- Continuous Speculative 连续动态检测，性能消耗高

![](./assets/2023-02-27-17-14-59.png)

8. Constraints

约束，对刚体运动的限制，可以限制物体在某些轴上不发生移动/旋转

- Freeze position 有选择地停止刚体沿世界 x、y、z 轴的移动
- Freeze Rotation 有选择地停止刚体围绕局部 x、y、z 周旋转

#### 碰撞体
