using Kros.Extensions;

namespace Reservation.Domains.ValueObjects;

/// <summary>
	/// Recurrence definition.
	/// </summary>
	public record Recurrence
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public RecurrenceType Type { get; init; }

		/// <summary>
		/// Gets or sets the interval of custom recurrence. (Only if <see cref="Type"/> is <see cref="RecurrenceType.Custom"/>.)
		/// </summary>
		public int? Interval { get; init; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// (Only if <see cref="Type"/> is <see cref="RecurrenceType.Custom"/>.)
		/// </summary>
		public RecurrenceIntervalType? IntervalType { get; init; }

		/// <summary>
		/// Gets or sets the week days.
		/// (Only if <see cref="Type"/> is <see cref="RecurrenceType.Custom"/>.)
		/// (Only if <see cref="IntervalType"/> is <see cref="RecurrenceType.Weekly"/> or <see cref="RecurrenceType.Monthly"/> .)
		/// </summary>
		public IEnumerable<DayOfWeek> WeekDays { get; init; }

		/// <summary>
		/// IsEaster
		/// </summary>
		public bool IsEaster => Type == RecurrenceType.Custom && IntervalType == RecurrenceIntervalType.Easter;

		internal bool Validate()
		{
			string Message(string property, string condition)
				=> "'{0}' property can be set only if '{1}'.".Format(property, condition);

			if (Interval.HasValue && Type != RecurrenceType.Custom)
			{
				throw new BusinessRuleValidationException(
					Message(nameof(Interval), $"{nameof(Type)} == {nameof(RecurrenceType.Custom)}"));
			}

			if (IntervalType.HasValue && Type != RecurrenceType.Custom)
			{
				throw new BusinessRuleValidationException(
					Message(nameof(IntervalType), $"{nameof(Type)} == {nameof(RecurrenceType.Custom)}"));
			}

			if (WeekDays?.Any() == true && !(Type == RecurrenceType.Custom && IntervalType == RecurrenceIntervalType.Weeks))
			{
				throw new BusinessRuleValidationException(
					Message(nameof(WeekDays),
						$"{nameof(Type)} == {nameof(RecurrenceType.Custom)} && {nameof(IntervalType)} == {nameof(RecurrenceIntervalType.Weeks)}"));
			}

			return true;
		}
	}