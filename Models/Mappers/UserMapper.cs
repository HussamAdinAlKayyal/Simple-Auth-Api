using BasicAuthApi.Models.Dtos;

namespace BasicAuthApi.Models.Mappers;

public static class UserMapper
{
    public static UserDetailsDto AsUserDetailsDto(this User user)
    {
        return new UserDetailsDto(
            user.Id,
            user.UserName!,
            user.FirstName,
            user.LastName,
            user.Email!);
    }
}
