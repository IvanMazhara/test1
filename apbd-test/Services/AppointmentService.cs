using Test1.Services;
using Microsoft.Data.SqlClient;
using Test1.Models.DTOs;

namespace Test1.Models;

public class AppointmentService : IAppointmentService
{
     private readonly IConfiguration _configuration;

     public AppointmentService(IConfiguration configuration)
     {
          _configuration = configuration;
     }

     public async Task<AppointmentDto> GetByIdAsync(int Id)
     {
          var query = @"
             SELECT a.Date, 
                    p.FirstName, p.LastName, p.DateOfBirth,
                    d.DoctorId, d.PWZ,
                    s.Name, s.ServiceFee
             FROM Appointment a
             JOIN Patient p ON a.PatientId = p.Id
             JOIN Doctor d ON a.DoctorId = d.Id
             LEFT JOIN Appointment_Service aps ON a.Id = aps.AppointmentId
             LEFT JOIN Service s ON aps.ServiceId = s.Id
             WHERE a.Id = @Id;";
          
          await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
          await using SqlCommand command = new SqlCommand(query,connection);
          
          command.Connection = connection;
          command.CommandText = query;
          command.Parameters.AddWithValue("@ID", Id);
        
          await connection.OpenAsync();
        
          var reader = await command.ExecuteReaderAsync();

          AppointmentDto appointment = new AppointmentDto();
          
          while (await reader.ReadAsync())
          {
               if (appointment == null)
               {
                    appointment = new AppointmentDto
                    {
                         Date = reader.GetDateTime(0),
                         Patient = new PatientDto
                         {
                              FirstName = reader.GetString(1),
                              LastName = reader.GetString(2),
                              DateOfBirth = reader.GetDateTime(3)
                         },
                         Doctor = new DoctorDto
                         {
                              DoctorId = reader.GetInt32(4),
                              PWZ = reader.GetString(5)
                         },
                         AppointmentServices = new List<ServiceDto>()
                    };
               }
          }

          return appointment;
     }
}