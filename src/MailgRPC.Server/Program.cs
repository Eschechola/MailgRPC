using EscNet.IoC.Mails;
using MailgRPC.Server.Services;

var builder = WebApplication.CreateBuilder(args);

#region Services

string senderEmail = builder.Configuration["Mailing:Email"];
string senderPassword = builder.Configuration["Mailing:Password"];

builder.Services.AddEmailSender(senderEmail, senderPassword);
builder.Services.AddGrpc();

#endregion

#region Middlewares

var app = builder.Build();
app.MapGrpcService<MailService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

#endregion