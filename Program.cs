using System;
using BenchmarkDotNet.Running;

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
            Performance();
        }

        private static void Tuples_Structures_On_The_Fly() {
            Console.WriteLine("STRUCTURES ON THE FLY\n");
            // Assume you have a x,y coordinate 
            var example_A = (1, 4); // Access by using .Item1, .Item2, and so on
            var example_B = (x: 4, y: 1); // Access by using .x and .y
            var example_C = (x: 10, y: 20, 30); // Access by .x, .y, .Item3
            (var example_D_x, var example_D_y) = (-1.0, -2.0); // Supports other than just integers
            var example_E = (name: "Rafael", surname: "Batista", company: "Rock Content", position: "Fullstack Developer", currentWorkPlace: "Home Office", doingNow: "Presenting how amazing this tuples are!"); // Strings are also fair deal

            Console.WriteLine($"- Coordinates A set as ({example_A.Item1},{example_A.Item2})");
            Console.WriteLine($"- Coordinates B set as ({example_B.x},{example_B.y})");
            Console.WriteLine($"- Coordinates C set as ({example_C.x},{example_C.y},{example_C.Item3})");
            Console.WriteLine($"- Coordinates D set as ({example_D_x},{example_D_y})");
            Console.WriteLine($"- Presented by {example_E.name} {example_E.surname} that works at {example_E.company} as a {example_E.position}. He is currently working {example_E.currentWorkPlace} and is actually {example_E.doingNow}");

            // Bringing the variables out and changing their values
            (var x, var y) = example_B;
            Console.WriteLine($"- The values x and y originated from {example_B} are, respectively, {x} and {y}");
            x = 7;
            example_B.y = 8;
            Console.WriteLine($"- After some changes, the original value is {example_B} and the extracted values are {x} and {y} -> value and not reference!");

            // Common scenario: Swapping two values 
            Console.WriteLine($"- The values of x and y are, respectively, {x} and {y}");

            (x, y) = (y, x); // NO NEED FOR A THIRD VARIABLE!

            Console.WriteLine($"- The new values of x and y are, respectively, {x} and {y} !");

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
            (int A, int B) val_1 = (10,20);
            (long A, byte B) val_2 = (10,20);
            (int B, int A) val_3 = (10,20);
            (int B, int A) val_4 = (20,10);
            (int Airplane, int Boat) val_5 = (10,20);

            Console.WriteLine($"val_1 == val_2 is {val_1 == val_2}, since {val_1} == {val_2}");
            Console.WriteLine($"val_1 == val_3 is {val_1 == val_3}, since {val_1} == {val_3}");
            Console.WriteLine($"val_1 == val_4 is {val_1 == val_4}, since {val_1} != {val_4}");
            Console.WriteLine($"val_1 == val_5 is {val_1 == val_5}, since {val_1} == {val_5}");
            
            if (val_1.A == 10 && val_1.B == 20 && val_4.B == 20 && val_4.A == 10) {
                Console.WriteLine($"- val_1 meets both requirements");
            } 

            if (val_1 == (10,20) && val_4 == (20,10)) {
                Console.WriteLine($"- val_4 meets both requirements");
            } 

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
            Console.WriteLine($"Scenario 1: App for car ride.");
            // Calculate price multiplier for rides

            // definitions:
            // rush hour: between 6 - 8 am, 5 - 7 pm

            // How many drivers and how many riders?
            // if driver / rider ratio greater or equal than 3,     non rush,   weekday     = 1.0
            // if driver / rider ratio greater or equal than 3,     rush,       weekday     = 1.25
            // if driver / rider ratio greater or equal than 3,     non rush,   weekend     = 1.0
            // if driver / rider ratio greater or equal than 3,     rush,       weekend     = 1.0

            // if driver / rider ratio between 2 and 3,             non rush,   weekday     = 1.25
            // if driver / rider ratio between 2 and 3,             rush,       weekday     = 1.5
            // if driver / rider ratio between 2 and 3,             non rush,   weekend     = 1.0
            // if driver / rider ratio between 2 and 3,             rush,       weekend     = 1.0

            // if driver / rider ratio between 1 and 2,             non rush,   weekday     = 1.5
            // if driver / rider ratio between 1 and 2,             rush,       weekday     = 1.75
            // if driver / rider ratio between 1 and 2,             non rush,   weekend     = 1.25
            // if driver / rider ratio between 1 and 2,             rush,       weekend     = 1.25

            // if driver / rider ratio less or equal than 1,        non rush,   weekday     = 1.75
            // if driver / rider ratio less or equal than 1,        rush,       weekday     = 2
            // if driver / rider ratio less or equal than 1,        non rush,   weekend     = 1.5
            // if driver / rider ratio less or equal than 1,        rush,       weekend     = 1.5

            var time = new DateTime(2020, 07, 15, 17, 0, 0);
            var ratio = 2;
            
            var multiplier_1 = RideApp.CalculateMultiplier_Version_1(time, ratio);
            var multiplier_2 = RideApp.CalculateMultiplier_Version_2(time, ratio);
            var multiplier_3 = RideApp.CalculateMultiplier_Version_3(time, ratio);
            var multiplier_4 = RideApp.CalculateMultiplier_Version_4(time, ratio);
            var multiplier_5 = RideApp.CalculateMultiplier_Version_5(time, ratio);

            Console.WriteLine($"- Multiplier on version 1: {multiplier_1}");
            Console.WriteLine($"- Multiplier on version 2: {multiplier_2}");
            Console.WriteLine($"- Multiplier on version 3: {multiplier_3}");
            Console.WriteLine($"- Multiplier on version 4: {multiplier_4}");
            Console.WriteLine($"- Multiplier on version 5 : {multiplier_5}");

            var point_1 = new Point(10,20);
            var point_2 = new Point(-10,20);
            var point_3 = new Point(-10,-20);
            var point_4 = new Point(10,-20);
            var point_5 = new Point(0,0);
            var point_6 = new Point(0,-20);

            Console.WriteLine($"Scenario 2: Getting the quadrant of a point.");
            Console.WriteLine($"- The point {point_1.Deconstruct()} is on the quadrant {point_1.GetQuadrant()}");
            Console.WriteLine($"- The point {point_2.Deconstruct()} is on the quadrant {point_2.GetQuadrant()}");
            Console.WriteLine($"- The point {point_3.Deconstruct()} is on the quadrant {point_3.GetQuadrant()}");
            Console.WriteLine($"- The point {point_4.Deconstruct()} is on the quadrant {point_4.GetQuadrant()}");
            Console.WriteLine($"- The point {point_5.Deconstruct()} is on the quadrant {point_5.GetQuadrant()}");
            Console.WriteLine($"- The point {point_6.Deconstruct()} is on the quadrant {point_6.GetQuadrant()}");


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
    
        private static void Performance()
        {
            var summary_1 = BenchmarkRunner.Run<BenchmarkTupplePatterns>();
            var summary_2 = BenchmarkRunner.Run<BenchmarkTuppleSwap>();
        }
    }


    public class Point {
        public int x {get; set;}
        public int y {get; set;}

        public Point(int x,int y) => (this.x, this.y) = (x,y);
        public void Deconstruct(out int x, out int y) => (x, y) = (this.x, this.y);
        public (int, int) Deconstruct() => (x, y);

        public enum Quadrant {
            Origin,
            One,
            Two,
            Three,
            Four,
            OnBorder
        }
        public Quadrant GetQuadrant() => (x,y) switch
        {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            var (_, _) => Quadrant.OnBorder
        };
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

    public static class RideApp {
        private static bool IsWeekend(DateTime time) =>
            time.DayOfWeek switch {
                DayOfWeek.Saturday => true,
                DayOfWeek.Sunday => true,
                _ => false
            };

        private enum TimeBand {
            Rush,
            Non_Rush
        }

        private static TimeBand GetTimeBand(DateTime time) {
            int hour = time.Hour;
            if (hour >= 6 && hour < 8)
                return TimeBand.Rush;
            else if (hour >= 17 && hour < 19)
                return TimeBand.Rush;
            else 
                return TimeBand.Non_Rush;
        }

        private enum RatioBand {
            Free_Capacity,
            Normal_Capacity,
            Busy_Capacity,
            Full_Capacity
        }

        private static RatioBand GetRatioBand(int ratio) {
            if (ratio >= 3)
                return RatioBand.Free_Capacity;
            else if (ratio > 2)
                return RatioBand.Normal_Capacity;
            else  if (ratio > 1)
                return RatioBand.Busy_Capacity;
            else
                return RatioBand.Full_Capacity;
        }

        public static decimal CalculateMultiplier_Version_1(DateTime time, int ratio) {
            if (IsWeekend(time)) {
                if (GetTimeBand(time) == TimeBand.Rush) {
                    if (GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                        return 1.0m;
                    } else if (GetRatioBand(ratio) == RatioBand.Normal_Capacity) { 
                        return 1.0m;
                    } else if (GetRatioBand(ratio) == RatioBand.Busy_Capacity) { 
                        return 1.25m;
                    } else { // GetRatioBand(ratio) == RatioBand.Full
                        return 1.5m;
                    }
                } else { // GetTimeBand(time) == TimeBand.Non_Rush
                  if (GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                        return 1.0m;
                    } else if (GetRatioBand(ratio) == RatioBand.Normal_Capacity) { 
                        return 1.0m;
                    } else if (GetRatioBand(ratio) == RatioBand.Busy_Capacity) { 
                        return 1.25m;
                    } else { // GetRatioBand(ratio) == RatioBand.Full
                        return 1.5m;
                    }
                }
            } else { // IsWeekend(time) == false
                if (GetTimeBand(time) == TimeBand.Rush) {
                  if (GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                        return 1.25m;
                    } else if (GetRatioBand(ratio) == RatioBand.Normal_Capacity) { 
                        return 1.5m;
                    } else if (GetRatioBand(ratio) == RatioBand.Busy_Capacity) { 
                        return 1.75m;
                    } else { // GetRatioBand(ratio) == RatioBand.Full
                        return 2.0m;
                    }
                } else { // GetTimeBand(time) == TimeBand.Non_Rush
                    if (GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                        return 1.0m;
                    } else if (GetRatioBand(ratio) == RatioBand.Normal_Capacity) { 
                        return 1.25m;
                    } else if (GetRatioBand(ratio) == RatioBand.Busy_Capacity) { 
                        return 1.5m;
                    } else { // GetRatioBand(ratio) == RatioBand.Full
                        return 1.75m;
                    }
                }
            }
        }

        public static decimal CalculateMultiplier_Version_2(DateTime time, int ratio) {
            if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                return 1.0m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Normal_Capacity) {
                return 1.0m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Busy_Capacity) {
                return 1.25m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Full_Capacity) {
                return 1.50m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                return 1.0m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Normal_Capacity) {
                return 1.0m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Busy_Capacity) {
                return 1.25m;
            } else if (IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Full_Capacity) {
                return 1.5m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                return 1.25m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Normal_Capacity) {
                return 1.5m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Busy_Capacity) {
                return 1.75m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush && GetRatioBand(ratio) == RatioBand.Full_Capacity) {
                return 2.0m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                return 1.0m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Normal_Capacity) {
                return 1.25m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Busy_Capacity) {
                return 1.5m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Non_Rush && GetRatioBand(ratio) == RatioBand.Full_Capacity) {
                return 1.75m;
            } else {
                return 1.0m;
            }
        }

        public static decimal CalculateMultiplier_Version_3(DateTime time, int ratio) {
            if (       IsWeekend(time)      && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Free_Capacity
                    || IsWeekend(time)      && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Normal_Capacity
                    || IsWeekend(time)      && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Free_Capacity
                    || IsWeekend(time)      && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Normal_Capacity
                    || !IsWeekend(time)     && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Free_Capacity) {
                return 1.0m;
            } else if (IsWeekend(time)      && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Busy_Capacity
                    || IsWeekend(time)      && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Busy_Capacity
                    || !IsWeekend(time)      && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Free_Capacity
                    || !IsWeekend(time)     && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Normal_Capacity) {
                return 1.25m;
            } else if (IsWeekend(time)      && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Full_Capacity
                    || IsWeekend(time)      && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Full_Capacity
                    || !IsWeekend(time)     && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Normal_Capacity) {
                return 1.50m;
            } else if (!IsWeekend(time)     && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Busy_Capacity
                    || !IsWeekend(time)     && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Busy_Capacity
                    || !IsWeekend(time)     && GetTimeBand(time) == TimeBand.Non_Rush   && GetRatioBand(ratio) == RatioBand.Full_Capacity) {
                return 1.75m;
            } else if (!IsWeekend(time) && GetTimeBand(time) == TimeBand.Rush       && GetRatioBand(ratio) == RatioBand.Full_Capacity) {
                return 2.0m;
            } else {
                return 1.0m;
            }
        }

        public static decimal CalculateMultiplier_Version_4(DateTime time, int ratio) => 
            (GetRatioBand(ratio), GetTimeBand(time), IsWeekend(time)) switch {
                (RatioBand.Free_Capacity,   TimeBand.Rush,      true)   => 1.0m,
                (RatioBand.Normal_Capacity, TimeBand.Rush,      true)   => 1.0m,
                (RatioBand.Busy_Capacity,   TimeBand.Rush,      true)   => 1.25m,
                (RatioBand.Full_Capacity,   TimeBand.Rush,      true)   => 1.5m,
                (RatioBand.Free_Capacity,   TimeBand.Non_Rush,  true)   => 1.0m,
                (RatioBand.Normal_Capacity, TimeBand.Non_Rush,  true)   => 1.0m,
                (RatioBand.Busy_Capacity,   TimeBand.Non_Rush,  true)   => 1.25m,
                (RatioBand.Full_Capacity,   TimeBand.Non_Rush,  true)   => 1.5m,
                (RatioBand.Free_Capacity,   TimeBand.Rush,      false)  => 1.25m,
                (RatioBand.Normal_Capacity, TimeBand.Rush,      false)  => 1.5m,
                (RatioBand.Busy_Capacity,   TimeBand.Rush,      false)  => 1.75m,
                (RatioBand.Full_Capacity,   TimeBand.Rush,      false)  => 2.0m,
                (RatioBand.Free_Capacity,   TimeBand.Non_Rush,  false)  => 1.0m,
                (RatioBand.Normal_Capacity, TimeBand.Non_Rush,  false)  => 1.25m,
                (RatioBand.Busy_Capacity,   TimeBand.Non_Rush,  false)  => 1.5m,
                (RatioBand.Full_Capacity,   TimeBand.Non_Rush,  false)  => 1.75m,
                (_,_,_)                                                 => 1.0m
            };

        public static decimal CalculateMultiplier_Version_5(DateTime time, int ratio) => 
            (GetRatioBand(ratio), GetTimeBand(time), IsWeekend(time)) switch {
                (RatioBand.Full_Capacity,   TimeBand.Rush,      false)  => 2.0m,
                (RatioBand.Full_Capacity,   TimeBand.Non_Rush,  false)  => 1.75m,
                (RatioBand.Busy_Capacity,   TimeBand.Rush,      false)  => 1.75m,
                (RatioBand.Full_Capacity,   _,                  true)   => 1.5m,
                (RatioBand.Busy_Capacity,   TimeBand.Non_Rush,  false)  => 1.5m,
                (RatioBand.Normal_Capacity, TimeBand.Rush,      false)  => 1.5m,
                (RatioBand.Busy_Capacity,   _,                  true)   => 1.25m,
                (RatioBand.Free_Capacity,   TimeBand.Rush,      false)  => 1.25m,
                (RatioBand.Normal_Capacity, TimeBand.Non_Rush,  false)  => 1.25m,
                (_,_,_)                                                 => 1.0m
            };

        public static decimal CalculateMultiplier_Version_6(DateTime time, int ratio) {
            switch((GetRatioBand(ratio), GetTimeBand(time), IsWeekend(time))) {
                case (RatioBand.Full_Capacity,   TimeBand.Rush,      false) :
                    return 2.0m;
                case (RatioBand.Full_Capacity,   TimeBand.Non_Rush,  false) :
                case (RatioBand.Busy_Capacity,   TimeBand.Rush,      false) :
                    return 1.75m;
                case (RatioBand.Full_Capacity,   _,                  true)  :
                case (RatioBand.Busy_Capacity,   TimeBand.Non_Rush,  false) : 
                case (RatioBand.Normal_Capacity, TimeBand.Rush,      false) :
                    return 1.5m;
                case (RatioBand.Busy_Capacity,   _,                  true)  :
                case (RatioBand.Free_Capacity,   TimeBand.Rush,      false) :
                case (RatioBand.Normal_Capacity, TimeBand.Non_Rush,  false) :
                    return 1.25m;
                default: 
                    return 1.0m;
            }
        }
    }
}
