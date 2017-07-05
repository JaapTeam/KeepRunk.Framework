namespace KeepRunk.Model.Core
{
    public interface IUserModel : IRootEntity
    {
        long UserId { get; set; }
        string UserName { get; set; }
    }
}