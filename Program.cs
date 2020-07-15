using System;

namespace dotnet_trilha
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Tuples are awesome! Here are some examples!");
            Tuples_Structures_On_The_Fly();
            Classes_Construction_Deconstruction();
            Equality();
            As_Return_Value();
            As_Parameter();
            Tuple_Patterns();
            FunMath();
        }

        private static void Tuples_Structures_On_The_Fly() {
            Console.WriteLine("STRUCTURES ON THE FLY\n");
            // Assume you have a x,y coordinate 
            var point = (x: 0, y: 1);
            Console.WriteLine($"- Coordinates set as ({point.x},{point.y})");

            // Or maybe you're dealing with more than two variables?
            var thisPresentation = (name: "Rafael", surname: "Batista", company: "Rock Content", position: "Fullstack Developer", currentWorkPlace: "Home Office", doingNow: "Presenting how amazing this tuples are!");
            Console.WriteLine($"- Presented by {thisPresentation.name} {thisPresentation.surname} that works at {thisPresentation.company} as a {thisPresentation.position}. He is currently working {thisPresentation.currentWorkPlace} and is actually {thisPresentation.doingNow}");

            // Now shall we do some operations?
            var pointA = (x: 1, y: 5);
            var pointB = (x: 4, y: 1);
            var distance = Math.Sqrt(Math.Pow(pointA.x - pointB.x, 2) + Math.Pow(pointA.y - pointB.y, 2));
            Console.WriteLine($"- The distance between {pointA} and {pointB} is {distance}");

            // Bringing the variables out
            (var x, var y) = point;
            Console.WriteLine($"- The values x and y originated from {point} are, respectively, {x} and {y}");
            x = 7;
            point.y = 8;
            Console.WriteLine($"- The values x and y originated from {point} are, respectively, {x} and {y} -> value and not reference!");

            // Common scenario: Swapping two values 
            var val_1 = 5;
            var val_2 = 7;
            Console.WriteLine($"- The values of the val_1 and val_2 are, respectively, {val_1} and {val_2}");
            (val_1, val_2) = (val_2, val_1); // NO NEED FOR A THIRD VARIABLE!
            Console.WriteLine($"- The new values of the val_1 and val_2 are, respectively, {val_1} and {val_2}");


            Console.WriteLine("-----------------------------------------------------");
        }

        private static void Classes_Construction_Deconstruction()
        {
            Console.WriteLine("ASSIGNMENT AND DECONSTRUCTION\n");
            // starting tuple
            var point_tuple = (0,1);

            // parsing it into a class
            var point_class = new Point(point_tuple.Item1, point_tuple.Item2);

            // parsing into tuple, option 1
            var point_deconstructed_option_1 = (0,0);
            point_class.Deconstruct(out point_deconstructed_option_1.Item1, out point_deconstructed_option_1.Item2);

            // parsing into tuple, option 2
            var point_deconstructed_option_2 = point_class.Deconstruct();

            Console.WriteLine($"We started with {point_tuple}");
            Console.WriteLine($"We created a class from it, which values are the same: ({point_class.x},{point_class.y})");
            Console.WriteLine($"After that we deconstructed the class in two differente ways resulting in {point_deconstructed_option_1} and {point_deconstructed_option_2})");

            Console.WriteLine("-----------------------------------------------------");
        }

        private static void Equality()
        {
            Console.WriteLine("EQUALITY\n");
            // tuples support the == and != operators, which are executed in the order of the elements on the tuple (regardless of the name they have)
            (int A, int B) val_1 = (0,1);
            (long A, byte B) val_2 = (0,1);
            (int B, int A) val_3 = (0,1);
            (int B, int A) val_4 = (1,0);
            (int Banana, int Airplane) val_5 = (0,1);

            Console.WriteLine($"val_1 == val_2 is {val_1 == val_2}, since {val_1} == {val_2}");
            Console.WriteLine($"val_1 == val_3 is {val_1 == val_3}, since {val_1} == {val_3}");
            Console.WriteLine($"val_1 == val_4 is {val_1 == val_4}, since {val_1} != {val_4}");
            Console.WriteLine($"val_1 == val_5 is {val_1 == val_5}, since {val_1} == {val_5}");
            // TAKE NOTE! We are comparing two variables at the same time here!
            // Before this, you would have to do something like 
            // x1 == x2 && y1 == y2

            Console.WriteLine("-----------------------------------------------------");
        }

        private static void As_Return_Value()
        {
            Console.WriteLine("AS RETURN VALUE\n");
            
            var bankAccount = new BankAccount(1, 100);
            Console.WriteLine(bankAccount.ToString());

            Console.WriteLine("Withdrawing $25...");
            long balance;
            var success = bankAccount.Withdraw(25, out balance);
            Console.WriteLine($"The operation was {success}! New balance is {balance}");

            Console.WriteLine("Withdrawing another $25...");
            var result = bankAccount.Withdraw(25);
            Console.WriteLine($"The operation was {result.success}! New balance is {result.balance}");

            Console.WriteLine("-----------------------------------------------------");
        }


        private static void As_Parameter()
        {
            Console.WriteLine("AS PARAMETER\n");
            
            var bankAccount = new BankAccount(1, 10);
            Console.WriteLine(bankAccount.ToString());

            Console.WriteLine("Transfering $25...");
            var input = (account: 2, amount: 25)         ;
            var result = bankAccount.TransferTo(input);
            Console.WriteLine($"The operation was {result}!");

            Console.WriteLine("-----------------------------------------------------");
        }

        private static void Tuple_Patterns() // Introduced on C# 8
        {
            Console.WriteLine("TUPLE PATTERNS\n");
            Console.WriteLine("-----------------------------------------------------");
        }
        
        private static void FunMath()
        {
            Console.WriteLine("Fun Math!\n");

            // lets calculate the 40th fibonnaci number!
            var fibonnaci = (0, 1); // starting point
            for(var i = 0; i < 40; i ++) fibonnaci = (fibonnaci.Item2, fibonnaci.Item1 + fibonnaci.Item2);
            Console.WriteLine($"The 40th fibonnaci number is {fibonnaci.Item1}!\n");

            Console.WriteLine("-----------------------------------------------------");
        }
    }

    public class Point {
        public int x {get; set;}
        public int y {get; set;}

        public Point(int x,int y) => (this.x, this.y) = (x,y);
        public void Deconstruct(out int x, out int y) => (x, y) = (this.x, this.y);
        public (int, int) Deconstruct() => (x, y);
    }

    public class BankAccount {
        public int account {get; set;}
        public long balance {get; set;}
        public long limit {get; set;}

        public BankAccount(int account, long balance) => (this.account, this.balance, this.limit) = (account, balance, 0);

        public bool Withdraw(int amount, out long newBalance) {
            bool success;
            if (balance > amount) {
                balance = balance - amount;
                success = true;
            } else {
                success = false;
            }
            
            newBalance = balance;
            return success;
        }

        public (bool success, long balance) Withdraw(int amount) {
            bool success;

            if (balance > amount) {
                balance = balance - amount;
                success = true;
            } else {
                success = false;
            }
            
            return (success, balance);
        }

        public bool TransferTo((int account, long amount) account) {
            bool success;

            if (balance > account.amount) {
                balance = balance - account.amount;
                success = true;
            } else {
                success = false;
            }

            // A call to Deposit the {amount} into the {account}
            
            return success;
        }

        public override string ToString() {
            return $"Account {account} has ${balance}.";
        }

    }
}
