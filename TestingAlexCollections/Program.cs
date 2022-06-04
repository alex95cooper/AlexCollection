using AlexCollections;

AlexDictionary<int, string> dictionaty = new() { { 1, "hello " }, { 2, "Its " }, { 3, "me" }, { 4, ", " }, { 5, "Mario" } };

foreach (var item in dictionaty)
    Console.Write(item.Value);

Console.WriteLine();

Console.WriteLine(dictionaty[5]);

if (!dictionaty.TryGetValue(6, out string name))
    Console.WriteLine("Im not exist" + name);

Console.WriteLine(dictionaty.ContainsValue("Mario"));

Console.WriteLine(dictionaty.TryAdd(5, "!"));



