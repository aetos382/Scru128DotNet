using System.CommandLine;

using Scru128DotNet;

var countOption = new Option<int>("--count");

countOption.AddAlias("-n");
countOption.SetDefaultValue(1);
countOption.AddValidator(result =>
{
    int count = result.GetValueForOption(countOption);
    if (count < 1)
    {
        result.ErrorMessage = $"{countOption.Name} must be greater than 0.";
    }
});

var rootCommand = new RootCommand();
rootCommand.AddOption(countOption);

rootCommand.SetHandler(context =>
{
    int count = context.ParseResult.GetValueForOption(countOption);

    var console = context.Console;

    using var generator = new Scru128Generator();

    for (int i = 0; i < count; ++i)
    {
        string str = generator.Generate().ToString();
        console.WriteLine(str);
    }

});

return await rootCommand.InvokeAsync(args);
