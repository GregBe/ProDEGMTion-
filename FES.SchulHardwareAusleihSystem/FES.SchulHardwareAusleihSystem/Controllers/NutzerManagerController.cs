using FES.SchulHardwareAusleihSystem.Data;
using FES.SchulHardwareAusleihSystem.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FES.SchulHardwareAusleihSystem.Controllers
{
    public class NutzerManagerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public NutzerManagerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var vm = new NutzerManagementIndexModel
            {
                Users = _dbContext.Users.OrderBy(u => u.Email).ToList()
            };

            return View(vm);
        }
    }
}
