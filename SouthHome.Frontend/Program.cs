using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using SouthHome.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// 添加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazor();

// 添加 Razor Pages (用于 _Host.cshtml)
builder.Services.AddRazorPages();

// 添加 Razor 组件
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 添加 Fluent UI 服务
builder.Services.AddFluentUIComponents();

var app = builder.Build();

// 配置 HTTP 请求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 映射 Razor Pages
app.MapRazorPages();

// 映射 Razor 组件
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// 回退到 _Host 页面
app.MapFallbackToPage("/_Host");

app.Run();
