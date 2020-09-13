using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.AI
{
    public static class GamePatterns
    {
        public static readonly Dictionary<string, int> MediumPatterns = new Dictionary<string, int>
        {
            { "*XXXX", 10000},
            { "X*XXX", 10000},
            { "XX*XX", 10000},
            //
            { "*YYYY", 3000},
            { "Y*YYY", 3000},
            { "YY*YY", 3000},
            //
            { "+XXX*+", 601},
            { "+XX*X+", 601},
            { "XXX*++", 251},
            { "XX*X++", 251},
            { "X*XX++", 251},
            { "*XXX++", 251},
            { "X+*XX++", 230},
            { "X+X*X++", 230},
            { "X*X+X++", 200},
            { "+X*X+X+", 200},
            { "+X+*+X", 100},
            //
            { "+YYY*+", 401},
            { "+YY*Y+", 401},
            { "YYY*++", 220},
            { "YY*Y++", 220},
            { "Y*YY++", 220},
            { "*YYY++", 220},
            { "Y+*YY++", 200},
            { "Y+Y*Y++", 200},
            { "Y*Y+Y++", 150},
            { "+Y*Y+Y+", 150},
            { "+Y+*+Y", 80},
            //
            { "+*XX++", 70},
            { "++X*X++", 70},
            { "++X*X+", 60},
            { "++X*X", 50},
            //            
            { "+*YY++", 70},
            { "++Y*Y++", 70},
            { "++Y*Y+", 60},
            { "++Y*Y", 50},
            //
            { "+Y*++", 40},
            //
            { "+X*++", 40},
            { "++*++", 30},
            { "+*+", 20},
            { "+*", 10},
        };
        public static readonly Dictionary<string, int> EasyPatterns = new Dictionary<string, int>
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
            { "+*XXX", 250},
            { "+XXX*", 250},
            //
            { "+YYY*+", 250},
            { "+Y*YY+", 250},
            { "*YYY+", 130},
            //
            { "+*XX++", 70},
            //            
            { "++Y*Y++", 70},
            //
            { "+Y*++", 20},
            //
            { "+X*++", 20},
        };
    }
}
