using TextFirstUniqueCharSelector.Logic;



var textProcessor = new CharFromTextSelector();
string? line = Console.ReadLine();

while (!string.IsNullOrEmpty(line))
{
    textProcessor.AddText(line);
    line = Console.ReadLine();
}

if (textProcessor.TryGetResult(out var result))
{
    Console.WriteLine($"Result is \"{result}\".");
}
else
{
    Console.WriteLine("Result is not exists.");
}