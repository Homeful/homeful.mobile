using System;
namespace Homeful.mobile
{
    public class CampDetailViewModel : CampBaseViewModel
    {
        public Camp Camp { get; set; }
        public CampDetailViewModel(Camp camp = null)
        {
            Title = camp?.Name;
            Camp = camp;
        }
    }
}
