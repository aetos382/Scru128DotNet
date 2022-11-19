using Scru128DotNet;

using var generator = new Scru128Generator();

var stop = false;

Console.CancelKeyPress += (s, e) =>
{
    stop = true;
    e.Cancel = true;
};

while (!stop)
{
    var id = generator.Generate(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    Console.WriteLine(id.ToString());
}

return 0;
