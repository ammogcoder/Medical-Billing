using System.Windows;
namespace MedQuist.BillingAdmin.Presentation.CustomEventArgs
{
    public class RoutedContractEventArgs : RoutedEventArgs
    {
        public decimal? ContractId { get; private set; }

        public RoutedContractEventArgs(RoutedEvent routedEvent,decimal? contractId)
            : base(routedEvent)
        {
            this.ContractId = contractId;
        }
    }
}