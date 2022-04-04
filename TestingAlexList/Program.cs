using AlexList;

List<int> vs = new List<int>() { 1, 7, 5, 2, 9, 3, 6, 4, 8 };

AlexList<int> alexList = new AlexList<int>();
alexList.Add(1);
alexList.Add(7);
alexList.Add(5);
alexList.Add(2);
alexList.Add(9);
alexList.Add(3);
alexList.Add(6);
alexList.Add(4);
alexList.Add(8);



Console.WriteLine("BinarySearch (List) :");
Console.WriteLine(vs.BinarySearch(1));
Console.WriteLine(vs.BinarySearch(2));
Console.WriteLine(vs.BinarySearch(3));
Console.WriteLine(vs.BinarySearch(4));
Console.WriteLine(vs.BinarySearch(5));
Console.WriteLine(vs.BinarySearch(6));
Console.WriteLine(vs.BinarySearch(7));
Console.WriteLine(vs.BinarySearch(8));
Console.WriteLine(vs.BinarySearch(9));


Console.WriteLine("BinarySearch (AlexList) :");
Console.WriteLine(alexList.BinarySearch(1));
Console.WriteLine(alexList.BinarySearch(2));
Console.WriteLine(alexList.BinarySearch(3));
Console.WriteLine(alexList.BinarySearch(4));
Console.WriteLine(alexList.BinarySearch(5));
Console.WriteLine(alexList.BinarySearch(6));
Console.WriteLine(alexList.BinarySearch(7));
Console.WriteLine(alexList.BinarySearch(8));
Console.WriteLine(alexList.BinarySearch(9));

alexList.Sort();

Console.WriteLine("BinarySearch (AlexList sort!!!) :");
Console.WriteLine(alexList.BinarySearch(1));
Console.WriteLine(alexList.BinarySearch(2));
Console.WriteLine(alexList.BinarySearch(3));
Console.WriteLine(alexList.BinarySearch(4));
Console.WriteLine(alexList.BinarySearch(5));
Console.WriteLine(alexList.BinarySearch(6));
Console.WriteLine(alexList.BinarySearch(7));
Console.WriteLine(alexList.BinarySearch(8));
Console.WriteLine(alexList.BinarySearch(9));



