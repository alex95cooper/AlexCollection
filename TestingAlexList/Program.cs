using AlexList;

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

foreach (int i in alexList)
    Console.Write(i);

alexList.Sort();

Console.WriteLine();

Console.WriteLine($"Index of nine is {alexList.IndexOf(9)}");

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