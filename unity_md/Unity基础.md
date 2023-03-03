## 基础知识

### Math/Mathf

- PI
- Abs 取绝对值
- CeilToInt 向上取整
- FloorToInt 向下取整
- Clamp 钳制函数
- Max 取最大值
- Min 取最小值
- Pow 次方
- RoundToInt 四舍五入
- Sqrt 平方根
- IsPowerOfTwo(x) 判断 x 是否为 2 的 n 次方
- Sign 判断正负数

- Lerp(start, end, t) 插值运算

> 计算方式： result = start + (end - start) \* t
> t 为插值系数，取之范围 0-1

每帧改变 start 的值，变化速度先快后慢，位置无限接近 end，但不会得到 end 的位置

```c#
start = Mathf.Lerp(start, 10, Time.deltaTime)
```

每帧改变 t 的值：变化速度为匀速，位置每帧接近，当 t>=1 时，得到结果

```c#
time += Time.deltaTime
result = Mathf.Lerp(start, 10, time)
```

### 三角函数

#### 角度和弧度的转换关系

π rad = 180°
1 rad = (180/π)° ==> 1rad = 180/3.14 ≈ 57.3°
1 = (π/180) rad ==> 1° = 3.14 / 180 ≈ 0.01745 rad

弧度 _ 57.3 = 对应角度
角度 _ 0.01745 = 对应弧度

- Mathf.Rad2Deg = 57.3
- Mathf.Deg2Red = 0.01745

```c#
anger = rad * Mathf.Rad2Deg
rad = anger * Mathf.Deg2Rad
```

#### 三角函数

- sin 正弦函数
- cos 余弦函数

Mathf 的三角函数传入的参数需要是弧度

```c#
Mathf.Sin(30*Mathf.Deg2Rad) // 1/2
Mathf.Cos(60*Mathf.Deg2Rad) // 1/2
```

- Asin 反正弦函数
- Acos 反余弦函数

Mathf 的反三角函数得到的结果是对应的弧度

```c#
rad = Mathf.Asin(0.5f);
rad * Mathf.Rad2Deg // 30°
rad = Mathf.Acos(0.5f);
rad * Mathf.Rad2Deg // 60°
```

### 坐标系

- 世界坐标系
  - 原点：世界中心点
  - 轴向：世界坐标系的三个轴向是固定的
- 物体坐标系
  - 原点：物体的中心点
  - 轴向
    - 物体右方为 x 轴正方向
    - 物体上方为 y 轴正方向
    - 物体前方为 z 轴正方向
- 屏幕坐标系
  - 原点：屏幕左下角
  - 轴向：
    - 向右为 x 轴正方向
    - 向上为 y 轴正方向
  - 最大宽高
    - Screen.width
    - Screen.height
- 视口坐标系
  - 原点：屏幕左下角
  - 轴向：
    - 向右为 x 轴正方向
    - 向上为 y 轴正方向
  - 特点
    - 左下角为(0,0)
    - 右上角为(1,1)
    - 和屏幕坐标系类似，将坐标单位化

```c#
// 相对于世界坐标系
transform.position
transform.rotation
transform.eulerAngles
transform.lossyScale

// 物体坐标系 - 相对父对象的物体坐标系位置
transform.localPosition
transform.localRotation
transform.localEulerAngles
transform.localScale

// 屏幕坐标系
Input.mousePosition
Screen.width
Screen.height

// 视口坐标系
Viewport Rect
```

#### 坐标系转换

- 世界转本地
  - transform.InverseTransformDirection
  - transform.InverseTransformPoint
  - transform.InverseTransformVector
- 本地转世界
  - transform.TransformDirection
  - transform.TransformPoint
  - transform.TransformVector
- 世界转屏幕 Camera.main.WorldToScreenPoint
- 屏幕转世界 Camera.main.ScreenToWorldPoint
- 世界转视口 Camera.main.WorldToViewportPoint
- 视口转世界 Camera.main.ViewportToWorldPoint
- 视口转屏幕 Camera.main.ViewportToScreenPoint
- 屏幕转视口 Camera.main.ScreenToViewportPoint

### 向量

- 集合意义
  - 位置：代表某个点
  - 方向：代表某个方向

```c#
// 代表一个点
transform.position
// 代表一方向
transform.forward
transform.up
```

- 两点决定 -- 向量

> 终点减起点

```c#
Vector3 A = new Vector3(1,2,3);
Vector3 B = new Vector3(4,5,6);

// A指向B的向量
Vector3 AB = B - A;
// B指向A的向量
Vector3 BA = A - B;
```

- 零向量 (0,0,0)

  - 零向量是唯一一个大小为 0 的向量
  - Vector3.zero

- 负向量
  - (x,y,z)的负向量为(-x,-y,-z)
  - 负向量和原向量的大小相等，方向相反
  - -Vector3.forward

#### 向量的模长

两点之间的距离，r = √x²+y²+z²

```c#
Vector3 AB = B - A;
// 向量的模长
AB.magnitude
// A,B之间的距离
Vector3.Distance(A, B);
```

#### 单位向量

模长为 1 的向量为单位向量，只需要方向，不想让模长影响计算，一般用来进行移动计算

```c#
Vector3 AB = B - A;
// AB 向量的单位向量
AB / AB.magnitude
```

#### 向量的加减乘除

1. 加法

- 位置 + 位置
  - 没有几何意义
- 向量 + 向量
  - 两个向量相加得到一个新的向量
  - 向量 + 向量 = 向量
  - 向量相加，首尾相连
- 向量 + 位置
  - 位置加向量得到一个新的位置
  - 相当于平移位置

2. 减法

- 位置 - 位置
  - 得到一个新向量
  - 两点决定一向量，终点减起点
- 向量 - 向量
  - 向量相减，头连头，尾连尾
  - A - B = B 头指向 A 头
- 位置 - 向量
  - 位置减向量相当于加负向量
  - 位置 + (-向量) = 位置
  - 相当于平移位置
- 向量 - 位置
  - 没有任何意义

3. 向量乘除

- 向量**乘(除)**标量 = 向量
  - 标量为正数：方向不变，放大缩小模长
  - 标量为负数：方向相反，放大缩小模长
  - 标量为 0：得到零向量

```c#
// 加法  平移位置
transform.position += Vector3.forward * 5;
// 一般直接使用位移方法，本质是向量做加法
transform.Translate(Vector3.forward * 5);
// 加法  平移位置
transform.position -= Vector3.forward * 5;
transform.Translate(-Vector3.forward * 5);

// 乘除  物体缩放
transform.localScale *=2; // 放大
transform.localScale /=2; // 缩小
```

#### 向量点乘

> 向量 A·向量 B = Xa\*Xb + Ya\*Yb + Za\*Zb
> 向量 A ·向量 B = 标量

- 判断对象的方位
- 计算两个向量之间的夹角

1. 几何意义

点乘可以得到一个向量在自己向量方向上的投影的长度

- 点乘结果 > 0 表示两个向量夹角为锐角
- 点乘结果 = 0 表示两个向量夹角为直角
- 点乘结果 < 0 表示两个向量夹角为钝角

![](/assets/2023-03-03-14-47-35.png)

```c#
// 调试画线
// 画线段
Debug.DrawLine(transform.position, transform.forward, Color.red);
// 画射线
Debug.DrawRay(transform.position, transform.forward, Color.red);

// 利用向量点乘来判断物体在我的前方还是后方
// 用我的正前方 单位向量，点乘 我所在位置和目标位置连接的向量 来判断目标的方位
float dot = Vector3.Dot(transform.forward, target.position - transform.position);
if(dot >= 0) {} //前方
else {} // 后方
```

2. 点乘求夹角

A,B 为单位向量，则有 cosβ = x / B 的模长(1)，即 cosβ = x，又 x = A · B，则 cosβ = A · B，得出 β = Acos(A · B)

<img src="/assets/2023-03-03-15-08-59.png" width=200px>

- 利用公式求夹角

```c#
// transform.forward 我的正前方单位向量
// b 我与目标连接向量的单位向量(normalized)
var b = (target.position - transform.position).normalized;
// 得到 A · B
float dot = Vector3.Dot(transform.forward, b);
// 角度 = Acos(A · B)
var anger = Mathf.Acos(dot) * Mathf.Rad2Deg;
```

- 使用 Vector3 自带方法

```c#
var anger = Vector3.Angle(transform.forward, target.position - transform.position);
```

#### 向量叉乘

- 得到一个平面的法向量
- 得到两个向量之间的左右位置关系

1. 计算公式

向量 X 向量 = 向量
向量 A(Xa,Ya,Za)
向量 B(Xb,Yb,Zb)
A x B = (X, Y, Z)
X = YaZb - ZaYb
Y = ZaXb - Xa-Zb
Z = XaYb - YaXb

- Vector3.Cross(A.position, B.position)

2. 几何意义

- AxB 得到向量垂直 A 和 B
- AxB 向量垂直于 A 和 B 组成的平面，是此平面的法向量
- AxB = -(BxA)

```c#
// 判断B在A的哪边
Vector3 c = Vector3.Cross(A.position, B.position);
if( c.y > 0) {} // B在A的右侧
else {} // B在A的左侧
// 判断A在B的哪边
Vector3 c = Vector3.Cross(B.position, A.position);
if( c.y > 0) {} // A在B的右侧
else {} // A在B的左侧
```


