using MediatR;

namespace JATO.JaaS.Services.FeatureTemplateWIPService.Core
{
    public interface IQueryHandler<in TParameter, TResponse>
        : IRequestHandler<TParameter, TResponse>
        where TResponse : IResult where TParameter : IQuery<TResponse>
    {
    }
}
