using System.Windows;
namespace MedQuist.BillingAdmin.Presentation.CustomEventArgs
{
    public class RoutedOverrideEventArgs : RoutedEventArgs
    {
        public int Defid { get; private set; }
        public string DefGrpName { get; private set; }

        public RoutedOverrideEventArgs(RoutedEvent routedEvent,int defId,string defGrpName)
            : base(routedEvent)
        {
            this.Defid = defId;
            this.DefGrpName = defGrpName;
        }
    }
}