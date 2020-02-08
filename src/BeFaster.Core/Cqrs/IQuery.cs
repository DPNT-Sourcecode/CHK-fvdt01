using MediatR;

namespace JATO.JaaS.Services.FeatureTemplateWIPService.Core
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}