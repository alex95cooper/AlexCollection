using AlexList;

AlexList<int> alexList = new();
alexList.Add(1);
alexList.Add(2);
alexList.Add(3);
alexList.Add(4);
alexList.Add(5);
alexList.Add(6);
alexList.Insert(8, 3);


foreach (var i in alexList)
    Console.WriteLine(i);

alexList.RemoveAt(3);
Console.WriteLine();
foreach (var i in alexList)
    Console.WriteLine(i);

Console.ReadLine();
