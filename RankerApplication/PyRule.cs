using System;
using IronPython.Hosting;
using IronPython.Runtime.Exceptions;
using Microsoft.Scripting.Hosting;

namespace RankerApplication
{
    public class PyRule:IRule
    {
        public int Rank(Snippet snippet)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            engine.Execute(Code, scope);

            Func<string, int> function;
            if (!scope.TryGetVariable("run", out function))
            {
                throw new Exception("Function run is not defined");
            }

            int rank = GetRank(function, snippet);


            return rank;
        }

        private static int GetRank(Func<string, int> function, Snippet snippet)
        {
            int rank;
            try
            {
                rank = function(snippet.Code);
            }
            catch (TypeErrorException ex)
            {
                throw new Exception("Script Error", ex);
            }
            return rank;
        }

        public double Weight {get; set; }

        public string Code { get; set; }
    }
}