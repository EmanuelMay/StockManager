using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces.Repositories;
using StockManager.Domain.Interfaces.Services;

namespace StockManager.Application.Services;

public class UserService(
    IUserRepository repository,
    IEmailService emailService
) : IUserService
{
    public async Task<ResponseUserDTO> Add(CreateUserDTO userDTO)
    {
        if(await repository.EmailExists(userDTO.Email))
            throw new EmailAlreadyExistsException("email already exists");

        var user = new User(userDTO.Name, userDTO.Email, BCrypt.Net.BCrypt.HashPassword(userDTO.Password));

        await repository.Add(user);
        await repository.SaveChanges();

        return ToDTO(user);
    }

    public async Task<ResponseUserDTO> GetById(int id) 
        => ToDTO(await GetUserOrThrow(id));

    public async Task<IEnumerable<ResponseUserDTO>> GetAllUser()
    {
        var users = await repository.GetAll();

        return users.Select(u => ToDTO(u));
    }

    public async Task Remove(int id)
    {
        var user = await GetUserOrThrow(id);

        repository.Remove(user);
        await repository.SaveChanges();
    }

    public async Task<ResponseUserDTO> Update(int id, UpdateUserDTO userDTO)
    {
        var user = await GetUserOrThrow(id);

        if (await repository.EmailExists(userDTO.Email))
            throw new EmailAlreadyExistsException("email already exists");

        user.Update(userDTO.Name, userDTO.Email);
        await repository.SaveChanges();

        return ToDTO(user);
    }

    public async Task ForgotPassword(string email)
    {
        var user = await GetUserByEmailOrThrow(email);
        
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        await emailService.ResetPasswordEmail(code, user.Email, user.Name);

        var resetPassword = new UserResetPassword(email, code, user.Id);

        await repository.CreateResetPassword(resetPassword);
        await repository.SaveChanges();
    }

    public async Task ResetPassword(ResetPasswordUserDTO resetDTO)
    {
        PasswordValidation(resetDTO.Password);

        var user = await GetUserByEmailOrThrow(resetDTO.Email);
        var resetPassword = await repository.GetResetPasswordCode(resetDTO.Code, user.Id)
            ?? throw new InvalidCodeException("invalid code");
        
        user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(resetDTO.Password));
        resetPassword.Used();

        await repository.SaveChanges();
    }

    private void PasswordValidation(string password)
    {
        if (password.Length < 8)
            throw new ValidationException("password must contain at least 8 characters");
    }

    private async Task<User> GetUserByEmailOrThrow(string email)
    {
        var user = await repository.GetByEmail(email)
            ?? throw new InvalidCredentialsException("invalid credentials");
        return user;
    }

    private async Task<User> GetUserOrThrow(int id)
    {
        var user = await repository.GetById(id)
            ?? throw new UserNotFoundException("user not found");
        return user;
    }

    private static ResponseUserDTO ToDTO(User user)
    {
        return new ResponseUserDTO()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
