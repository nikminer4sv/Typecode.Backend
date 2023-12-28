namespace Typecode.Domain;

public class FinishedTest : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TestId { get; set; }
    public int Time { get; set; }
    public double Accuracy { get; set; }
}