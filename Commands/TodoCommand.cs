using System.Text;

internal class TodoCommand : ICommand {

	private const string HelpCommandArg = "todo-help";
	private const string DetailedHelpCommandArg = "todo-detailed";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Upcoming features list.");
		dict.Add(DetailedHelpCommandArg, "");
	}
	public string GetKey() {
		return "todo";
	}

	public string GetHelp() {
		return Localization.Localize(HelpCommandArg);
	}

	public string? GetDetailedHelp() {
		return Localization.Localize(DetailedHelpCommandArg);
	}

	public string? Run(string[] args) {
		var sb = new StringBuilder();
		sb.Append("Upcoming features:");
		sb.Append("\n* Backups: manual backup and backup behaviour;");
		sb.Append("\n* Brands: info about brands + filters;");
		sb.Append("\n* Export & Import: using json/xml/csv/xlsx formats;");
		sb.Append("\n* Languages: add ukrainian, deutsch, français, español, italiano, čeština.");
		return sb.ToString();
	}
}