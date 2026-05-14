namespace Msa3edcomAdmin.Models;

/// <summary>
/// Lightweight notification stub. By default it just logs notifications so the
/// app works out-of-the-box. Drop in a real SMTP/SendGrid client later by
/// implementing SendAsync to actually deliver mail.
///
/// Configure recipient in appsettings.json:
///   "Notifications": { "AdminEmail": "you@example.com" }
/// </summary>
public class EmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly string? _adminEmail;

    public EmailService(ILogger<EmailService> logger, IConfiguration config)
    {
        _logger = logger;
        _adminEmail = config["Notifications:AdminEmail"]
                      ?? config["DefaultAdmin:Email"];
    }

    public Task NotifyNewLeadAsync(LeadRequest lead)
    {
        var body =
            $"New lead from {lead.FullName} <{lead.Email}>" +
            (string.IsNullOrEmpty(lead.PhoneNumber)  ? "" : $" · phone {lead.PhoneNumber}") +
            (string.IsNullOrEmpty(lead.ServiceType)  ? "" : $" · service {lead.ServiceType}") +
            (string.IsNullOrEmpty(lead.Budget)       ? "" : $" · budget {lead.Budget}") +
            (string.IsNullOrEmpty(lead.AttachmentUrl) ? "" : $" · attachment {lead.AttachmentUrl}");

        _logger.LogInformation("[Lead notification → {To}] {Body}", _adminEmail ?? "(no admin email configured)", body);
        return Task.CompletedTask;
    }
}
