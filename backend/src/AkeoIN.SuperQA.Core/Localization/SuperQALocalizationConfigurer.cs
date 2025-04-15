using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AkeoIN.SuperQA.Localization
{
    public static class SuperQALocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SuperQAConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SuperQALocalizationConfigurer).GetAssembly(),
                        "AkeoIN.SuperQA.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
