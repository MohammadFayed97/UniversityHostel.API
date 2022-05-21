namespace CommonLibrary
{
    using System.Globalization;

    public class BaseSettingViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string NameSecondLanguage { get; set; }
        public string LocalizedName => CultureInfo.CurrentCulture.ThreeLetterISOLanguageName.Equals("ara") ? Name : NameSecondLanguage;

    }
}

