namespace dotnetgrad
{
    public interface ILayer
    {
        List<Neuron> Neurons { get; }

        List<Value> ActivateLayer(List<Value> inputValues);
        List<Value> GetParameters();
    }
}