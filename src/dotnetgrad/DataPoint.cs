using System;
namespace dotnetgrad
{
	public class DataPoint
	{
		public List<double> Input { get; init; }
		public List<double> Target { get; init; }

		public DataPoint(List<double> input, List<double> target)
		{
			Input = input;
			Target = target;
		}
	}
}

