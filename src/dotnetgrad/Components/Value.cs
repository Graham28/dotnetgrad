using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dotnetgrad
{
	public class Value
	{
		public double Data { get; set; }
		public List<Value> Children { get; }
        public Operation Operation { get; }
		public double Gradient { get; set; }
        private Func<Value, double> _derivative;

        public Value(double data,
			List<Value>? children = null,
			Operation operation = Operation.None,
            Func<Value, double>? derivative = null,
            double gradient = 0)
		{
			Data = data;
			Children = children ?? new List<Value>();
			Operation = operation;
			Gradient = gradient;
            _derivative = derivative ?? (child => 1);
		}
        //For deserialisation
        public Value() { }

        public Value Add(Value otherValue)
        {
			return new Value(Data + otherValue.Data, new List<Value>() { this, otherValue }, Operation.Add, child => 1);
        }

        public Value Subtract(Value otherValue)
        {
            var thisData = this.Data;
            return new Value(Data - otherValue.Data, new List<Value>() { this, otherValue }, Operation.Subtract, child =>
                child.Data == thisData ?
                    1
                    :
                    -1
                );
        }

        public Value Multiply(Value otherValue)
        {
            var thisData = Data;
            return new Value(Data * otherValue.Data,
                new List<Value>() { this, otherValue },
                Operation.Multiply,
                child => thisData != child.Data ? thisData : otherValue.Data);
        }

        public Value Pow(Value otherValue)
        {
            var thisData = Data;
            var exp = Math.Pow(Data, otherValue.Data);
            return new Value(exp, new List<Value>() { this, otherValue }, Operation.Pow, child =>
            child.Data == thisData ?
                otherValue.Data * (Math.Pow(Data, otherValue.Data - 1))
                :
                Math.Pow(Data, otherValue.Data) * Math.Log(Data)
            );
        }

        public Value DevideBy(Value otherValue)
        {
            var thisData = Data;
            return new Value(thisData/otherValue.Data, new List<Value>() { this, otherValue }, Operation.Devide, child =>
            child.Data == thisData ?
                1 / otherValue.Data
                :
                -thisData / Math.Pow(otherValue.Data, 2)
            );
        }

        public Value TanH()
        {
			var n = Data;
			var tanH = (Math.Exp(2*n) - 1) / (Math.Exp(2*n) + 1);
            return new Value(tanH, new List<Value>() { this }, Operation.TanH, child => 1 - Math.Pow(tanH, 2));
        }

        public Value ReLU()
        {
            var thisData = Data;
            return new Value(thisData > 0 ? thisData : 0, new List<Value>() { this }, Operation.ReLU,
                child => thisData > 0 ? 1 : 0);
        }

        public Value lReLU()
        {
            var thisData = Data;
            return new Value(thisData > 0 ? thisData : 0.01 * thisData, new List<Value>() { this }, Operation.lReLU,
                child => thisData > 0 ? 1 : 0.01);
        }

        public Value Exp()
        {
            var n = Data;
            var exp = Math.Exp(n);
            return new Value(exp, new List<Value>() { this }, Operation.Exp, child => exp);
        }

        public void Backword()
		{
            var topologicallyOrderedListOfNodes = orderNodesTopologically(this, new HashSet<Value>(), new List<Value>());
            topologicallyOrderedListOfNodes.Reverse();
            foreach (var node in topologicallyOrderedListOfNodes)
            {
                node.SetChildGradients();
            }
        }

        public void SetChildGradients()
        {
            foreach (var child in Children)
            {
                var d = _derivative(child);
                child.Gradient += d * Gradient;
            }
        }

        private List<Value> orderNodesTopologically(Value node, HashSet<Value> visited, List<Value> returnList)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                foreach (var child in node.Children)
                {
                    orderNodesTopologically(child, visited, returnList);
                }
                returnList.Add(node);
            }
            return returnList;
        }

        public void Reset()
        {
            Children.RemoveAll(child => child != null);
            Gradient = 0.0;
        }
    }
}

