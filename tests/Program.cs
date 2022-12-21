// // See https://aka.ms/new-console-template for more information

// // Dictionary<int, bool> dic = new Dictionary<int, bool>();

// // List<int> arr = new List<int> { 101, 102, 103 };
// // List<bool> arr1 = new List<bool> { false, true, true };


// // int a = arr[];
// // int value = arr.Find(v => v == 102);
// // Console.WriteLine("value: {0}", value);



// // arr.ForEach((v) =>
// // {
// //   Console.WriteLine(v);
// // });


// // List<int> arr3 = arr.Select(e => e / 10).ToList();

// // arr3.ForEach(v =>
// // {
// //   Console.WriteLine(v);
// // });



// // foreach (var v in dic)
// // {
// //   Console.WriteLine(v);
// // }

// // List<int> templateIds = new List<int>() { 0, 1, 2, 3 };
// // List<int> weights = new List<int>();


// // weights.AddRange(templateIds.Select(v => 0));

// // weights.ForEach(v =>
// // {
// //   Console.WriteLine(v);

// // });


// List<int> F = new List<int>() { 1, 2, 3 };

// List<int> C = new List<int>() { -1, -2, -3 };

// // C.AddRange(F);
// // C.AddRange(F);

// int i = 0;

// var V = F.Select((v) =>
// {
//   int value = v + C[i];
//   i++;
//   return value;
// }).ToList();

// V.ForEach(v =>
// {
//   Console.WriteLine(v);
// });

// // int res = F.Aggregate(0, (p, n) => p + n);

// // Console.WriteLine(res);
// // Console.WriteLine($"{C.Count > 0 && C[0] == 1}");


// // Dictionary<int, int> Attributes = new Dictionary<int, int>();
// // Attributes.Add(1, 1);
// // Attributes.Add(2, 2);
// // Attributes.Add(3, 3);
// // Attributes.Add(4, 4);


// // string a = "item_1";

// // Console.WriteLine(a.Substring(4));


// List<int> weight = new List<int>() { 1, 2, 3 };
// List<int> weightEx = new List<int>() { -1, -2, -3 };
// List<int> newWeight1 = weight.Select(v => v + weightEx[weight.IndexOf(v)]).ToList();
// Console.WriteLine("====");

// newWeight1.ForEach(v =>
// {
//   Console.WriteLine(v);
// });


// Dictionary<int, int> Attributes = new Dictionary<int, int>();
// List<int> EvolveValueBase = new List<int>() { 100, 100, 100 };
// Attributes.Add(102, 100);
// Attributes.Add(103, 200);
// Attributes.Add(104, 300);

// int index = 0;
// foreach (var attr in Attributes)
// {
//   Attributes[attr.Key] += EvolveValueBase[index];
//   index++;
// };
// Console.WriteLine("========");

// foreach (var attr in Attributes)
// {
//   Console.WriteLine($"attr key: {attr.Key}, attr value :{attr.Value}");
// };




// List<ArrItem> arr = new List<ArrItem>();

// arr.Add(new ArrItem() { id = 1 });
// arr.Add(new ArrItem() { id = 2 });
// arr.Add(new ArrItem() { id = 3 });

// arr.Remove(arr[0]);
// arr.ForEach(v =>
// {
//     Console.WriteLine(v.id);
// });


// var t = new Test();

// t.getType<ArrItem>();

// class Test
// {

//     public void getType<T>()
//     {
//         if (typeof(T) == typeof(ArrItem))
//         {
//             Console.WriteLine("====");
//         }
//         return;
//     }
// }

// List<ArrItem> arr = new List<ArrItem>();

// List<int> arr1 = (List<int>)arr.Select(v => v.id);


// arr1.ForEach(v =>
// {
//     Console.WriteLine(v);
// });

// class ArrItem
// {
//     public int id = 0;
// };



// List<int> weight = new List<int>() { 1, 2, 3 };

// var a = weight.IndexOf(4);
// Console.WriteLine(a);



// var a = one.getChild(1);
// Console.WriteLine(a);


// class OneClass : BaseClass
// {

//     public int getChild(int index)
//     {
//         return getChild(1) * 2;
//     }
// }
// BaseClass one = new BaseClass();

// int a = one.getChild(1);
// int b = one.getChild(1, 2);

// Console.WriteLine($"a:{a},b:{b}");

// class BaseClass
// {

//     public int getChild(int index)
//     {

//         return index + 1;
//     }
//     public int getChild(int index, int b)
//     {
//         return index + b;
//     }
// }


// enum c
// {
//     a = "sssss",
// }


// Dictionary<string, string> Dic = new Dictionary<string, string>();


// Dictionary<int, string> dic = new Dictionary<int, string>();
// dic.Add(1, "一");
// dic.Add(3, "三");
// dic.Add(2, "二");
// foreach (KeyValuePair<int, string> item in dic)
// {
//     Console.WriteLine(item.Key.ToString() + item.Value.ToString());
// }


// List<int> UsableTemplateList = new List<int>();

// UsableTemplateList.Add(1);
// UsableTemplateList.Add(2);
// UsableTemplateList.Add(3);
// UsableTemplateList.Remove(2);
// UsableTemplateList.Add(2);
// Console.WriteLine(UsableTemplateList.Count);

// double a = Math.Min(1, 2);
// Console.WriteLine((int)(a));


// Dictionary<int, int> dic = new Dictionary<int, int>();


// List<int> weight = new List<int>() { 1, 2, 3 };


// int level = 10;
// int minor = 2;

// int forage = 13;

// double res = weight.Average(v => v);

// Console.WriteLine(--forage);
// Console.WriteLine(forage--);
// Console.WriteLine(forage--);





// var DragonExplore = new Dictionary<int, int>();

// DragonExplore.Add(0, 1);
// DragonExplore.Add(1, 1);
// DragonExplore.Add(2, 1);
// DragonExplore.Add(3, 1);

// Console.WriteLine(DragonExplore.Count);

// var keys = DragonExplore.Keys.ToList();
// int res = 0;
// foreach (var item in DragonExplore)
// {
//     res += item.Value;
// }
// for (int i = 0; i < keys.Count; i++)
// {
//     if (i > 1)
//     {
//         DragonExplore[keys[i]] = 0;
//     }
// }
// foreach (var item in DragonExplore)
// {
//     Console.WriteLine($"key:{item.Key}, value: {item.Value}");
// }
// Console.WriteLine($"res: {res}");

// string name = "btn_1";
// int a = Int32.Parse(name.Substring(4));
// Console.WriteLine($"res: {a}");


// OutClass.TestOutFunc(out int a);

// Console.WriteLine(a);
// class OutClass
// {


//     public static void TestOutFunc(int a)
//     {
//         a = 100000000;
//     }
// }



// List<Test> arr = new List<Test>() { new Test(1), new Test(2) };

// var a = arr.Find(v => v.id == 2);
// a.id = 3;
// // Console.WriteLine(a == null);

// arr.ForEach(v =>
// {
//     Console.WriteLine(v.id);

// });



// class Test
// {
//     public int id;

//     public Test(int id)
//     {
//         this.id = id;
//     }

// }


// List<int> weight = new List<int>() { 1, 2, 3, 4, 5, 6 };
// List<float> weightEx = new List<float>() { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f };

// List<float> newWeight = weight.Select(v => v + weightEx[weight.IndexOf(v)]).ToList();

// newWeight.ForEach(v =>
// {

//     Console.WriteLine(v);
// });
// var a = weight.Find(v => v == 3);

// var b = weight.Exists(t => t == 7);
// Console.WriteLine(Math.Floor(0.2));
// Console.WriteLine(Math.Floor(0.8));



// Console.WriteLine("================");
// List<int> arr = new List<int>() { 1, 2, 3 };
// List<int> vvv = arr.Select((v, i) => v + v * i).ToList();

// vvv.ForEach(v =>
// {
//     Console.WriteLine(v);
// });

// var arr1 = arr.FindIndex(v => v > 5);
// Console.WriteLine(arr1);

// var dic = new Dictionary<int, int>(){
//     {1,1}
// };

// Console.WriteLine(dic.GetType() == typeof(Dictionary<int, int>));


// var data = new Dictionary<int, List<int>>();
// var posType = 0;

// if (!data.ContainsKey(posType))
// {
//     data[posType] = new List<int>();
// }
// data[posType].Add(1000);

// foreach (var item in data)
// {
//     Console.WriteLine(item.Key);
//     Console.WriteLine(item.Value.Count);
// }



// Console.WriteLine($"{4 / 5}");
// Console.WriteLine($"{4 % 5}");
// Console.WriteLine($"{5 / 5}");
// Console.WriteLine($"{5 % 5}");


// List<int> arr = new List<int>() { 1, 2, 3 };
// arr.Clear();
// Console.WriteLine(arr.Count);

// interface I
// {
//     int i { get; set; }
//     string name { get; set; }
// }

// abstract class AA
// {
//     public abstract void Func(); // 虚函数
// }
// class BB : AA
// {
//     public override void Func() // 虚函数
//     {
//         Console.WriteLine("Func In A");
//     }
// }

// class CC : AA
// {
//     public override void Func() // 虚函数
//     {
//         Console.WriteLine("Func In A");
//     }
// }


// public partial class A
// {
//     // 分部方法声明
//     partial void Write();
// }
// partial class A
// {
//     // 分部方法实现
//     partial void Write()
//     {
//         Console.WriteLine("这是一个分部方法");
//     }
// }


IContravariant<Object> iobj = new Sample<Object>();
IContravariant<String> istr = new Sample<String>();
iobj = (IContravariant<Object>)istr;
Console.WriteLine(iobj == istr);

interface IContravariant<in A> { }

interface IExtContravariant<in A> : IContravariant<A> { }

class Sample<A> : IContravariant<A> { }
