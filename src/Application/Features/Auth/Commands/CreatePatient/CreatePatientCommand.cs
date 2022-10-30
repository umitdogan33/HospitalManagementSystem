using Application.Features.Auth.Dtos;
using MediatR;

namespace Application.Features.Auth.Commands.CreatePatient;

public class CreatePatientCommand : IRequest<CreatedUserDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
