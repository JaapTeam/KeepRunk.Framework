using KeepRunk.Model.Core;

namespace KeepRunk.Dto.Core
{
    public interface IApplicationInputDto
    {
        IUserModel UserModel { get; set; }
    }
}