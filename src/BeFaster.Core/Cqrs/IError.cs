namespace JATO.JaaS.Services.FeatureTemplateWIPService.Core
{
    public interface IError
    {
        int Code { get; set; }
        string Message { get; set; }
    }
}