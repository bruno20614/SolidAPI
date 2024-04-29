namespace Manager.Domain.Entities;
using System;
public class User : Base
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    //EF
    protected user(){}
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        _errors = new List<string>();
    }

    public void ChangName(string name)
    {
        Name = name;
        Validade();
    }

    public void ChangePassword(string password)
    {
        Password = password;
        Validade();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        Validade();
    }

    public override bool Validade()
    {
        var validator = new UserValidador();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in valiador.Errors)
                _errors.Add(error.ErrorMessage);
            throw new Exception("Alguns campos estão inválidos,por favor corrija-os ", _errors[0]);
        }
    }
}