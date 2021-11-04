using LibraryUsage.Mapper;

namespace LibraryUsage.Validators
{
    public interface IUserValidator
    {
        bool IsValid(CreateUser user);
    }
}
