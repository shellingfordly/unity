# 基础知识

## 2D 相关

### 图片

1. unity 支持的图片格式

- BMP 占磁盘空间大
- TIF 体积大
- JPG 文件小、有损失、无透明通道
- PNG 无损压缩、文件小、有透明通道
- TGA 体积小、有透明通道
- PSD UI 界面
- EXR、GIF、HDR、IFF、PICT

2. 图片设置

- 纹理类型
- 纹理形状
- 高级设置
- 平铺拉伸
- 平台设置
- 预览窗口

### 纹理

#### 纹理类型

1. Default 默认纹理

- sRGB(Color Texture)

启用可以将纹理存储在伽马空间中(对每一个像素做一次幂函数运算)
简单点说就是做色彩灰阶的校准，使其能让人眼看起来正常，一般开启

- Alpha Source 指定如何生成纹理的 Alpha 通道

  - None 关闭透明通道，无论纹理有没有
  - Input Texture Alpha 输入纹理中的 Alpha
  - From Gray Scale 从输入纹理 RGB 值的平均值生成 Alpha(自己计算 Alpha)

- Alpha Is Transparency 启用可以避免边缘上的过滤瑕疵

2. Normal map 法线贴图格式

法线贴图就是在原物体的凹凸表面的每个点上均作法线
法线就是垂直于某个点的切线的方向向量

开启 Create From GrayScale 后可设置以下参数

- Bumpiness 控制凹凸程度，值越大凹凸感越强
- Filtering 如何计算凹凸值
  - Sharp 生成比标准模式更锐利的法线贴图
  - Smooth 使用标准算法生成法线贴图

3. Editor GUI Legacy GUI

一般在编辑器中或者 GUI 上使用的纹理

4. Sprite (2D and UI)

- Sprite Mode
  - Single 按原样使用精灵图
  - Multiple 瓦片模式，如果是图集，使用该选项
  - Polygon 网格精灵模式
- Pixels Per Unit 世界空间中的一个距离单位对应图片该有多少像素
- MeshType 网格类型，至于 Single 和 Multiple 模式才支持
  - Full Rect 创建四边形，讲精灵显示在四边形上
  - Tight 基于像素 Alpha 值来生成网格，更加贴合精灵图片的形状，任何小于 32\*32 的精灵都用时 FullReact 模式，即使设置成 Tight 模式也是
- Extrude Edges 使用滑动条确定生成的网格中精灵周围流出的区域大小
- Pivot 精灵图的轴心点，Single 模式才有此选项，对应九宫格布局的九个点，可以自定义，决定了旋转的中心点
- Generate Physics Shape 启用此选项，Unity 会自动根据精灵轮廓生成默认物理形状
- Sprite Editor 编辑 Sprite，需要安装 2D Sprite 包

5. Cursor 自定义光标
6. Cookie 光源剪影格式

Light Type 应用光源类型，一般点光源的剪影需要设置为立方体纹理；方向光和聚光灯的剪影设置为 2D 纹理

- Spotlight 聚光灯类型，需要边缘纯黑纹理
- Directional 方向光，平铺纹理
- Point 点光源，需要设置为立方体形状

7. Light map 光照贴图格式

8. Single Channel 纹理只需要单通道格式

- Channel 希望纹理处理为 Alpha 还是 Red 通道
  - Alpha 不允许进行压缩
  - Red

#### 纹理形状

Texture Shape
纹理可以用于模型贴图，或者制作天空盒和反射探针
纹理形状设置，就是用于在两种模式之间进行切换

1. 2D

2D 纹理，最常用设置，这些纹理讲使用到模型和 GUI 元素上

2. Cube

立方体贴图，主要用于天空盒和反射探针

- Mapping 如何将纹理投影到游戏对象上
  - Auto 根据纹理信息创建布局
  - 6 Frames Layout 纹理包含标准立方体贴图布局之一排列的六个图像
  - Latitude-Longitude Layout 将纹理映射到 2D 维度/经度
  - Mirrored Ball 将纹理映射到类似球体的立方体贴图上
- Convolution Type 纹理的过滤类型
  - None 无过滤
  - Specular 将立方体作为反射探针
  - Diffuse 将纹理进行过滤表示辐照度，可作为光照探针
- Fixup Edge Seams
  - Convolution Type 为 None 和 Diffuse 时有效
  - 解决地段设备上面之间立方体贴图过滤错误

#### 高级设置

1. Non-Power of 2

如果纹理尺寸非 2 的幂如何处理，根据图形学的规则，纹理必须是 2 的幂的尺寸

- None 纹理尺寸大小保持不变
- To nearest 将纹理缩放到最接近 2 的幂的大小(注意：PVRTC 格式要求纹理为正方形)
- To larger 将纹理缩放到最大尺寸大小值的 2 幂的大小
- To smaller 将纹理缩放到最小尺寸大小值的 2 的幂的大小

2. Read/Write Enabled

启用可以使用 Unity 中提供的一些方法从纹理中获取到数据
一般需要获取图片数据时才开启

3. Streaming Mipmaps

启用则可以使用纹理串流，主要用于在控制加载在内存中的 Mipmap 级别，用于减少 Unity 对于纹理所需的内存总量，用性能换内存

- MipMap Priority

MipMap 有限级，Unity 根据优先级来确定分配资源时优先考虑哪些 MipMap

4. Generate Mip Maps 允许生成 MipMap

- Border Mip Maps 启用可避免颜色向外渗透到较低 MIP 级别的边缘
- Mip Map Filtering
  - Box 随着尺寸减小，级别更加平滑
  - Kaiser 随着 Mipmap 尺寸大小下降而使用的锐化算法，如果远处纹理太模糊，可以使用该算法
- Mip Maps Preserve Coverage
  - Mipmap 的 Alpha 通道在 Alpha 测试期间保留覆盖率
  - Alpha Cutoff Value 覆盖率参考值
- Fadeout Mip Maps 级别递减时使 Mipmap 淡化为灰色

#### Mip Map

在三维计算机图形的贴图渲染中有一个常用的技术被称为 Mipmapping；为了加快渲染速度和减少图像锯齿，贴图被处理成由一系列预先计算和优化过的图片组成的文件，这养的贴图就被称为 mipmap。

Mipmap 需要占用一定的内存空间，mipmap 中每一个层级的小图都是主图的一个特定比列的缩小细节的复制品；虽然在某些必要的视角，主图仍然会被使用来渲染细节；但是当贴图被缩小或者只需要从远距离观看时，mipmap 就会转换到适当的层级。

因为 mipmap 贴图需要被读取的像素远少于普通贴图，所以渲染速度得到了提升；而且操作的视角减少了，因为 mipmap 的图片已经是做过抗锯齿处理的，从而减少了实时渲染的负担，放大和缩小也因为 mipmap 变得更有效率。

如果贴图基本尺寸是 256x256 像素的话，mipmap 就会有 8 个层级，每个层级是上一层级的四分之一大小；依次层级大小就是：128x128,64x64,32x32,16x16,8x8,4x4,2x2,1x1(一个像素)。

简单点说，开启 Mip Map 功能后，Unity 会帮助我们根据图片信息生成 n 张不同分辨率的图片，在场景中会根据我们离该模型的距离选择合适尺寸的图片用于渲染，提升渲染效率。

#### 平铺拉伸

1. Wrap Mode 平铺纹理

- Repeat 在区块中重复纹理
- Clamp 拉伸纹理的边缘
- Mirror 在每个整数边界上镜像纹理以创建重复图案
- Mirror Once 镜像纹理一次，然后将拉伸边缘纹理
- Per-axis 单独控制如何在 U 轴和 V 轴上包裹纹理

2. Filter Mode 纹理在通过 3D 变化拉伸时如何进行过度
3. Aniso Level 以大角度查看纹理时提高纹理质量，性能消耗高

#### 平台设置

1. MaxSize

设置导入的纹理的最大尺寸，即使美术出的图很大，也可以通过这里把图片限制在一定的范围内

2. Resize Algorithm

当纹理尺寸大于指定的 Max Size 时，使用的缩小算法

- Mitchell 默认米切尔算法来调整大小，该算法是常用的尺寸缩小算法
- Bilinear 双线性插值来调整大小；如果细节很重要的图片，它比 Mitchell 算法保留更多细节

3. Format 纹理格式

各平台支持的格式有所不同，如果选择 Automatic，会根据平台使用默认设置。

- IOS
  - PVRTC
  - ASTC
- Android
  - ASTC
  - ETC2/EAC
  - ETC
  - RGBA 16 位
  - RGBA 32 位

4. Compression

选择纹理的压缩类型，帮助 Unity 正确选择压缩格式，会根据平台和压缩格式的可用性进行压缩

- None 不压缩纹理
- Low Quality 以低质量格式压缩纹理
- Normal Quality 以标准格式压缩纹理
- High Quality 以高质量格式压缩纹理

5. Use Crunch Compression

启用后使用 Crunch 压缩，Crunch 是一种基于 DXT 或 ETC 纹理压缩的有损压缩格式、压缩时间长、压缩速度快

- Compressor Quality 压缩质量，质量越高意味着纹理越大，压缩时间越长

6. Split Alpha Channel

当 Format 为 ETC 格式时，才会出现此参数

Alpha 通道分离，节约内存，会把一张图分成两张纹理。一张包涵 RGB 数据，一张包涵 Alpha 数据，在渲染时在合并渲染。

7. Override ETC2 Fallback

不支持 ETC2 压缩的设备上使用的格式

### Sprite

#### Sprite Editor

##### Sprite 图片编辑

1. 参数

- Name
- Position 在图片中的偏移位置和宽高
- Border 边框，用于设置九宫格的 4 跳边
- Pivot 轴心(中心)点位置
  - Pivot Unit Mode 轴心点单位模式
  - Normalized 标准化模式，0-1
  - Pixels 像素模式
- Custom Pivot 自定义轴心点

2. Custom Outline 渲染区域

自定义边缘线设置，可以自定义精灵网格的轮廓形状；
默认情况下在矩形网格上渲染，边缘外部透明区域会被渲染，浪费性能；
使用自定义轮廓，可以调小透明区域，提高性能

- Snap 将控制点贴近在最近的像素
- Outline Tolerance 轮廓点的复杂性和准确性，0-1 值越大轮廓点越多，越准确
- Generate 生成网格轮廓

3. Custom Physics Shape 决定碰撞判断区域

自定义精灵图片的物理性状，主要用于设置需要物理碰撞判断的 2D 图形；
决定了之后产生碰撞检测的区域；
参数和 Custom Outline 一样；

4. Secondary Textures 为图片添加特殊效果

次要纹理设置，可以将其他纹理和该精灵图关联
着色器可以得到这些辅助纹理然后用于做一些效果处理，让精灵应用其他效果

##### Multiple 图集元素分割

1. Automatic 自动分割

- Pivot 单张图片轴心点位置
- Custom Pivot 自定义轴心点
  - Delete Existing 替换已选择的任何矩形
  - Smart 尝试创建新矩形同时保留或调整现有矩形
  - Safe 添加新矩形而不更改任何已经存在的矩形
- Method 如何处理现有对象

2. Grid By Cell Size 按单元格大小切割

- Pixel Size 单元格宽高
- Offset 偏移位置
- Padding 和边缘的偏移位置
- Keep Empty Rects 是否保留空矩形

3. Grid Bey Cell Count

- Column & Row 行列数

##### Polygon 多边形编辑

#### Sprite Renderer

1. Sprite 渲染的精灵图片
2. Color 定义着色，一般没有特殊需求不会修改
3. Filp 水平或者竖直翻转精灵图片
4. Draw Mode 绘制模式，当尺寸变化时的缩放方式

- Simple 简单模式，缩放时整个图像一起缩放
- Sliced 切片模式
  - 9宫格切片模式，十字区域缩放，4个角不变化
  - 一般用于变化不大的纯色图
  - 需要把精灵图的网格类型设置为Full Rect
- Tiled 平铺模式，将中间部分平铺而不是缩放
  - 网格类型Full Rect
  - Continuous 当尺寸变化时，中间部分将均匀平埔
  - Adaptive 当尺寸变化时，类似Simple模式，当更改尺寸达到Stretch Value时，中间才开始平铺

5. Mask Interaction 与精灵遮罩交互时的方式

- None 不与场景中任何精灵遮罩交互
- Visible Inside Mask 精灵遮罩覆盖的地方可见，而遮罩外部不可见
- Visible Outside Mask 精灵遮罩外部的地方可见，而遮罩覆盖处不可见

6. Sprite Sort Point

计算摄像机和精灵之间距离时，使用精灵中心Center还是轴心点Pivot，一般情况不修改

7. Material 材质

可以使用一些自定义材质来显示一些特殊效果，一般情况不修改；
默认材质时不会受到光照影响的，如果想要受光照影响，可以选择Default Diffuse

8. Additional Setting 高级设置

- Sorting Layer 排序层选择
- Order in Layer 层级序列号，数值越大显示在越前面


9. 通过代码设置

```c#
GameObject obj = new GameObject();
SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
sr.sprite = Resources.Load<Sprite>("name");
// sr.color
// sr.filp
Sprite[] sprList = Resources.LoadAll<Sprite>("name");
```

#### Sprite Creator

#### Sprite Mask

#### Sprite Group

#### 图集制作

### 物理系统

### Sprite Shape

### Tile map
