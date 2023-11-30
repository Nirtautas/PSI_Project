﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamWebApplicationAPI.Data.Database;
using TeamWebApplicationAPI.Data.ExceptionLogger;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataLogger _logger;

        public HomeController(ApplicationDBContext db, IDataLogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                HttpContext.Session.Clear();
                return View();
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        public IActionResult Registration()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}