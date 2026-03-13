# 微服务架构设计模式

## 服务发现

### 服务注册与发现

- **客户端发现**：客户端查询注册中心获取服务地址
- **服务端发现**：通过负载均衡器路由请求

### 常用方案

| 方案 | 特点 |
|------|------|
| Consul | 支持 HTTP 和 DNS |
| Eureka | Netflix 出品，Spring Cloud 默认 |
| etcd | 简单高效，Kubernetes 使用 |

## 熔断降级

### 熔断器模式

```csharp
public class CircuitBreaker
{
    private int failureCount = 0;
    private DateTime lastFailureTime;
    private CircuitState state = CircuitState.Closed;

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
    {
        if (state == CircuitState.Open)
        {
            if (DateTime.Now - lastFailureTime > Timeout)
            {
                state = CircuitState.HalfOpen;
            }
            else
            {
                throw new CircuitOpenException();
            }
        }

        try
        {
            var result = await action();
            OnSuccess();
            return result;
        }
        catch (Exception ex)
        {
            OnFailure();
            throw;
        }
    }
}
```

## API 网关

### 核心功能

- 路由转发
- 负载均衡
- 认证授权
- 限流熔断
- 监控日志

## 总结

微服务架构需要综合考虑服务发现、熔断降级、API 网关等模式，确保系统的可用性和可维护性。
