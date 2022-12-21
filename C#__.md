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

## 接口
