using System.Text;

internal class TodoCommand : ICommand {

	public string Key => "todo";
	public string Help => Localization.Localize(HelpCommandArg);
	public string? DetailedHelp => Localization.Localize(DetailedHelpCommandArg);

	public string? Run(string[] args) {
		var sb = new StringBuilder();
		sb.Append("Upcoming features:");
		sb.Append("\n* Backups: manual backup and backup behaviour;");
		sb.Append("\n* Brands: info about brands + filters;");
		sb.Append("\n* Export & Import: using json/xml/csv/xlsx formats;");
		sb.Append("\n* Languages: add ukrainian, deutsch, français, español, italiano, čeština.");
		return sb.ToString();
	}

	#region Localization

	private const string HelpCommandArg = "todo-help";
	private const string DetailedHelpCommandArg = "todo-detailed";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Upcoming features list.");
		dict.Add(DetailedHelpCommandArg, "");
	}

	#endregion
}