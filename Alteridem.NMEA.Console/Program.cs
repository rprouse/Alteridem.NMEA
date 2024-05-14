using Alteridem.NMEA;
using Spectre.Console;

AnsiConsole.Clear();
AnsiConsole.Cursor.SetPosition(0, 6);
AnsiConsole.MarkupLine("[yellow]<ESC> to quit[/]");
AnsiConsole.Cursor.Hide();

NmeaListener listener = new("COM10");

listener.Error += (sender, args) =>
{
    AnsiConsole.Cursor.SetPosition(0, 5);
    AnsiConsole.MarkupLine($"[red]Error: {args.Message}[/]");
    AnsiConsole.Cursor.Hide();
};

listener.PositionChanged += (sender, args) =>
{
    AnsiConsole.Cursor.SetPosition(0, 1);
    AnsiConsole.MarkupLine($"[cyan]Position:[/] [green]{args.Latitude}, {args.Longitude}[/]");
    AnsiConsole.Cursor.Hide();
};

listener.AltitudeChanged += (sender, args) =>
{
    AnsiConsole.Cursor.SetPosition(0, 2);
    AnsiConsole.MarkupLine($"[cyan]Altitude:[/] [green]{args.Altitude}m[/]   ");
    AnsiConsole.Cursor.Hide();
};

listener.TimeChanged += (sender, args) =>
{
    AnsiConsole.Cursor.SetPosition(0, 3);
    AnsiConsole.MarkupLine($"[cyan]Time:[/]     [green]{args.Time.ToLocalTime().ToString("T")}[/]             ");
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
