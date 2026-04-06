using MediatR;

namespace LicenseService.Commands
{
    public record CreateLicenseCommand(
        string TenantId,
        string UserId,
        string LicenseType
    ) : IRequest<int>;
}
