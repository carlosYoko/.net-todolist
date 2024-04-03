using System.Data;
using FluentValidation;
using TodoList.DTOs;

namespace TodoList.Validators;

public class TodoInsertValidator : AbstractValidator<TodoInsertDto>
{
    public TodoInsertValidator()
    {
        RuleFor(x => x.ToDoName).NotEmpty().WithMessage("Nombre obligatorio");
        RuleFor(x => x.ToDoName).Length(2, 20).WithMessage("El nombre debe ser entre 2 y 20 caracateres");
        RuleFor(x => x.UserId).NotNull().WithMessage("El usuario es obligatorio");
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("Ingresa un número válido");
        RuleFor(x => x.IsDone).NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacía");
    }
}