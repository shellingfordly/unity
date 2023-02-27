## 关于字典的问题

### 字典便利

- 不能在 foreach 遍历字典时修改字典值

```c#
var Dict = new Dictionary<int, int>();

foreach (var item in Dict)
{
    Dict[item.Key] = newValue; // 报错
}
```

- 需要修改字典 Value 时遍历 Keys

```c#
var keys = Dict.Keys.ToList();

for (int i = 0; i < keys.Count; i++)
{
    Dict[keys[i]] = newValue;
}
```

## => 赋值和 = 赋值

=> 赋值时，每次都会重新实例化对象
= 赋值时，用的是同一个对象

```c#
public override T Output { get; } = T.New();
public override T Output => T.New();
```
