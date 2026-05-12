internal class HelpCommand : ICommand {

	private const string HelpCommandArg = "help-help";
	private const string DetailedHelpCommandArg = "help-detailed";
	private const string CommandNotFoundArg = "help-command-not-found";
	private const string CommandNameNotFoundArg = "help-command-name-not-found";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Show all commands.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(CommandNotFoundArg, "Command not found.");
		dict.Add(CommandNameNotFoundArg, "Command {0} not found.");
	}

	public string GetKey() {
		return "help";
	}

	public string GetHelp() {
		return Localization.Localize(HelpCommandArg);
	}

	public string? GetDetailedHelp() {
		return Localization.Localize(DetailedHelpCommandArg);
	}

	public string? Run(string[] args) {
		if (args.Length == 1)
			return Program.Help ?? Localization.Localize(CommandNotFoundArg);

		string cmd = args[1].ToLower();
		if (!Program.Commands.TryGetValue(cmd, out var command))
			return Localization.Localize(CommandNameNotFoundArg, args[1]);

		return command.GetDetailedHelp() ?? command.GetHelp();
	}
}