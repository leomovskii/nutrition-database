internal class AddCommand : ICommand {

	private const string HelpCommandArg = "add-help";
	private const string DetailedHelpCommandArg = "add-detailed";
	private const string UsageArg = "add-usage";
	private const string TitleRequiredArg = "add-title-required";
	private const string AddedArg = "add-ok";
	private const string FailedArg = "add-fail";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Add new product.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(UsageArg, "Not enough arguments. Use [{0} add -t:\"product name\" -p:num -f:num -c:num -s:num -cal:num]");
		dict.Add(TitleRequiredArg, "Title is required (-t tag).");
		dict.Add(AddedArg, "Product added successfully.");
		dict.Add(FailedArg, "Failed to add product.");
	}

	public string GetKey() {
		return "add";
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

		var product = ProductInfo.ParseFromArgs(args[1..]);

		if (string.IsNullOrWhiteSpace(product.Title))
			return Localization.Localize(TitleRequiredArg);

		int result = Program.Database.AddOrUpdateProduct(product);

		return Localization.Localize(result > 0 ? AddedArg : FailedArg);
	}
}