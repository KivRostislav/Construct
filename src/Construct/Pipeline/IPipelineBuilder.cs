namespace Construct.Pipeline;

public interface IPipelineBuilder<T>
{
    IPipelineBuilder<T> Register(Action<T> middleware);

    IPipelineBuilder<T> Register(Action<T>[] middlewares);

    Action<T> Build();
}
