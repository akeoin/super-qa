using AkeoIN.SuperQA.Debugging;

namespace AkeoIN.SuperQA
{
    public class SuperQAConsts
    {
        public const string LocalizationSourceName = "SuperQA";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "b322dc3470e74a738653ae512ebcc87b";
    }
}
