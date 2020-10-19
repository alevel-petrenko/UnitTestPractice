using System.Linq;

namespace TestNinja.Mocking
{
	public interface IBookingRepository
	{
		IQueryable<Booking> GetActiveBookings(int? cancelledId = null);
	}

	public class BookingRepository : IBookingRepository
	{
		public IQueryable<Booking> GetActiveBookings(int? cancelledId = null)
		{
			var unitOfWork = new UnitOfWork();

			IQueryable<Booking> bookings = unitOfWork.Query<Booking>()
									 .Where(b => b.Status != "Cancelled");

			if (cancelledId.HasValue)
				return bookings.Where(b => b.Id != cancelledId.Value);

			return bookings;
		}
	}
}