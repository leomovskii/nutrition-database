using System.Text;

internal class FindCommand : ICommand {

	private const string HelpCommandArg = "find-help";
	private const string DetailedHelpCommandArg = "find-detailed";
	private const string UsageArg = "find-usage";
	private const string ProductNotFoundArg = "find-product-not-find";
	private const string FoundArg = "find-found";
	private const string AndMoreArg = "find-and-more";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Find product(s) by title.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(UsageArg, "Not enough arguments. Use [{0} find product name]");
		dict.Add(ProductNotFoundArg, "Product not found.");
		dict.Add(FoundArg, "Found {0} product(s):");
		dict.Add(AndMoreArg, "... and {0} more.");
	}
	public string GetKey() {
		return "find";
	}

	public string GetHelp() {
		return Localization.Localize(HelpCommandArg);
	}

	public string? GetDetailedHelp() {
		return Localization.Localize(DetailedHelpCommandArg);
	}

	public string? Run(string[] args) {
		if (args.Length == 1)
			return Localization.Localize(UsageArg, Program.RootCommand);

		var found = Program.Database.TryFindProducts(args[1..]);
		if (found.Length == 0)
			return Localization.Localize(ProductNotFoundArg);

		var sb = new StringBuilder();
		sb.Append(Localization.Localize(FoundArg, found.Length));

		int lenToShow = Math.Min(found.Length, 20);
		for (int i = 0; i < lenToShow; i++)
			sb.Append($"\n{found[i].ToString()}");

		int delta = found.Length - lenToShow;
		if (delta > 0)
			sb.Append($"\n{Localization.Localize(AndMoreArg, delta)}");

		return sb.ToString();
	}
}