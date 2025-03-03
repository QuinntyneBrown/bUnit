namespace Bunit.TestAssets.SampleComponents
{
	public partial class PersistentComponentStateSample
	{
		public const string PersistanceKey = "fetchdata";

		public WeatherForecast[] Forecasts { get; private set; }

#if NET6_0_OR_GREATER
		[Inject] public PersistentComponentState State { get; set; }

		protected override void OnInitialized()
		{
			State.RegisterOnPersisting(PersistForecasts);
			if (!State.TryTakeFromJson<WeatherForecast[]>(PersistanceKey, out var data))
			{
				Forecasts = CreateForecasts();
			}
			else
			{
				Forecasts = data;
			}
		}

		private Task PersistForecasts()
		{
			State.PersistAsJson(PersistanceKey, Forecasts);
			return Task.CompletedTask;
		}

		private WeatherForecast[] CreateForecasts()
		{
			return new WeatherForecast[]
			{
			new WeatherForecast{ Temperature = 42 },
			};
		}
#endif
	}

	public record class WeatherForecast
	{
		public int Temperature { get; set; }
	}
}
