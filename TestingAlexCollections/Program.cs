using AlexCollections;

//---------------Using String Key----------------------//
Console.WriteLine("<--------------Use string------------->");
AlexDictionary<string, string> dictionaty = new() { { "One", "hello " }, { "Two", "Its " }, { "Three", "me" }, { "Four", ", " }, { "Five", "Mario" } };

foreach (var item in dictionaty)
    Console.Write(item.Value);

Console.WriteLine();

var mykeylist = dictionaty.Keys;

foreach (var key in mykeylist)
    Console.Write(key);

Console.WriteLine();
Console.WriteLine(dictionaty["Five"]);

Console.WriteLine();

if (!dictionaty.TryGetValue("Six", out string name))
    Console.WriteLine("Im not exist" + name);

Console.WriteLine(dictionaty.ContainsValue("Mario"));

Console.WriteLine();

foreach (var key in mykeylist)
{
    Console.Write(key);

    if (key == "Tree")
    {
        dictionaty.Remove(key);
    }
}
Console.WriteLine();
Console.WriteLine();

//-------------------Using StructKey----------------------//
Console.WriteLine("<--------------Use Struct Key------------->");

AlexDictionary<StructKey, string> structDictionaty = new();

structDictionaty.Add(new StructKey(1, 2, 3), "hello ");
structDictionaty.Add(new StructKey(2, 4, 8), "Its ");
structDictionaty.Add(new StructKey(3, 9, 27), "me");
structDictionaty.Add(new StructKey(4, 16, 64), ", ");
structDictionaty.Add(new StructKey(5, 25, 125), "Mario");

foreach (var item in structDictionaty)
    Console.Write(item.Value);

Console.WriteLine();

var myStructKeylist = structDictionaty.Keys;

foreach (var key in myStructKeylist)
    Console.WriteLine(key.Number);

Console.WriteLine();
Console.WriteLine(structDictionaty[new StructKey(5, 25, 125)]);

Console.WriteLine();

if (structDictionaty.TryGetValue(new StructKey(5, 25, 125), out string nameTwo))
    Console.WriteLine("Im exist, my name is " + nameTwo);

Console.WriteLine(structDictionaty.ContainsValue("Mario"));

Console.WriteLine();

foreach (StructKey key in myStructKeylist)
{
    Console.Write(key.Number);
}

Console.WriteLine();
Console.WriteLine();

//-------------------Using ClassKey----------------------//
Console.WriteLine("<--------------Use Struct Key------------->");
MyComparer myComparer = new();
AlexDictionary<ClassKey, string> classDictionaty = new(myComparer);

classDictionaty.Add(new ClassKey('A', 'B', 'C'), "hello ");
classDictionaty.Add(new ClassKey('A', 'C', 'D'), "Its ");
classDictionaty.Add(new ClassKey('B', 'C', 'D'), "me");
classDictionaty.Add(new ClassKey('B', 'D', 'E'), ", ");
classDictionaty.Add(new ClassKey('C', 'D', 'E'), "Mario");

foreach (var item in classDictionaty)
    Console.Write(item.Value);

Console.WriteLine();

var myClassKeylist = classDictionaty.Keys;

foreach (var key in myClassKeylist)
    Console.WriteLine(key.Word);

Console.WriteLine();
Console.WriteLine(classDictionaty[new ClassKey('C', 'D', 'E')]);

Console.WriteLine();

if (classDictionaty.TryGetValue(new ClassKey('C', 'D', 'E'), out string nameThree))
    Console.WriteLine("Im exist, my name is " + nameThree);

Console.WriteLine(classDictionaty.ContainsValue("Mario"));

Console.WriteLine();

//!!!!!!!!!!!!!!!!!!!!!!!!!!!---------------Testting ObservableCillection----------------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!---------------Testing ObservableCillection----------------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
Console.WriteLine();
AlexObservableCollection<int> oc = new AlexObservableCollection<int>();

oc.CollectionChanged += OcHandler;

oc.Add(5);
oc.Add(7);
oc.Insert(1, 6);
oc.Clear();


void OcHandler(object sender, CollectionChangedEventArgs<int> e)
{
    if (e.NewValues == null && e.OldValues.Count > 1)
    {
        Console.Write($"e --> Action: {e.Action}, NewValue: null, OldValues: ");
        foreach (var item in e.OldValues)   
            Console.Write(item);
        Console.Write($", NewIndex: { e.NewIndex}, OldIndex: { e.OldIndex}.");
    } 
    else if (e.NewValues == null && e.OldValues != null)
    {
        Console.WriteLine($"e --> Action: {e.Action}, NewValue: null, OldValue: {e.OldValues}, NewIndex: {e.NewIndex}, OldIndex: {e.OldIndex}.");
    }
    else if (e.NewValues != null && e.OldValues == null)
    {
        Console.WriteLine($"e --> Action: {e.Action}, NewValue: {e.NewValues[0]}, OldValue: null, NewIndex: {e.NewIndex}, OldIndex: {e.OldIndex}.");
    }
    else if (e.NewValues != null && e.OldValues != null)
    {
        Console.WriteLine($"e --> Action: {e.Action}, NewValue: {e.NewValues[0]}, OldValue: {e.OldValues}, NewIndex: {e.NewIndex}, OldIndex: {e.OldIndex}.");
    }

    Console.WriteLine();
}




public struct StructKey
{
    private readonly int _firstNumber;
    private readonly int _secondNumber;
    private readonly int _lastNumber;

    public StructKey(int firstNumber, int secondNumber, int lastNumber)
    {
        _firstNumber = firstNumber;
        _secondNumber = secondNumber;
        _lastNumber = lastNumber;
    }

    public string Number => _firstNumber.ToString() + "_" + _secondNumber.ToString() + "_" + _lastNumber.ToString();
}

public class ClassKey
{
    private readonly string _firstLetter;
    private readonly string _secondLetter;
    private readonly string _lastLetter;

    public ClassKey(char firstFigure, char secondFigure, char lastFigure)
    {
        _firstLetter = firstFigure.ToString();
        _secondLetter = secondFigure.ToString();
        _lastLetter = lastFigure.ToString();
    }

    public string FirstLetter => _firstLetter;
    public string SecondLetter => _secondLetter;
    public string LastLetter => _lastLetter;
    public string Word => _firstLetter + _secondLetter + _lastLetter;

    public bool Equals(ClassKey classKey)
    {
        return classKey.FirstLetter == FirstLetter
            && classKey.SecondLetter == SecondLetter
            && classKey.LastLetter == LastLetter;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((ClassKey)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstLetter, SecondLetter, LastLetter);
    }
}

public class MyComparer : IAlexComparer<ClassKey>
{
    public int Compare(ClassKey x, ClassKey y)
    {
        if (x == null || y == null)
        {
            return CompareNullReference(x, y);
        }
        if (x.Equals(y))
        {
            return 0;
        }

        return -1;
    }

    private static int CompareNullReference(ClassKey? x, ClassKey? y)
    {
        if (x == null && y == null)
            return 0;
        else if (x == null)
            return -1;
        else
            return 1;
    }
}




