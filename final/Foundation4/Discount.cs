public abstract class Discount
{
    protected string _name;
 
    public string Name { get { return _name; } }
 
    public Discount(string name)
    {
        _name = name;
    }
 
    // Same method name and signature for every discount
    public abstract decimal Apply(decimal price, int quantity);
 
    // Default description that child classes can replace.
    public virtual string GetDescription()
    {
        return _name;
    }
}