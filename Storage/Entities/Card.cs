using Storage.Enums;

namespace Storage.Entities;

public class Card
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Pan { get; set; }

    public CardTypes CardType { get; set; }

    public DateTime ExpirationDate { get; set; }

    public uint Cvv { get; set; }

    public int UserId { get; set; }

    public CurrencyAccounts? CurrencyAccounts { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
