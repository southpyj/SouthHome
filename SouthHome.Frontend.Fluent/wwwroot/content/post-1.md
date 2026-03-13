# ASP.NET Core 中间件深入解析

## 什么是中间件？

中间件是 ASP.NET Core 应用程序的核心概念，它构成了请求处理管道。

## 中间件的工作原理

每个中间件可以选择：

- 将请求传递给下一个中间件
- 短路处理，不继续传递

```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 处理请求
        await _next(context);
        // 处理响应
    }
}
```

## 常用中间件

| 中间件 | 功能 |
|--------|------|
| Authentication | 身份验证 |
| Authorization | 授权 |
| CORS | 跨域资源共享 |
| StaticFiles | 静态文件服务 |

## 扩展方法模式

使用扩展方法注册中间件是常见做法：

```csharp
public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustom(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}
```

## 总结

中间件提供了灵活的请求处理方式，是理解 ASP.NET Core 的关键。
