# C#

## 基础

### 代码

- using 引入命名空间
- namespace 命名空间(类的住址)，对类进行逻辑上的划分，避免重名

```c#
using System;

namespace Test
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }
  }
}
```

## 变量

在内存中开辟一块空间

### 数据类型

#### 容量的单位

- 位 bit(比特)：电脑记忆体中的最小单位，每一位可以代表 0 或者 1.
- 字节 Byte：电脑中存储的最小单位
  - 1Byte = 8bit 1KB = 1024Byte
  - 1M = 1024KB 1G = 1024MB
- 网速 10M 只的是 Mbps(兆位/秒)是速率单位，换算成字节为 10/8 = 1.25 兆字节/秒

#### 整型（整数）

- 1 个字节：有符号 sbyte(-128~127)，无符号 byte（0-255）
- 2 个字节：有符号 short（-32768 ～ 32767）
- 4 字节：有符号 int，无符号 uint
- 8 字节：有符号 long，无符号 ulong

#### 非整型

- 4 字节：单精度浮点类型 float，精度 7 位
- 8 字节：双精度浮点类型 double，精度 15-16 位
- 16 字节：128 位数据类型 decimal，精度 28-29 位，适用于财务和货币计算

- 注意事项
  - 非整型变量赋值要加上后缀，不加默认位 double
  - 浮点型运算会出现舍入误差 bool number = 1.0f -0.9f == 0.1f

二进制无法精确的表示 1/10，就像十进制无法精确的表示 1/3，所以二进制表示十进制会有一些舍入误差，对于精度要求较高的场合会导致代码缺陷，可以使用 decimal 代替。

#### 非数值型

- char 字符，2 字节，存储单个字符，使用单引号
- string 字符串，存储文本，使用双引号
- bool 类型，1 字节，true/false

### 占位符

- {0}
- {0:c} 数字字符串格式化
  - c 货币
  - d2 指定 2 位数字填充
  - f1 指定精度显示
  - p 指定精度以百分数显示

```c#
string.Format("{0},{1}", 1, 2); // 1,2
string.Format("{0:c}", 10); // 10:00
string.Format("{0:d2}", 1); // 01
string.Format("{0:f1}", 1.26); // 1.3
string.Format("{0:p}", 0.1); // 10.00%
string.Format("{0:p0}", 0.1); // 10%
```

### 运算符

- = 赋值预算符
- +-\*\\ 算术运算符
- \> \< == 比较运算符
- && || 逻辑运算符(判断 bool 值关系的符号)
- += \*= /= %= 快捷运算符

### 数据类型转换

- Parse
- ToString

```c#
string strNumber = "18";
int num1 = int.Parse(strNumber); // 18
float num1 = float.Parse(strNumber); // 18.0
string str =  num1.ToString();// 18.0
```

- 强制转换

```c#
float num1 = 1;
double num2 =2;
short res = (short)(num1 + num2)
```

### 数组

#### 属性

- IsFixedSize 数组是否固定大小
- IsReadOnly 是否只读
- Length 长度
- LongLength 64 位整数长度
- Rank 数组维度

#### 方法

- Clear 设置某个范围的元素为 0/false/null
- Copy(Array, Array, Int32) 从第一个参数拷贝多少个到第二参数
- GetLength 表示指定维度的数组中的元素总数(适合多维数组)

#### 多维数组/交错数组

```c#
// 交错数组
int[][] arr2 = new int[][]{
  new int[]{0},
  new int[]{1,2},
  new int[]{3,4,5},
};
// 多维数组
int [,] arr3 = new int [2,2] {
  {1,2},
  {3,4}
};
Console.WriteLine(arr2.Length); // 3
Console.WriteLine(arr2.GetLength(0)); // 3
Console.WriteLine(arr2.GetLength(1)); // 报错
Console.WriteLine(arr3.Length); // 4
Console.WriteLine(arr3.GetLength(0)); // 2
Console.WriteLine(arr3.GetLength(1)); // 2
```

#### 参数数组

params 参数数组，可以传数组，变量集合，或者不传。

```c#
Add(new int[] {1,2,3});
Add(1,2);
Add();
int Add(params int[] arr){
  return arr.Length
}
```

### 类型分类

- 值类型
  - 结构
    - 数值类型
    - bool
    - char
  - 枚举
- 引用类型
  - 接口
  - 类 string Array 委托
