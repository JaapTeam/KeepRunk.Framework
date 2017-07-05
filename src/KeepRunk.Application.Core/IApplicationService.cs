using KeepRunk.Dto.Core;

namespace KeepRunk.Application.Core
{
    public interface IApplicationService<in TInput, out TOutput> 
        where TInput : IApplicationInputDto
        where TOutput:IApplicationOutputDto
    {
        TOutput HandlerService(TInput input);
    }
}