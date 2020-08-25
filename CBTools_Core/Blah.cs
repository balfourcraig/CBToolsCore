using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core {
    public static class Blah {

        public static void Caller() {

            if(DoSomething(3, -5).HasValue(out int value)) {
                Console.WriteLine(value);
            }
            else {
                Console.WriteLine("Oh no");
            }

            Console.WriteLine(DoSomething(3,-5) switch
            {
                Some<int> some => some.ValueOrDefault(),
                Err<int> err => err.Message,
                None<int> n => "NOTHING",
                _ => "Something else"
            }
            );
        }

        public static IOption<int> DoSomething(int num1, int num2) {
            if(num1 < 0 || num2 < 0) {
                return new Err<int>("Can't be negative, you bozo");
            }
            return Some<int>.New(num1 + num2);
        }

    }
}
