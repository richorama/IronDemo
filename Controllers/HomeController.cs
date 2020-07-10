using System;
using Microsoft.AspNetCore.Mvc;
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
            var printBlock = new CustomPrintBlock();
            var xml = new StreamReader(this.Request.Body).ReadToEnd();

            new Parser()
                .AddStandardBlocks()
                .AddBlock("text_print", printBlock)
                .AddBlock<TempBlock>("temp")
                .Parse(xml)
                .Evaluate();

            return this.Content(string.Join('\n',printBlock.Text));
        }
    }

}
