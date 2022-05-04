using AlexList;
using AlexLinkedList;

AlexList<int> alexList = new();
alexList.Add(1);
alexList.Add(7);
alexList.Add(5);
alexList.Add(2);
alexList.Add(9);
alexList.Add(3);
alexList.Add(6);
alexList.Add(4);
alexList.Add(8);
alexList.Add(10);
alexList.Add(11);
alexList.Add(12);
alexList.Add(13);
alexList.Add(14);
alexList.Add(15);
alexList.Add(16);
alexList.Add(17);
alexList.Add(18);
alexList.Add(19);
alexList.Add(20);

AlexList<string> alexListString = new();
alexListString.Add("one");
alexListString.Add("two");
alexListString.Add("three");

foreach (int i in alexList)
    Console.Write(i);

AlexList<int> alexListInt = new();
alexListInt.Add(0);
alexListInt.Add(1);
alexListInt.Add(1);
alexListInt.Add(1);
alexListInt.Add(2);
alexListInt.Add(2);
alexListInt.Add(2);

alexList.Sort();

alexList.InsertRange(6,alexListInt);

Console.WriteLine();

foreach (int i in alexList)
    Console.Write(i);

Console.WriteLine();
alexList.Sort();


Console.WriteLine($"Index of nine is {alexList.IndexOf(9)}");

Console.WriteLine();

foreach (int i in alexList)
    Console.Write(i);

Console.WriteLine();

Console.Write("BinarySearch (AlexList sort!!!) : ");
Console.Write(alexList.BinarySearch(1));
Console.Write(alexList.BinarySearch(2));
Console.Write(alexList.BinarySearch(3));
Console.Write(alexList.BinarySearch(4));
Console.Write(alexList.BinarySearch(5));
Console.Write(alexList.BinarySearch(6));
Console.Write(alexList.BinarySearch(7));
Console.Write(alexList.BinarySearch(8));
Console.Write(alexList.BinarySearch(9));
Console.Write(alexList.BinarySearch(10));
Console.Write(" ");
Console.Write(alexList.BinarySearch(11));
Console.Write(" ");
Console.Write(alexList.BinarySearch(12));
Console.Write(" ");
Console.Write(alexList.BinarySearch(13));
Console.Write(" ");
Console.Write(alexList.BinarySearch(14));
Console.Write(" ");
Console.Write(alexList.BinarySearch(15));
Console.Write(" ");
Console.Write(alexList.BinarySearch(16));
Console.Write(" ");
Console.Write(alexList.BinarySearch(17));
Console.Write(" ");
Console.Write(alexList.BinarySearch(18));
Console.Write(" ");
Console.Write(alexList.BinarySearch(19));
Console.Write(" ");
Console.Write(alexList.BinarySearch(20));

alexList.RemoveAt(3);

Console.WriteLine();

foreach (int i in alexList)
    Console.Write(i);

Console.WriteLine();

AlexList<Guy> guys = new();
guys.Add(new Guy { Name = "Alexey", Age = 26, Height = 175 });
guys.Add(new Guy { Name = "Sanek", Age = 45, Height = 179 });
guys.Add(new Guy { Name = "Evgeniy", Age = 36, Height = 196 });
guys.Add(new Guy { Name = "Yaroslav", Age = 13, Height = 174 });
guys.Add(new Guy { Name = "Oleg", Age = 20, Height = 173 });
guys.Add(new Guy { Name = "Andrey", Age = 55, Height = 174 });
guys.Add(new Guy { Name = "Roman", Age = 32, Height = 180 });
guys.Add(new Guy { Name = "Vetal", Age = 33, Height = 179 });

Console.WriteLine(guys.FindIndex(x => x.Name == "Oleg"));

guys.Insert(new Guy { Name = "Valera", Age = 31, Height = 181 }, 5);

Console.WriteLine();

foreach (Guy guy in guys)
    Console.WriteLine(guy.Name);

Console.ReadKey();

class Guy
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
}