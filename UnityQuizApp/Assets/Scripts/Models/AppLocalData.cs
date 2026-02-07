namespace HyperCasualGame.Scripts.Models
{
    using HyperCasualGame.Scripts.Models.Controller;
    using Sirenix.Serialization;
    using Submodules.UITemplate.Scripts.Models.Core.Element;

    public class AppLocalData : UITemplateLocalData<AppLocalDataController>
    {
        [OdinSerialize] public bool IsFirstTimeOpenApp { get; set; } = true;
    }
}