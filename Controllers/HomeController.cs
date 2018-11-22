using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IronDemo.Models;
using IronBlock;
using IronBlock.Blocks;
using System.IO;
using IronDemo.Blocks;

namespace IronDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/execute")]
        public IActionResult Execute()
        {
            var parser = new Parser();
            parser.AddStandardBlocks();
            CustomPrintBlock.Text.Clear();
            parser.AddBlock<CustomPrintBlock>("text_print");
            parser.AddBlock<TempBlock>("temp");

            var xml = new StreamReader(this.Request.Body).ReadToEnd();
            var workspace = parser.Parse(xml);

            workspace.Evaluate();

            return this.Content(string.Join('\n',CustomPrintBlock.Text));
        }
    }

}
