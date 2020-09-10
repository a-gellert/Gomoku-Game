using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.AI
{
    public static class GamePatterns
    {
        public static readonly Dictionary<string, int> Patterns = new Dictionary<string, int>
        {
            { "*XXXX", 10000},
            { "X*XXX", 10000},
            { "XX*XX", 10000},
            //
            { "*YYYY", 1000},
            { "Y*YYY", 1000},
            { "YY*YY", 1000},
            //
            { "+X*XX+", 400},
            { "+XXX*+", 400},
            { "+*XXX", 250},
            { "+X*XX", 250},
            { "+XX*X", 250},
            { "+XXX*", 250},
            //
            { "+YYY*+", 250},
            { "+Y*YY+", 250},
            { "YY*Y+", 130},
            { "YYY*+", 130},
            { "Y*YY+", 130},
            { "*YYY+", 130},
            //
            { "+*XX++", 70},
            { "++X*X++", 70},
            //            
            { "+*YY++", 70},
            { "++Y*Y++", 70},
            //
            { "+Y*++", 20},
            //
            { "+X*++", 20},
            { "++*++", 10},
            { "+*+", 5},
            { "+*", 2},
            { "*", 1},
        };
    }
}
