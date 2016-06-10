namespace HappyCommon
{
    public interface IConfigManager
    {
        string GetConfigurationSetting(string configurationSettingName);
        string GetConfigurationSetting(string configurationSettingName, string defaultValue);


    }
}