namespace FinanceAccounting.DTO;

public class OperationDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int BankAccountId { get; set; }
    public int CategoryId { get; set; }
    
    public string Description { get; set; }
}