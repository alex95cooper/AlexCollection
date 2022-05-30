using AlexCollections;

AlexStack<int> sl = new();

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

Console.WriteLine();

AlexQueue<int> ql = new();

ql.Enqueue(0);
ql.Enqueue(1);
ql.Enqueue(2);
ql.Enqueue(3);
ql.Enqueue(4);
ql.Enqueue(5);
ql.Enqueue(6);
ql.Enqueue(7);

foreach (int i in ql)
    Console.Write(i);

Console.WriteLine();

Console.WriteLine(ql.Dequeue());
Console.WriteLine(ql.Dequeue());
Console.WriteLine(ql.Dequeue());
Console.WriteLine(ql.Dequeue());
Console.WriteLine(ql.Dequeue());
Console.WriteLine(ql.Dequeue());

ql.Enqueue(8);
ql.Enqueue(9);
ql.Enqueue(10);
ql.Enqueue(11);
ql.Enqueue(12);
ql.Enqueue(13);
ql.Enqueue(14);
ql.Enqueue(15);
ql.Enqueue(16);
ql.Enqueue(17);
ql.Enqueue(18);
ql.Enqueue(19);
ql.Enqueue(20);

Console.WriteLine();

foreach (int i in ql)
    Console.Write(i);

Console.WriteLine();


