namespace Storage.Entities;

public class CurrencyAccounts
{
    [Key]
    public long Pan { get; set; }

    public uint? Rub { get; set; } = null;

    public uint? Usd { get; set; } = null;

    public uint? Eur { get; set; } = null;

    [ForeignKey(nameof(Pan))]
    public Card Card { get; set; }
}
