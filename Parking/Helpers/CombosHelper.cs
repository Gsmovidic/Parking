using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.Data;

namespace Parking.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboPlanesAsync()
        {
            List<SelectListItem> list = await _context.Planes.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleciona Un Plan]",
                Value = "0"
            });

            return list;
        }       
    }
}
