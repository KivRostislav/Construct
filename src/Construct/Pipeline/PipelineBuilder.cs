namespace Construct.Pipeline;

public class PipelineBuilder<T> : IPipelineBuilder<T>
{
    private readonly List<Action<T>> _middlewares;

    public PipelineBuilder()
    {
        _middlewares = new();
    }

    public IPipelineBuilder<T> Register(Action<T> middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }

    public IPipelineBuilder<T> Register(Action<T>[] middlewares)
    {
        _middlewares.AddRange(middlewares);
        return this;
    }

    public Action<T> Build()
    {
        return (argument) =>
        {
            foreach (Action<T> middleware in _middlewares)
            {
                middleware(argument);
            }
        };
    }
}
