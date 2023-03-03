## 基础知识

### 2D 相关

#### 图片

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

#### 纹理

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

在三维计算机图形的贴图渲染中有一个常用的技术被称为Mipmapping；为了加快渲染速度和减少图像锯齿，贴图被处理成由一系列预先计算和优化过的图片组成的文件，这养的贴图就被称为mipmap。

Mipmap需要占用一定的内存空间，mipmap中每一个层级的小图都是主图的一个特定比列的缩小细节的复制品；虽然在某些必要的视角，主图仍然会被使用来渲染细节；但是当贴图被缩小或者只需要从远距离观看时，mipmap就会转换到适当的层级。

因为mipmap贴图需要被读取的像素远少于普通贴图，所以渲染速度得到了提升；而且操作的视角减少了，因为mipmap的图片已经是做过抗锯齿处理的，从而减少了实时渲染的负担，放大和缩小也因为mipmap变得更有效率。

如果贴图基本尺寸是256x256像素的话，mipmap就会有8个层级，每个层级是上一层级的四分之一大小；依次层级大小就是：128x128,64x64,32x32,16x16,8x8,4x4,2x2,1x1(一个像素)。

简单点说，开启Mip Map功能后，Unity会帮助我们根据图片信息生成n张不同分辨率的图片，在场景中会根据我们离该模型的距离选择合适尺寸的图片用于渲染，提升渲染效率。