internal class UpdateCommand : ICommand {

	public string Key => "update";
	public string Help => Localization.Localize(HelpCommandArg);
	public string? DetailedHelp => Localization.Localize(DetailedHelpCommandArg);

	public string? Run(string[] args) {
		if (args.Length < 2)
			return Localization.Localize(UsageArg, Program.RootCommand);

		if (!int.TryParse(args[1], out int productId))
			return Localization.Localize(InvalidIdArg, args[1]);

		if (!Program.Database.TryFindProductById(productId, out var existingProduct))
			return Localization.Localize(ProductNotFoundArg, productId);

		var updates = ProductInfo.ParseFromArgs(args[2..]);

		if (!string.IsNullOrWhiteSpace(updates.Title))
			existingProduct.Title = updates.Title;
		if (updates.Proteins.HasValue)
			existingProduct.Proteins = updates.Proteins;
		if (updates.Fats.HasValue)
			existingProduct.Fats = updates.Fats;
		if (updates.Carbs.HasValue)
			existingProduct.Carbs = updates.Carbs;
		if (updates.Calories.HasValue)
			existingProduct.Calories = updates.Calories;
		if (updates.Salt.HasValue)
			existingProduct.Salt = updates.Salt;

		int result = Program.Database.AddOrUpdateProduct(existingProduct);
		return Localization.Localize(result > 0 ? SuccessArg : FailArg);
	}

	#region Localization

	private const string HelpCommandArg = "update-help";
	private const string DetailedHelpCommandArg = "update-detailed";
	private const string UsageArg = "update-usage";
	private const string InvalidIdArg = "update-invalid-id";
	private const string ProductNotFoundArg = "update-product-not-found";
	private const string SuccessArg = "update-success";
	private const string FailArg = "update-fail";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Update product.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(UsageArg, "Not enough arguments. Use [{0} update id -t:\"product name\" -p:num ...]");
		dict.Add(InvalidIdArg, "Invalid product ID: {0}");
		dict.Add(ProductNotFoundArg, "Product with ID {0} not found.");
		dict.Add(SuccessArg, "Product updated successfully.");
		dict.Add(FailArg, "Failed to update product.");
	}

	#endregion
}