// dotnet add package Newtonsoft.Json
// dotnet add package Microsoft.Data.Sqlite
// dotnet add package e_sqlite3

using System.Globalization;
using System.Text;

internal class Program {

	public const string RootCommand = "ndb";
	public const string Version = "1.6";

	public readonly static CultureInfo? Culture = CultureInfo.InvariantCulture;
	public readonly static NumberStyles NumStyle = NumberStyles.Any;

	public readonly static Dictionary<string, ICommand> Commands = [];
	public static string? Help;

#pragma warning disable 8618
	public static IDatabase Database { get; private set; }
#pragma warning restore 8618

	private static void Main(string[] args) {
		Localization.Init();

		Database = new SQLiteDatabase();

		var sb = new StringBuilder();
		sb.Append(Localization.Localize(AvailableCommandsKey));

		RegisterCommand(new AddCommand());
		RegisterCommand(new FileCommand());
		RegisterCommand(new FindCommand());
		RegisterCommand(new HelpCommand());
		RegisterCommand(new RemoveCommand());
		RegisterCommand(new StatusCommand());
		RegisterCommand(new TodoCommand());
		RegisterCommand(new UpdateCommand());

		Help = sb.ToString();

		if (args == null || args.Length == 0) {
			Console.WriteLine(Localization.Localize(AboutKey, Version));
			return;
		}

		string cmd = args[0].ToLower();

		if (!Commands.TryGetValue(cmd, out var command)) {
			Console.WriteLine(Localization.Localize(UnknownCommandKey, cmd));
			return;
		}

		try {
			string? response = command.Run(args);
			if (!string.IsNullOrEmpty(response))
				Console.WriteLine($"{response}");

		} catch (Exception e) {
			Console.WriteLine(Localization.Localize(FailKey, e.Message));
		}

		void RegisterCommand(ICommand cmd) {
			string cmdKey = cmd.GetKey();
			if (Commands.ContainsKey(cmdKey))
				return;

			Commands.Add(cmdKey, cmd);
			sb.Append($"\n  {cmdKey} - {cmdKey}");
		}
	}

	#region Localization

	private const string AvailableCommandsKey = "available-commands";
	private const string AboutKey = "about";
	private const string UnknownCommandKey = "unknown-command";
	private const string FailKey = "fail-run-command";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(AvailableCommandsKey, "Available commands:");
		dict.Add(AboutKey, "Nutrition database v{0}. Use 'help' to see commands.");
		dict.Add(UnknownCommandKey, "Unknown command '{0}'.");
		dict.Add(FailKey, "Fail to run command. Message: {0}");
	}

	#endregion
}