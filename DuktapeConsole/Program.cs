using System;
using System.Collections.Generic;
using System.Text;

namespace DuktapeConsole {
  class Program {

    static void Main(string[] args) {
      Console.Title = "Celones Duktape.NET Console, version "
        + System.Reflection.Assembly.GetExecutingAssembly().GetName()
        .Version.ToString();

      using(var js = new Duktape.Context()) {
        object[] objCelones = {
          "Duktape.NET",
          "No spoko.",
          new object[] { 2, 3.14 }
        };

        js.SetParameter("Celones", objCelones);
        js.Run(@"// fib.js
function fib(n) {
    if (n == 0) { return 0; }
    if (n == 1) { return 1; }
    return fib(n-1) + fib(n-2);
}

function test() {
    var res = [];
    for (i = 0; i < 20; i++) {
        res.push(fib(i));
    }
    print(res.join(' '));
}

test();");

        var line = "";
        while(true) {
          Console.Write("> ");
          line = Console.ReadLine();

          if(line == "exit")
            break;

          try {
            var value = js.Run(line);
            Console.WriteLine(value.ToString());
          } catch(Exception e) {
            Console.Error.WriteLine(e.GetType().Name + ": " + e.Message);
          }
        }
      }
    }

  }
}
