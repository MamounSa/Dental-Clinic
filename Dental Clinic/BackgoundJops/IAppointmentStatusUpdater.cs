public interface IAppointmentStatusUpdater
{
    Task UpdateExpiredAppointmentsAsync();
}