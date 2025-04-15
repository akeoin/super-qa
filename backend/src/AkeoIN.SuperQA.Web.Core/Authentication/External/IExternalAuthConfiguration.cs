using System.Collections.Generic;

namespace AkeoIN.SuperQA.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
