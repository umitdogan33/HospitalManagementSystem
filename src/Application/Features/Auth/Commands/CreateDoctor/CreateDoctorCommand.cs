using Application.Features.Auth.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateDoctor
{
    public class CreateDoctorCommand:IRequest<CreatedDoctorDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
    }
}
