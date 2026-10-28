namespace Lab3;

[System.Serializable]
public struct Toy
{
    private string _name;
    private double _price;
    private int _minAge;
    private int _maxAge;

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public double Price
    {
        get
        {
            return _price;
        }
        set
        {
            _price = value;
        }
    }

    public int MinAge
    {
        get
        {
            return _minAge;
        }
        set
        {
            _minAge = value;
        }
    }

    public int MaxAge
    {
        get
        {
            return _maxAge;
        }
        set
        {
            _maxAge = value;
        }
    }
}