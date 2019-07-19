# Stubble Extensions - String Formatter

This repository extends the mustache syntax to support string formatting by replacing the built in interpolation token renderer.

Example Usage:
```csharp

			using Stubble.Extensions.StringFormatter;
			
			....

			var data = new Dictionary<string, object> {
				{"number", 123456.7},
				{"date", DateTime.Now},
			};

			var stubble = new StubbleBuilder()
				.Configure(settings =>
				{
					settings.AddJsonNet();
					settings.SetFormattedInterpolationTokenRenderer();
				})
				.Build();

			using (var streamReader = new StringReader(@"number={{number:N2}} date={{date:d}} hour={{date:T}}"))
			{
				var output = stubble.Render(streamReader.ReadToEnd(), data);
			}

```
