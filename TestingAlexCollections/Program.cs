using AlexCollections;

AlexStackList<int> sl = new();

sl.Push(0);
sl.Push(1);
sl.Push(2);
sl.Push(3); 
sl.Push(4);
sl.Push(5);
sl.Push(6);
sl.Push(7);

foreach (int i in sl)
    Console.Write(i);

Console.WriteLine();
Console.WriteLine();

Console.WriteLine(sl.Pop());
Console.WriteLine(sl.Pop());