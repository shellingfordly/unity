# c#基础

## 命名空间

### using

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

---

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

---

---

## 字符

### 字符串比较

- ==
- Equals true/false
- Compare -1/0/1 小于/等于/大于
- CompareTo -1/0/1

```c#
string.Equals(str1, str2); // true/false
string.Compare(str1, str2); // -1/0/1
str1.CompareTo(str2); // -1/0/1
```

### 字符串格式化

- C 货币值 ¥1000
- D 只用于整数，整数 1000
- E 科学计数法 1.000330E+003
- F 小数点后位数固定 1000.33
- G 整数 1000
- N 通用(带分隔符) 1,000
- P 百分数 10%
- X 只用于整数，十六进制格式

### 常用方法

- Substring(index, count) 截取
- Split(char) 分割
- Split(new char[])
- Concat(str1, str2) 合并

```c#
Split('-s')
Split(new char[] {'-', '='})
```

## 方法

### 关键字

- virtual 虚方法
- override 重写(虚方法)
- overload 重载
- new 覆写(覆盖父类的方法)
- sealed 不让子类重写方法
- extern 声明外部方法
- partial 声明分部方法
- abstract 抽象类

- public
- private
- protected 只能在当前类和子类访问
- internal 当前项目访问
- protected internal 当前项目当前类和子类访问

#### 重写/重载/覆写 的区别

1. override 重写

- 函数特征(函数名、参数类型个数)相同
- 使用 override 重写时，不能修改可访问性(public/private)
- 重写属性必须是 virtual、abstract 或 override
- 父类的 abstract 在子类中必须有 override 对应，virtual 则可以没有
- 有 override 必有父子类关系

2. overload 重载

- 出现在同一个类中
- 参数列表不同

3. new 覆写

- 覆盖不会改变父类方法的功能

#### abstract 抽象类

- abstract 可以用于类、方法、属性、索引和事件
- abstract 标识的属性必须在 abstract 标识的抽象类下
- 抽象类不能实例化，由子类来实现
- 无法使用 sealed 修改抽象类，因为 sealed 阻止被继承，而 abstract 修饰符要求类被继承

### 虚方法

当调用一个对象的函数时，系统会之间去检查这个对象的声明的类，是否为虚方法；
如果不是，直接执行；
如果是虚方法，则不会立即执行，而是检查实例类(即 new 的类)是否有重写虚方法，如果有就执行，没有则像父类查找，直到找到第一个重写的父类为止，执行重写方法。

```c#
class A
{
    public virtual void Func() // 虚函数
    {
        Console.WriteLine("Func In A");
    }
}
class B : A
{
    public override void Func() // 重新实现了虚函数
    {
        Console.WriteLine("Func In B");
    }
}
class D : A
{
    public new void Func() // new 覆盖父类里的同名方法，不是重写
    {
        Console.WriteLine("Func In D");
    }
}

A a = new B();
A d = new D();

a.Func(); // Func为虚函数，B类中有重写，执行B类中重写的虚函数，结果为 Func In B
d.Func(); // D类中没有重写，不会执行new的覆盖函数，执行A类的虚函数，结果为 Func In A
```

### 外部方法

声明外部方法，使用关键字 extern
配合 DLLImport 属性使用时必须包含 static 字段

```c#
[DLLImport("User32.dll")]
public static extern int MessageBox(int h, string m, string c, int type);
```

### 分部方法

- 使用 partial 关键字
- 方法必须返回 void，只能默认为 private
- 不能为 virtual 和 extern 方法
- 可以有 ref 参数，不能有 out 参数
- 分部方法必须有定义，可以不实现，但实现的方法必须先定义

```c#
public partial class A
{
    public static int a = 1;
    // 分部方法声明
    partial void Write();
}
partial class A
{
    // 分部方法实现，可以没有，一旦有，必须先声明
    partial void Write()
    {
        Console.WriteLine("这是一个分部方法");
    }
}
```

### 参数

#### 引用参数

- 修改值形参不会影响为其传递的实参；
- 使用引用参数可以，引用参数指出的存储位置与自变量相同；

```c#
static void Swap(ref int x, ref int y)
{
    int temp = x;
    x = y;
    y = temp;
}

public static void SwapExample()
{
    int i = 1, j = 2;
    Swap(ref i, ref j);
    // 引用参数将改变实参的值
    Console.WriteLine($"{i} {j}");    // "2 1"
}
```

#### 输出参数

输出参数与引用参数类似，作用是将参数赋值给调用方提供给的自变量

```c#
static void Divide(int x, int y, out int quotient, out int remainder)
{
    quotient = x / y;
    remainder = x % y;
}

public static void OutUsage()
{
    Divide(10, 3, out int quo, out int rem);
    Console.WriteLine($"{quo} {rem}");	// "3 1"
}
```

#### 参数数组

params 修饰符进行声明

```c#
public class Console
{
    public static void Write(string fmt, params object[] args) { }
    public static void WriteLine(string fmt, params object[] args) { }
    // ...
}

int x = 3, y = 4, z = 5;
Console.WriteLine("x={0} y={1} z={2}", x, y, z);
object[] args = new object[3];
Console.WriteLine(s, args);
```

## 接口
