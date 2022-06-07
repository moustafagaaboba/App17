using System.Threading.Tasks;

namespace App17.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
