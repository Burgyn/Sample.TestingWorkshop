using System.Text.Json.Serialization;
using Kros.KORM;
using Kros.KORM.Converter;
using Kros.KORM.Metadata;
using Reservation.Domains.ValueObjects;

namespace Reservation.Domains.Infrastructure;

	public class DatabaseConfiguration : DatabaseConfigurationBase
	{
		public const string CompaniesTableName = "Companies";

		/// <summary>
		/// Create database model.
		/// </summary>
		/// <param name="modelBuilder"><see cref="ModelConfigurationBuilder"/></param>
		public override void OnModelCreating(ModelConfigurationBuilder modelBuilder)
		{
			modelBuilder.UseIdentifierDelimiters(Delimiters.SquareBrackets);

			CompanyTable(modelBuilder);
		}

		private static void CompanyTable(ModelConfigurationBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>()
				.HasTableName(CompaniesTableName)
				.HasPrimaryKey(f => f.Id)
				.Property(f => f.WebName).UseConverter<WebNameConverter>()
				.Property(f => f.DefaultReservationSettings).UseConverter<JsonConverter<ReservationSettings>>();
		}
	}