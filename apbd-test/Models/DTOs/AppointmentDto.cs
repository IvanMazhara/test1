namespace Test1.Models.DTOs;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<ServiceDto> AppointmentServices  { get; set; }
    public DateTime Date { get; set; }
}