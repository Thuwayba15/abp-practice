using AbpPractice.Debugging;

namespace AbpPractice;

public class AbpPracticeConsts
{
    public const string LocalizationSourceName = "AbpPractice";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "aa3ad87bca894d7fae50264ad4f9f9b3";
}
