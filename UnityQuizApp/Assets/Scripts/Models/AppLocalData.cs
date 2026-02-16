namespace HyperCasualGame.Scripts.Models
{
    using System.Collections.Generic;
    using HyperCasualGame.Scripts.Models.Controller;
    using Sirenix.Serialization;
    using Submodules.UITemplate.Scripts.Models.Core.Element;

    public class AppLocalData : UITemplateLocalData<AppLocalDataController>
    {
        [OdinSerialize] public bool              IsFirstTimeOpenApp { get; set; } = true;

        // For demo!!!!!!!!!!!! dit me tu trinh sau khi test xong thi xoa di nhe, cai nay chi de demo thoi, khong phai de luu tren server hay gi ca
        [OdinSerialize] public List<AccountDemo> AccountDemos       { get; set; } = new();
    }

    public class AccountDemo
    {
        [OdinSerialize] public string Email    { get; set; }
        [OdinSerialize] public string Username { get; set; }
        [OdinSerialize] public string Password { get; set; }
    }
}