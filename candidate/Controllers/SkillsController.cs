using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using candidate.Models;

namespace candidate.Controllers
{
    public class SkillsController : Controller
    {
        private readonly r53_candidateDbContext _context;

        public SkillsController(r53_candidateDbContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.ToListAsync());
        }



        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }


        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(m => m.SkillsId == id);
            var existSkill = _context.CandidateSkills.Where(x => x.SkillsId == id).ToList();
            foreach (var item in existSkill)
            {
                _context.CandidateSkills.Remove(item);
            }
            _context.Remove(skill!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
