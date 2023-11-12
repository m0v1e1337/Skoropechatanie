namespace Skoropechatanie;

public class KotuZajali
{
    public readonly string Name;
    public readonly double AmountPerMin;
    public readonly double AmountPerSec;

    public KotuZajali(string name, double amountPerMin, double amountPerSec)
    {
        Name = name;
        AmountPerMin = amountPerMin;
        AmountPerSec = amountPerSec;
    }
}