namespace Core.enums
{
    public enum AppointmentStatus
    {
        Pending,    // Appointment is scheduled but not yet confirmed
        Confirmed,  // Appointment is confirmed by the user
        Cancelled,  // Appointment is cancelled by the user or system
        Completed
    }
}
