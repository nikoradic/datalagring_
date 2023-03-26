namespace EFC_.Models.Entities;

internal class CaseEntity
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public string Status { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;


}


