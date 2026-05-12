internal class RemoveCommand : ICommand {

	private const string HelpCommandArg = "remove-help";
	private const string DetailedHelpCommandArg = "remove-detailed";
	private const string UsageArg = "remove-usage";
	private const string InvalidIdArg = "remove-invalid-id";
	private const string ProductNotFoundArg = "remove-product-not-found";
	private const string RemovedArg = "remove-removed";
	private const string FailedArg = "remove-failed";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Remove product from database.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(UsageArg, "Not enough arguments. Use [{0} remove id]");
		dict.Add(InvalidIdArg, "Invalid product ID: {0}");
		dict.Add(ProductNotFoundArg, "Product with ID {0} not found.");
		dict.Add(RemovedArg, "Product removed successfully.");
		dict.Add(FailedArg, "Failed to remove product.");
	}

	public string GetKey() {
		return "remove";
	}

	public string GetHelp() {
		return Localization.Localize(HelpCommandArg);
	}

	public string? GetDetailedHelp() {
		return Localization.Localize(DetailedHelpCommandArg);
	}

	public string? Run(string[] args) {
		if (args.Length < 2)
			return Localization.Localize(UsageArg, Program.RootCommand);

		if (!int.TryParse(args[1], out int productId))
			return Localization.Localize(InvalidIdArg, args[1]);

		if (!Program.Database.TryFindProductById(productId, out _))
			return Localization.Localize(ProductNotFoundArg, productId);

		int result = Program.Database.DeleteProduct(productId);
		return Localization.Localize(result > 0 ? RemovedArg : FailedArg);
	}
}