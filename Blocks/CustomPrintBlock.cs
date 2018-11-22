using System.Collections.Generic;
using System.Linq;
using IronBlock;

namespace IronDemo.Blocks
{
    internal class CustomPrintBlock : IBlock
    {
        public static List<string> Text { get; set; }

        static CustomPrintBlock()
        {
            Text = new List<string>();
        }

        public override object Evaluate(Context context)
        {
            Text.Add((this.Values.First(x => x.Name == "TEXT").Evaluate(context) ?? "").ToString());
            return base.Evaluate(context);
        }
    }


}