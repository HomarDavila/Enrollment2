using Domain.Custom_Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.API.Validations
{
    public class ApplicationPeopleRequestValidation : AbstractValidator<ApplicationPeopleRequest>
    {
        public ApplicationPeopleRequestValidation()
        {
            //RuleFor(c => c.Last4SSN).NotEmpty().When(c => string.IsNullOrEmpty(c.MPI)).WithMessage("No puede ser vacio si el MPI no ha sido ingresado");
            //RuleFor(c => c.DateOfBirth).NotEmpty().When(c => string.IsNullOrEmpty(c.MPI)).WithMessage("No puede ser vacio si el MPI no ha sido ingresado");
            //RuleFor(c => c.FirstName).NotEmpty().When(c => string.IsNullOrEmpty(c.MPI)).WithMessage("No puede ser vacio si el MPI no ha sido ingresado");
            //RuleFor(c => c.FirtLastName).NotEmpty().When(c => string.IsNullOrEmpty(c.MPI)).WithMessage("No puede ser vacio si el MPI no ha sido ingresado");

            //RuleFor(c => c.Last4SSN).Empty().When(c => !string.IsNullOrEmpty(c.MPI)).WithMessage("Tiene que ser vacio si el MPI ha sido ingresado");
            //RuleFor(c => c.DateOfBirth).Empty().When(c => !string.IsNullOrEmpty(c.MPI)).WithMessage("Tiene que ser vacio si el MPI ha sido ingresado");
            //RuleFor(c => c.FirstName).Empty().When(c => !string.IsNullOrEmpty(c.MPI)).WithMessage("Tiene que ser vacio si el MPI ha sido ingresado");
            //RuleFor(c => c.FirtLastName).Empty().When(c => !string.IsNullOrEmpty(c.MPI)).WithMessage("Tiene que ser vacio si el MPI ha sido ingresado");

            RuleFor(c => c.Last4SSN).Length(4).When(c => string.IsNullOrEmpty(c.MPI)).WithMessage("Deben ser solo 4 digitos");

        }
    }
}