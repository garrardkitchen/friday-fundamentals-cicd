using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class PolicyModel {

    public PolicyModel()
    {        
    }

    public PolicyModel(string policyId, string customerId, CoverTypes coverType, PeriodTypes periodType, int periodDuration)
    {
        PolicyId = policyId;
        CustomerId = customerId;
        CoverType = coverType;
        PeriodType = periodType;
        PeriodDuration = periodDuration;
    }

    [Key]
    public string? PolicyId { get; set; }
    [DisplayName("Customer Id")]
    public string? CustomerId { get; set; }
    [DisplayName("Cover")]
    public CoverTypes CoverType { get; set; }
    [DisplayName("Period")]
    public PeriodTypes PeriodType { get; set; }
    [DisplayName("Duration")]
    public int PeriodDuration { get; set; }
}






