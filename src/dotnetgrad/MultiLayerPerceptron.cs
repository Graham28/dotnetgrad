using System;
namespace dotnetgrad
{
	public class MultiLayerPerceptron
	{
		public List<ILayer> Layers { get; private set; }
		public double TrainingRate { get; set; }
		public MultiLayerPerceptron(
			int numInputs, int numHiddenLayers, int sizeOfHiddenLayers, int numOutputs)
		{
			Layers = new List<ILayer>();
			Layers.Add(new Layer(numInputs, sizeOfHiddenLayers));
			for (int i = 1; i < numHiddenLayers; i++)
				Layers.Add(new Layer(sizeOfHiddenLayers, sizeOfHiddenLayers));
			Layers.Add(new Layer(sizeOfHiddenLayers, numOutputs, Activation.Linear));
			TrainingRate = 0.05;
		}
        public MultiLayerPerceptron()
        {
            Layers = new List<ILayer>();
            TrainingRate = 0.05;
        }

        public List<Value> FeedForward(List<double> inputs)
		{
			var inputsAsValues = new List<Value>();
			foreach(double input in inputs)
				inputsAsValues.Add(new Value(input));

			var outputsOfPreviousLayer = inputsAsValues;
			foreach (var layer in Layers)
				outputsOfPreviousLayer = layer.ActivateLayer(outputsOfPreviousLayer);
			return outputsOfPreviousLayer;
		}

        public List<Value> GetParameters()
        {
            var parameters = new List<Value>();
            foreach (var layer in Layers)
                parameters.AddRange(layer.GetParameters());
            return parameters;
        }

		public void TrainBatch(IEnumerable<DataPoint> batch)
		{
            var loss = new Value(0.0);
            foreach (var datapoint in batch)
			{
                var outputValueList = FeedForward(datapoint.Input);
                
                for (int i = 0; i < outputValueList.Count(); i++)
					loss = loss.Add((new Value(datapoint.Target[i]).Subtract(outputValueList[i])).Pow(new Value(2)));
            }
            //System.Diagnostics.Debug.WriteLine(loss.Data);
            loss.Gradient = 1.0;
            loss.Backword();
            var parameters = GetParameters();
            foreach (var parameter in parameters)
            {
                parameter.Data += -TrainingRate * parameter.Gradient;
                parameter.Reset();
            }
        }
    }
}

