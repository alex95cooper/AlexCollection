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

AlexLinkedList<int> newLinkedList = new();
newLinkedList.Add(3);
newLinkedList.Add(3);
newLinkedList.Add(3);
newLinkedList.Add(3);

linkedList.InsertRangeAfter(linkedList.Head, newLinkedList);

linkedList.InsertBefore(linkedList.Head, 4);


foreach (int i in linkedList)
    Console.Write(i);

Console.WriteLine();

Console.WriteLine(linkedList.Contains(9));
Console.WriteLine(linkedList.IndexOf(9));


