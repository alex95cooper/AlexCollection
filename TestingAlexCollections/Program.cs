using AlexCollections;

AlexDictionary<int, string> dictionaty = new() { { 1, "hello " }, { 2, "Its " }, { 3, "me" }, { 4, ", " }, { 5, "Mario" } };

foreach (var item in dictionaty)
    Console.Write(item.Value);

Console.WriteLine();

var mykeylist = dictionaty.Keys;

foreach (var key in mykeylist)
    Console.Write(key);

Console.WriteLine();
Console.WriteLine(dictionaty[5]);

Console.WriteLine();

if (!dictionaty.TryGetValue(6, out string name))
    Console.WriteLine("Im not exist" + name);

Console.WriteLine(dictionaty.ContainsValue("Mario"));

Console.WriteLine();

foreach (var key in mykeylist)
{
    Console.Write(key);

    if (key == 3)
    {
        dictionaty.Remove(key);
    }
}


