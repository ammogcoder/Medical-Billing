using System;




/// <summary>
/// Base class for all WPF objects
/// It provides support for property change notifications 
/// and has a DisplayName property.  This class is abstract.
/// </summary>
/// 

public interface IGetValidationError
{
    string GetValidationError(string propertyName);
}
