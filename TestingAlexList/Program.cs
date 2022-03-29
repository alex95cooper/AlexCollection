using AlexList;

AlexList<string> alexList = new();
alexList.Add("four");
alexList.Add("five");

foreach (string i in alexList)
    Console.WriteLine(i);

Console.ReadLine();
