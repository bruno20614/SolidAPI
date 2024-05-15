using Manager.API.ViewModels;

namespace Manager.API.Utilities;

public static class Response
{
    public static ResultViewModels ApplicationErrorMessage()
    {
        return new ResultViewModels
        {
            Message = "Ocorreu algum erro interno na aplicação "
            Success=false;
            Data=null;    
        };
    }

    public static ResultViewModels DomainErrorMessage(string message)
    {
        return new ResultViewModels
        {
            Message = message,
            Success=false,
            Data=null;
        };
    }
}