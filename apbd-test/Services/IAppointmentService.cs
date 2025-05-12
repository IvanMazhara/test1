using Test1.Models.DTOs;

namespace Test1.Services;

public interface IAppointmentService
{
    Task<AppointmentDto> GetByIdAsync(int Id);
}