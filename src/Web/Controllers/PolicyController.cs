using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Controllers
{
    public class PolicyController : Controller
    {
        private readonly PolicyContext _context;

        public PolicyController(PolicyContext context)
        {
            _context = context;
        }

        // GET: Policy
        public async Task<IActionResult> Index()
        {
            return _context.PolicyModel != null ?
                        View(await _context.PolicyModel.ToListAsync()) :
                        Problem("Entity set 'PolicyContext.PolicyModel'  is null.");
        }

        // GET: Policy/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PolicyModel == null)
            {
                return NotFound();
            }

            var policyModel = await _context.PolicyModel
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policyModel == null)
            {
                return NotFound();
            }

            return View(policyModel);
        }

        // GET: Policy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Policy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyId,CustomerId,CoverType,PeriodType,PeriodDuration")] PolicyModel policyModel)
        {
            if (ModelState.IsValid)
            {
                policyModel.PolicyId = Guid.NewGuid().ToString();
                _context.Add(policyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policyModel);
        }

        // GET: Policy/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PolicyModel == null)
            {
                return NotFound();
            }

            var policyModel = await _context.PolicyModel.FindAsync(id);
            if (policyModel == null)
            {
                return NotFound();
            }
            return View(policyModel);
        }

        // POST: Policy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PolicyId,CustomerId,CoverType,PeriodType,PeriodDuration")] PolicyModel policyModel)
        {
            if (id != policyModel.PolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyModelExists(policyModel.PolicyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(policyModel);
        }

        // GET: Policy/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PolicyModel == null)
            {
                return NotFound();
            }

            var policyModel = await _context.PolicyModel
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policyModel == null)
            {
                return NotFound();
            }

            return View(policyModel);
        }

        // POST: Policy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PolicyModel == null)
            {
                return Problem("Entity set 'PolicyContext.PolicyModel'  is null.");
            }
            var policyModel = await _context.PolicyModel.FindAsync(id);
            if (policyModel != null)
            {
                _context.PolicyModel.Remove(policyModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyModelExists(string id)
        {
            return (_context.PolicyModel?.Any(e => e.PolicyId == id)).GetValueOrDefault();
        }
    }
}
