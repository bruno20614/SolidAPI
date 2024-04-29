namespace Manager.Domain.Entities;
using fluentValidation;

public class UserValidador : AbstractValidator<User>
{
    public UserValidador()
    {
        RuleFor(x => x)
            .NotEmpt()
            .WithMessage("A entidade não pode ser vazia")

            .NotNull()
            .WithMessage("A entidade não pode ser nula");

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("O nome não pode ser vazio")

            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres")

            .MaximunLength(80)
            .WithMessage("O nome deve ter no máximo 80 caracteres");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("O email não pode ser nulo")

            .NotEmpty()
            .WithMessage("O email não pode ser vazio")

            .MinimumLength(10)
            .WithMessage("O email deve ter no mínimo 10 caracteres ")

            .MaximumLength(80)
            .WithMessage("O nome deve ter no máximo de 80 caracteres")

            .Matches( @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            );
            .WithMessage("O email informado não é valido");

            RuleFor(x => x.Password)
             .NotNull()
             .WithMessage("A sennha não pode ser nula")
                
             .NotEmpty()
             .WithMessage("A senha não pode ser vazia")
                
             .MinimumLength(10)
             .WithWithMessage("A senha deve ter no mínimo 10 caracteres")
                
             .MaximumLength(80)
             .WithMessage("A senha deve ter no máximo 80 caracteres")
    }
}