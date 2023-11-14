using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using candidate.Models;
using candidate.Models.ViewModels;

namespace candidate.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly r53_candidateDbContext _context;
        private readonly IWebHostEnvironment _he;
        public CandidatesController(r53_candidateDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Candidates.Include(x => x.CandidateSkills).ThenInclude(y => y.Skills).ToListAsync());
        }
        public IActionResult AddNewSkills(int? id)
        {
            ViewBag.skill = new SelectList(_context.Skills, "SkillsId", "SkillName", id.ToString() ?? "");
            return PartialView("_AddNewSkills");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateVM candidateVM, int[] SkillsId)
        {
            if (ModelState.IsValid)
            {
                Candidate candidate = new Candidate
                {
                    CandidateName = candidateVM.CandidateName,
                    DateOfBirth = candidateVM.DateOfBirth,
                    Phone = candidateVM.Phone,
                    Fresher = candidateVM.Fresher
                };
                var file = candidateVM.ImagePath;
               
                if (file != null)
                {
                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string fileToSave = Path.Combine(webroot, folder, Path.GetFileName(file.FileName));
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        candidateVM.ImagePath!.CopyTo(stream);
                        candidate.Image = "/" + folder + "/" + Path.GetFileName(file.FileName);
                    }
                }
                foreach (var item in SkillsId)
                {
                    CandidateSkill candidateSkills = new CandidateSkill()
                    {
                        Candidate = candidate,
                        CandidateId = candidate.CandidateId,
                        SkillsId = item
                    };
                    _context.CandidateSkills.Add(candidateSkills);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.CandidateId == id);
            CandidateVM candidateVM = new CandidateVM()
            {
                CandidateId = candidate!.CandidateId,
                CandidateName = candidate.CandidateName,
                DateOfBirth = candidate.DateOfBirth,
                Phone = candidate.Phone,
                Image = candidate.Image,
                Fresher = candidate.Fresher


            };
            var existSkill = _context.CandidateSkills.Where(x => x.CandidateId == id).ToList();
            foreach (var item in existSkill)
            {
                candidateVM.SkillList.Add(item.SkillsId);
            }
            return View(candidateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CandidateVM candidateVM, int[] SkillsId)
        {
            if (ModelState.IsValid)
            {
                Candidate candidate = new Candidate
                {
                    CandidateId = candidateVM.CandidateId,
                    CandidateName = candidateVM.CandidateName,
                    DateOfBirth = candidateVM.DateOfBirth,
                    Phone = candidateVM.Phone,
                    Fresher = candidateVM.Fresher,
                    Image = candidateVM.Image
                };
                var file = candidateVM.ImagePath;
                if (file != null)
                {
                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Path.GetFileName(candidateVM.ImagePath!.FileName);
                    string fileToSave = Path.Combine(webroot, folder, imgFileName);
                    using (var stream = new FileStream(fileToSave, FileMode.Create))
                    {
                        candidateVM.ImagePath.CopyTo(stream);
                        candidate.Image = "/" + folder + "/" + imgFileName;
                    }
                }
                var existSkill = _context.CandidateSkills.Where(x => x.CandidateId == candidate.CandidateId).ToList();
                foreach (var item in existSkill)
                {
                    _context.CandidateSkills.Remove(item);
                }
                foreach (var item in SkillsId)
                {
                    CandidateSkill candidateSkills = new CandidateSkill()
                    {
                        CandidateId = candidate.CandidateId,
                        SkillsId = item
                    };
                    _context.CandidateSkills.Add(candidateSkills);
                }
                _context.Update(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.CandidateId == id);
            var existSkill = _context.CandidateSkills.Where(x => x.CandidateId == id).ToList();
            foreach (var item in existSkill)
            {
                _context.CandidateSkills.Remove(item);
            }
            _context.Remove(candidate!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}