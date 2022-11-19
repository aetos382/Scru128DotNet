using Scru128;

using var generator = new Scru128Generator();

bool stop = false;

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
