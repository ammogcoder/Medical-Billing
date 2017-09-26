using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing
{
    public enum AddOnChargeTypes
    {
        TatReductionAppliedToWorkUnits = 14,
        ReversalOfTatCreditDueToExcessiveStats = 15,
        ReportsWithStatUrgencyRequested = 30,
        BillableUnitsWithStatUrgency = 31,
        BillableUnitsWithStatUrgencyOverAllowed = 32,
        TatReductionCalculated = 34,
        ChargeForExceedingMaxStatRequests = 42
    }
}
