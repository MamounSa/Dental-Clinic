public class AppointmentStatusUpdater : IAppointmentStatusUpdater
{
    private readonly AppDbContext _context;

    public AppointmentStatusUpdater(AppDbContext context)
    {
        _context = context;
    }

    public async Task UpdateExpiredAppointmentsAsync()
    {
        var now = DateTime.Now;

        await _context.Appointments
       .Where(a => a.Start <= now && a.Status == "مؤكد")
       .ExecuteUpdateAsync(setters => setters
       .SetProperty(a => a.Status, "لم يحضر"));
    }
}