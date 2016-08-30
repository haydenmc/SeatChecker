using System;

namespace SeatChecker.Models
{
    // <summary>
	/// Enum value used to represent section registration status / availability.
	/// </summary>
	public enum RegistrationStatus
	{
		NotAvailable,
		Closed,
		Open
	}

	/// <summary>
	/// Section model, representing the most granular order of class representation.
	/// </summary>
	public class Section
	{
		/// <summary>
		/// Unique ID with which to identify a section internally.
		/// </summary>
		public Guid SectionId { get; set; }

        /// <summary>
        /// Reference number used by Purdue to refer to sections.
        /// </summary>
		public string CRN { get; set; }

        /// <summary>
        /// ID of the class to which this section belongs.
        /// </summary>
        public Guid ClassId { get; set; }

		/// <summary>
		/// Type of section. e.g. Lecture, Laboratory, etc.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Indicates whether this section is available for registration.
		/// </summary>
		public RegistrationStatus RegistrationStatus { get; set; }
		
		/// <summary>
		/// Date this section begins (earliest meeting date).
		/// </summary>
		public DateTimeOffset StartDate { get; set; }

		/// <summary>
		/// Date this section ends (latest meeting date).
		/// </summary>
		public DateTimeOffset EndDate { get; set; }

		/// <summary>
		/// This section's attendance capacity.
		/// </summary>
		public int Capacity { get; set; }

		/// <summary>
		/// How many students are enrolled - referred to as 'Actual' by MyPurdue.
		/// </summary>
		public int Enrolled { get; set; }

		/// <summary>
		/// Remaining space for enrollment.
		/// </summary>
		public int RemainingSpace { get; set; }

		/// <summary>
		/// Wait list capacity.
		/// </summary>
		public int WaitlistCapacity { get; set; }

		/// <summary>
		/// How many students are on the wait list.
		/// </summary>
		public int WaitlistCount { get; set; }

		/// <summary>
		/// How much space is available on the wait list.
		/// </summary>
		public int WaitlistSpace { get; set; }
	}
}