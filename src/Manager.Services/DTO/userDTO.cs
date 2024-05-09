using System;

namespace Manager.Services.DTO;
    public class UserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long Id { get; set; } 

        public UserDTO()
        {
            
        }

        public UserDTO(long id, string name, string password, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }