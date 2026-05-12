internal class ProductInfo {

	private readonly static string[] TitleArgs = ["-t", "-t:", "-title", "-title:"];
	private readonly static string[] ProteinsArgs = ["-p", "-p:", "-protein", "-protein:", "-proteins", "-proteins:"];
	private readonly static string[] FatsArgs = ["-f", "-f:", "-fat", "-fat:", "-fats", "-fats:"];
	private readonly static string[] CarbsArgs = ["-c", "-c:", "-carb", "-carb:", "-carbs", "-carbs:", "-carbohydrate", "-carbohydrate:", "-carbohydrates", "-carbohydrates:"];
	private readonly static string[] SaltArgs = ["-s", "-s:", "-salt", "-salt:", "-salts", "-salts:"];
	private readonly static string[] CaloriesArgs = ["-cc", "-cc:", "-cal", "-cal:", "-calories", "-calories:"];
	// private readonly static string[] BrandArgs = ["-b", "-b:", "-brand", "-brand:"];

	public int Id { get; set; }
	public required string Title { get; set; }
	// public string? Brand { get; set; }
	public double? Proteins { get; set; }
	public double? Fats { get; set; }
	public double? Carbs { get; set; }
	public double? Calories { get; set; }
	public double? Salt { get; set; }
	public DateTime LastUpdated { get; set; }

	public double? CaloriesKj => Calories.HasValue ? Utils.ConvertCcalToKj(Calories.Value) : null;

	public override string ToString() {
		return $"{Id} {Title} (PFC {Utils.DoubleToString(Proteins)}/{Utils.DoubleToString(Fats)}/{Utils.DoubleToString(Carbs)} g, {Utils.DoubleToString(Calories)} kcal)";
	}

	public static ProductInfo ParseFromArgs(string[] args) {
		var product = new ProductInfo { Title = "", Id = -1, LastUpdated = DateTime.Now };

		foreach (var arg in args) {
			if (Utils.CompateArg(arg, out string value, TitleArgs)) {
				if (value.Length >= 2 && value[0] == '"' && value[^1] == '"')
					value = value[1..^1];
				product.Title = value;

			} else if (Utils.CompateArg(arg, out value, ProteinsArgs)) {
				if (double.TryParse(value, Program.NumStyle, Program.Culture, out double proteins))
					product.Proteins = proteins;

			} else if (Utils.CompateArg(arg, out value, FatsArgs)) {
				if (double.TryParse(value, Program.NumStyle, Program.Culture, out double fats))
					product.Fats = fats;

			} else if (Utils.CompateArg(arg, out value, CarbsArgs)) {
				if (double.TryParse(value, Program.NumStyle, Program.Culture, out double carbs))
					product.Carbs = carbs;

			} else if (Utils.CompateArg(arg, out value, SaltArgs)) {
				if (double.TryParse(value, Program.NumStyle, Program.Culture, out double salt))
					product.Salt = salt;

			} else if (Utils.CompateArg(arg, out value, CaloriesArgs)) {
				if (double.TryParse(value, Program.NumStyle, Program.Culture, out double calories))
					product.Calories = calories;
			}
		}

		return product;
	}
}