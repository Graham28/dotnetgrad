using System;
namespace dotnetgrad
{
	public class Value
	{
		public double Data { get; }
		public List<Value> Children { get; }
		public Operation Operation { get; }
		public double Gradient { get; set; }

		public Value(double data, List<Value>? children = null,
			Operation operation = Operation.None, double gradient = 0)
		{
			Data = data;
			Children = children ?? new List<Value>();
			Operation = operation;
			Gradient = gradient; 
		}

        public Value Add(Value otherValue)
        {
			return new Value(Data + otherValue.Data, new List<Value>() { this, otherValue }, Operation.Add);
        }

        public Value Multiply(Value otherValue)
        {
            return new Value(Data * otherValue.Data, new List<Value>() { this, otherValue }, Operation.Multiply);
        }
        public Value TanH()
        {
			var n = Data;
			var tanH = (Math.Exp(2*n) - 1) / (Math.Exp(2*n) + 1);
            return new Value(tanH, new List<Value>() { this }, Operation.TanH);
        }

		public void Backword()
		{
            foreach (var child in Children)
            {
				if (this.Operation == Operation.Multiply)
				{
                    child.Gradient += Children.Where(x => !x.Equals(child)).First().Data * Gradient;
                }
				else
				{
                    child.Gradient += Gradient;
                }
                child.Backword();
            }

        }
    }
}

