using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class BookingHelper_OverlappingBookingsExistTests
	{
		private Mock<IBookingRepository> repository;
		private Booking existingBooking;

		[SetUp]
		public void Setup()
		{
			repository = new Mock<IBookingRepository>();
			existingBooking = new Booking
			{
				Id = 2,
				Status = "Active",
				ArrivalDate = new DateTime(2020, 02, 01),
				DepartureDate = new DateTime(2020, 02, 10),
				Reference = "a"
			};
			repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
			{
				existingBooking
			}.AsQueryable());
		}

		[Test]
		public void PassCancelledBooking_ReturnsEmptyString()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = Before(existingBooking.ArrivalDate),
				DepartureDate = Before(existingBooking.ArrivalDate),
				Status = "Cancelled"
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = Before(existingBooking.ArrivalDate),
				DepartureDate = Before(existingBooking.ArrivalDate)
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void BookingStartsBeforeAndFinishesInTheMiddleAnExistingBooking_ReturnsExistingBookingReference()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = Before(existingBooking.ArrivalDate),
				DepartureDate = After(existingBooking.ArrivalDate)
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.EqualTo(existingBooking.Reference));
		}

		[Test]
		public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnsExistingBookingReference()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = Before(existingBooking.ArrivalDate),
				DepartureDate = After(existingBooking.DepartureDate)
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.EqualTo(existingBooking.Reference));
		}

		[Test]
		public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingReference()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = After(existingBooking.ArrivalDate),
				DepartureDate = Before(existingBooking.DepartureDate)
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.EqualTo(existingBooking.Reference));
		}

		[Test]
		public void BookingStartsInTheMiddleOfAnExistingBookingAndFinishesAfter_ReturnsExistingBookingReference()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = After(existingBooking.ArrivalDate),
				DepartureDate = After(existingBooking.DepartureDate)
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.EqualTo(existingBooking.Reference));
		}

		[Test]
		public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnsEmptyString()
		{
			var booking = new Booking
			{
				Id = 1,
				ArrivalDate = After(existingBooking.DepartureDate),
				DepartureDate = After(existingBooking.DepartureDate)
			};

			var result = BookingHelper.OverlappingBookingsExist(booking, repository.Object);

			Assert.That(result, Is.Empty);
		}

		private DateTime Before(DateTime date, int days = 1) => date.AddDays(-days);

		private DateTime After(DateTime date) => date.AddDays(1);
	}
}