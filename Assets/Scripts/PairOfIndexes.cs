using System;
[System.Serializable]
 public struct PairOfIndexes
{
    public string ToString()
    {
        return i.ToString() + " " + j.ToString();
    }

    public int i, j;
    public PairOfIndexes(int i, int j)
    {
        this.i = i;
        this.j = j;
    }
    public PairOfIndexes(PairOfIndexes pair)
    {
        this.i = pair.i;
        this.j = pair.j;
    }
    
    public int MaxIndex()
    {
        if (i > j) return i;
        else return j;
    }

    public int AbsMaxIndex()
    {
        int a = Math.Abs(i), b = Math.Abs(j);
        if (a > b) return a;
        else return b;
    }

    static public PairOfIndexes None;

    static public PairOfIndexes right
    {
        get
        {
            return new PairOfIndexes(0, 1);
        }
    }
    static public PairOfIndexes left
    {
        get
        {
            return new PairOfIndexes(0, -1);
        }
    }
    static public PairOfIndexes up{
        get 
        { 
            return new PairOfIndexes(-1, 0); 
        }
    }
    static public PairOfIndexes down
    {
        get
        {
            return new PairOfIndexes(+1, 0);
        }
    }

    static public PairOfIndexes operator + (PairOfIndexes a, PairOfIndexes b) => new PairOfIndexes(a.i + b.i, a.j + b.j);
    static public PairOfIndexes operator - (PairOfIndexes a, PairOfIndexes b) => new PairOfIndexes(a.i - b.i, a.j - b.j);

    static public bool operator ==(PairOfIndexes a, PairOfIndexes b) => a.i == b.i && a.j == b.j;
    static public bool operator !=(PairOfIndexes a, PairOfIndexes b) => a.i != b.i || a.j != b.j;


    // Оператор умножения на любое число. Результат целое число. Уберается дробная часть
    static public PairOfIndexes operator *(PairOfIndexes a, float lambda) => new PairOfIndexes((int)(a.i * lambda), (int)(a.j * lambda));
    static public PairOfIndexes operator *(float lambda, PairOfIndexes a) => new PairOfIndexes((int)(a.i * lambda), (int)(a.j * lambda));

    // Оператор деления на любое число. Результат целое число. Уберается дробная часть
    static public PairOfIndexes operator /(PairOfIndexes a, float lambda) => new PairOfIndexes((int)(a.i / lambda), (int)(a.j / lambda));
}
