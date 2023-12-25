using GraphQL;
using GraphQL.SystemTextJson;
using GraphQLAPI.Models;
using Microsoft.Extensions.Options;
using UserInfoGraphQL;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQL(b => b
                .AddSchema<UserInfoSchema>()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(UserInfoSchema).Assembly)
                .UseMemoryCache()
                .UseApolloTracing(options => options.RequestServices!.GetRequiredService<IOptions<GraphQLSettings>>().Value.EnableMetrics));
builder.Services.AddSingleton<UserInfoData>();

builder.Services.AddGraphQL(b => b
                .AddSchema<AnotherUserInfoSchema>()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(AnotherUserInfoSchema).Assembly)
                .UseMemoryCache()
                .UseApolloTracing(options => options.RequestServices!.GetRequiredService<IOptions<GraphQLSettings>>().Value.EnableMetrics));
builder.Services.AddSingleton<AnotherUserInfoData>();

builder.Services.Configure<GraphQLSettings>(builder.Configuration.GetSection("GraphQLSettings"));
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new InputsJsonConverter()));

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
_ = app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseGraphQL<UserInfoSchema>("/graphql/information");
app.UseGraphQL<AnotherUserInfoSchema>("/graphql/anotherinformation");

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();