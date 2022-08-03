using Kros.Extensions;
using Kros.Utils;
using Reservation.Domains.ValueObjects;

namespace Reservation.Domains;

/// <summary>
	/// Rule of close.
	/// </summary>
	public class RuleOfClose : EntityBase
	{
		private DateRange? _date;
		private DateRange? _easter;

		/// <summary>
		/// Gets or sets the company.
		/// </summary>
		public Company? Company { get; set; }

		/// <summary>
		/// Data range.
		/// </summary>
		public DateRange? Date
		{
			get
			{
				if (_date is null || !IsSystem)
				{
					return _date;
				}

				var now = DateOnly.FromDateTime(DateTimeProvider.UtcNow);
				if (Recurrence?.IsEaster == true)
				{
					return _easter ??= GetEasterDate(now);
				}

				return TransformHolidayToUpComingYear(_date.Value, now);
			}
			set => _date = value;
		}

		private static DateRange? TransformHolidayToUpComingYear(DateRange dateRange, DateOnly now)
		{
			var date = dateRange.WithYear(now.Year);
			return date.From < now ? date.AddYears(1) : date;
		}

		/// <summary>
		/// Time range;
		/// </summary>
		public TimeSlot? Time { get; set; }

		private DateRange GetEasterDate(DateOnly now)
		{
			DateRange GetEaster(int year)
				=> DateRange.Create(DateTimeExtensions.GetEaster(year).AddDays(Recurrence!.Interval!.Value));

			var easter = GetEaster(now.Year);
			return easter.From < now ? GetEaster(now.Year + 1) : easter;
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the closed services.
		/// </summary>
		/// <remarks>
		/// <see langword="null" /> or empty if all services are closed.
		/// </remarks>
		public IEnumerable<int>? ClosedServices { get; set; }

		/// <summary>
		/// Is system rule? (e.g.: holiday)
		/// </summary>
		public bool IsSystem { get; set; }

		/// <summary>
		/// Gets or sets the recurrence.
		/// </summary>
		/// <remarks>
		/// <see langword="null" /> if no recurrence is defined.
		/// </remarks>
		public Recurrence? Recurrence { get; set; }

		/// <summary>
		/// Determine if day is closed.
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public bool IsDayClosed(DateOnly date)
		{
			if (Date is null)
			{
				return false;
			}

			return Time is null && Date.Value.IsOverlap(date);
		}

		/// <summary>
		/// Is specific slot closed?
		/// </summary>
		public bool IsSlotClosed(DateOnly date, ITimeSlot slot)
		{
			if (IsDayClosed(date))
			{
				return true;
			}

			if (Time is null || date < Date?.From || date > Date?.To)
			{
				return false;
			}

			return Time.IsOverlap(slot, false);
		}
	}