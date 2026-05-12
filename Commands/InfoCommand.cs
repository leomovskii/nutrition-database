using System.Text;

internal class InfoCommand : ICommand {

	public string Key => "info";
	public string Help => Localization.Localize(HelpCommandArg);
	public string? DetailedHelp => Localization.Localize(DetailedHelpCommandArg);

	public string? Run(string[] args) {
		if (args.Length < 2)
			return Localization.Localize(UsageArg, Program.RootCommand);

		if (!int.TryParse(args[1], out int productId))
			return Localization.Localize(InvalidIdArg, args[1]);

		if (!Program.Database.TryFindProductById(productId, out var product))
			return Localization.Localize(ProductNotFoundArg, productId);

		var sb = new StringBuilder();
		sb.Append(Localization.Localize(MessageArg, productId));
		sb.Append($"\n  {Localization.Localize(TitleArg, product.Title)}");
		sb.Append($"\n  {Localization.Localize(ProteinsArg, Utils.DoubleToString(product.Proteins))}");
		sb.Append($"\n  {Localization.Localize(FatsArg, Utils.DoubleToString(product.Fats))}");
		sb.Append($"\n  {Localization.Localize(CarbsArg, Utils.DoubleToString(product.Carbs))}");
		sb.Append($"\n  {Localization.Localize(CaloriesArg, Utils.DoubleToString(product.Calories), Utils.DoubleToString(product.CaloriesKj))}");
		sb.Append($"\n  {Localization.Localize(SaltArg, Utils.DoubleToString(product.Salt))}");
		sb.Append($"\n  {Localization.Localize(TimestampArg, Utils.DateToStr(product.LastUpdated))}");

		return sb.ToString();
	}

	#region Localization

	private const string HelpCommandArg = "info-help";
	private const string DetailedHelpCommandArg = "info-detailed";
	private const string UsageArg = "info-usage";
	private const string InvalidIdArg = "info-invalid-id";
	private const string ProductNotFoundArg = "info-product-not-found";
	private const string MessageArg = "info-message";

	private const string TitleArg = "info-title";
	private const string ProteinsArg = "info-proteins";
	private const string FatsArg = "info-fats";
	private const string CarbsArg = "info-carbs";
	private const string CaloriesArg = "info-calories";
	private const string SaltArg = "info-salt";
	private const string TimestampArg = "info-timestamp";

	public static void FillLocalization(Dictionary<string, string> dict) {
		dict.Add(HelpCommandArg, "Show info about product.");
		dict.Add(DetailedHelpCommandArg, "");
		dict.Add(UsageArg, "Not enough arguments. Use [{0} info <num>]");
		dict.Add(InvalidIdArg, "Invalid product ID: {0}");
		dict.Add(ProductNotFoundArg, "Product with ID {0} not found.");
		dict.Add(MessageArg, "Product Info (ID: {0}):");

		dict.Add(TitleArg, "Title: {0}");
		dict.Add(ProteinsArg, "Proteins: {0} g");
		dict.Add(FatsArg, "Fats: {0} g");
		dict.Add(CarbsArg, "Carbs: {0} g");
		dict.Add(CaloriesArg, "Calories: {0} kcal ({1} kj)");
		dict.Add(SaltArg, "Salt: {0} g");
		dict.Add(TimestampArg, "Last Updated: {0}");
	}

	#endregion
}