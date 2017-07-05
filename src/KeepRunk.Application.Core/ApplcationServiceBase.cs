using KeepRunk.Dto.Core;

namespace KeepRunk.Application.Core
{
    public abstract class ApplcationServiceBase<TInput, TOutput> : IApplicationService<TInput, TOutput>
        where TInput : IApplicationInputDto
        where TOutput : IApplicationOutputDto
    {
        protected MessageContext MessageContext { get; private set; }

        public TOutput HandlerService(TInput input)
        {
            MessageContext = new MessageContext(input.UserModel);

            return ProcessService(input);
        }

        protected abstract TOutput ProcessService(TInput input);
    }
}