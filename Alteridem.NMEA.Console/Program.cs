using Alteridem.NMEA;
using Spectre.Console;

AnsiConsole.Clear();
AnsiConsole.Cursor.SetPosition(0, 4);
AnsiConsole.MarkupLine("[yellow]<ESC> to quit[/]");
AnsiConsole.Cursor.Hide();

NmeaListener listener = new("COM10");

listener.PositionChanged += (sender, args) =>
{
    AnsiConsole.Cursor.SetPosition(0, 0);
    AnsiConsole.MarkupLine($"[cyan]Position:[/] [green]{args.Latitude}, {args.Longitude}[/]");
    AnsiConsole.Cursor.Hide();
};

listener.Error += (sender, args) =>
{
    AnsiConsole.Cursor.SetPosition(0, 3);
    AnsiConsole.MarkupLine($"[red]Error: {args.Message}[/]");
    AnsiConsole.Cursor.Hide();
};

listener.Start();

while (true)
{
    if (Console.ReadKey().Key == ConsoleKey.Escape)
    {
        listener.Stop();
        break;
    }
}
