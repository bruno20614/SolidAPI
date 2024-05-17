using System.Collections.Generic;
using Manager.API.ViewModels;

namespace Manager.API.Utilities;
    public static class Response{
        public static ResultViewModels ApplicationErrorMessage()
        {
            return new ResultViewModels()
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModels DomainErrorMessage(string message)
        {
            return new ResultViewModels()
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModels DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModels
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }

        public static ResultViewModels UnauthorizedErrorMessage()
        {
            return new ResultViewModels
            {
                Message = "A combinação de login e senha está incorreta!",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModels InternalServerErrorMessage()
        {
            return new ResultViewModels
            {
                Message = "Ocorreu um erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Data = null
            };
        }
    }