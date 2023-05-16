using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherCheck.Models
{
	public class Weather
	{
		[Key]
		public int Id { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Дата")]
		public DateTime Date { get; set; }
		[DataType(DataType.Time)]
		[Display(Name = "Время")]
		public DateTime Time { get; set; }
		[Display(Name = "Т")]
		public float? Temperature { get; set; }
		[Display(Name = "Отн.влажность, %")]
		[Range(0, 100)]
		public float? RelativeHumidity { get; set; }
		[Display(Name = "Td")]
		public float? DewPoint { get; set; }
		[Display(Name = "Атм.давление, мм рт.ст.")]
		public short? AtmosphericPressure { get; set; }

		[StringLength(50)]
		[Display(Name = "Направление ветра")]
		public string WindDirection { get; set; }
		[Display(Name = "Скорость ветра")]
		public byte? WindSpeed { get; set; }

		[Display(Name = "Облачность")]
		[Range(0, 100)]
		public byte? Cloudiness { get; set; }
		[Display(Name = "H")]
		public short? CloudCeiling { get; set; }

		[StringLength(50)]
		[Display(Name = "VV")]
		public string HorizontalVisibility { get; set; }
		[Display(Name = "Погодные явления")]
		[StringLength(500)]
		public string WeatherEvents { get; set; }

	}

	public enum Month
	{
		Choose = 0,
		January,
		February,
		March,
		April,
		May,
		June,
		July,
		August,
		September,
		October,
		November,
		December
	}
}
