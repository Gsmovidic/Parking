using Microsoft.AspNetCore.Mvc.Rendering;

namespace Parking.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboPlanesAsync();
    }
}
