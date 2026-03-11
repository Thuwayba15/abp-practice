using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AbpPractice.Localization;

public static class AbpPracticeLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(AbpPracticeConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(AbpPracticeLocalizationConfigurer).GetAssembly(),
                    "AbpPractice.Localization.SourceFiles"
                )
            )
        );
    }
}
