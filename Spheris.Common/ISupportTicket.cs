using System;
using System.Text;

namespace Spheris.Common
{
    public interface ISupportTicket
    {
        // Properties
        string Requester{get; set;}
        string EmailAddress{get; set;}
        string Category{get; set;}
        string Type{get; set;}
        string Item{get; set;}
        string OperationalStatus{get; set;}
        string Description {get; set;}
        string AssignedGroup{get; set;}
        string LogOnName { get; set;}
    
        // Methods
        string Create();    
    }
}
