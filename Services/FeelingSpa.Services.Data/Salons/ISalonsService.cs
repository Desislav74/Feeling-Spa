using System.Threading.Tasks;
using FeelingSpa.Web.ViewModels.Salons;

namespace FeelingSpa.Services.Data.Salons
{
    public interface ISalonsService
    {
        Task CreateAsync(CreateSalonInputModel input, string imagePath);
    }
}
