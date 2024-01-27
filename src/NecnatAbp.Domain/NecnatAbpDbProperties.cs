namespace NecnatAbp;

public static class NecnatAbpDbProperties
{
    public static string DbTablePrefix { get; set; } = "NecnatAbp";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "NecnatAbp";
}
