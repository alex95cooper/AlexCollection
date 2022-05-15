using AlexCollections;

AlexLinkedList<int> linkedList = new();

linkedList.Add(5);
linkedList.Add(6);
linkedList.Add(7);
linkedList.Add(8);
linkedList.Add(9);

foreach (int i in linkedList)
    Console.Write(i);

Console.WriteLine();

linkedList.InsertRangeAfter(linkedList.First, 3, 3, 3, 3);

linkedList.InsertBefore(linkedList.First, 4);

foreach (int i in linkedList)
    Console.Write(i);

Console.WriteLine();

Console.WriteLine(linkedList.Contains(9));
Console.WriteLine(linkedList.IndexOf(9));


