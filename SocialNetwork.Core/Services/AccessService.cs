using Microsoft.AspNetCore.Identity;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Interfaces;
using System.Security.Claims;

namespace SocialNetwork.Core.Services;

public class AccessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;

    public AccessService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<User>();
    }

    public User? VerifyLogin(LoginDto loginDto)
    {
        var user = _userRepository.Find(x => x.UserName.Equals(loginDto.UserName)).FirstOrDefault();
        if (user != null)
        {
            if(VerifyPasswordHash(user, loginDto.Password))
                return user;
        }

        return null;
    }

    public async Task<User?> RegistrationAsync(RegistrationDto registrationDto)
    {
        var existUser = _userRepository.Find(x => x.UserName.Equals(registrationDto.UserName)).FirstOrDefault();
        if (existUser == null)
        {
            var user = new User()
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                Bio = registrationDto.Bio,
                Interests = registrationDto.Interests,
                BirthDate = registrationDto.BirthDate,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                PhoneNumber = registrationDto.PhoneNumber
            };
            user.PasswordHash = HashPassword(user, registrationDto.Password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }
        return null;
    }

    private string HashPassword(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        return passwordHasher.HashPassword(user, password);
    }

    private bool VerifyPasswordHash(User user, string providedPassword)
    {
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
        if (result == PasswordVerificationResult.Success)
            return true;

        return false;
    }

    public ClaimsIdentity CreateClaimsIdentity(string userId, string username, string? authenticationType)
    {
        var claims = new List<Claim>()
            {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username),
            };
        var claimsIdentity = new ClaimsIdentity(claims, authenticationType);

        return claimsIdentity;
    }
}
