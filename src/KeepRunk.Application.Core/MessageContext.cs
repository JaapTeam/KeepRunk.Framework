using KeepRunk.Model.Core;

namespace KeepRunk.Application.Core
{
    /// <summary>
    /// 会话信息上下文
    /// </summary>
    public class MessageContext
    {
        private readonly IUserModel _userModel;

        public MessageContext(IUserModel userModel)
        {
            _userModel = userModel;
        }

        public IUserModel UserModel
        {
            get
            {
                return _userModel; 
            }
        }
    }
}