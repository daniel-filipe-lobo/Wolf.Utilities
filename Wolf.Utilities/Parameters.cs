namespace Wolf.Utilities
{
	public static class Parameters
	{
		public static string? Format((string ParameterName, object? ParameterValue)[] parametersInformation)
		{
			string? message = null;
			if (parametersInformation != null)
			{
				List<string> parameters = [];
				foreach (var parameterInformation in parametersInformation)
				{
					var json = JsonSerializer.Serialize(parameterInformation.ParameterValue).EscapeBraces();
					if (json.Length > 1024)
					{
						json = $"{json[..1024]}...";
					}
					parameters.Add($"{parameterInformation.ParameterName} = {json}");
				}
				message = $"Parameters: {string.Join(", ", parameters)}";
			}
			return message;
		}
	}
}
