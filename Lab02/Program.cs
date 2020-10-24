using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Lab02
{
    public class Worker : IEquatable<Worker>, IComparable<Worker>
    {
        private string _name;
        private DateTime _employmentDate;
        private decimal _salary;

        public Worker()
        {
            Name = "Anonymus";
            EmploymentDate = DateTime.Today;
            Salary = 0;
        }

        public string Name 
        { 
            get => this._name;
            set 
            {
                this._name = value.Trim();
            } 
        }
        public DateTime EmploymentDate
        {
            get => this._employmentDate;
            set 
            {
                if(value > DateTime.Today)
                    throw new ArgumentException("Have you been time travelling?");
                
                this._employmentDate = value;
            }
        }
        public decimal Salary
        {
            get => this._salary;
            set => this._salary = (value > 0) ? value : 0; 
        }

        public int EmploymentTime
        {
            get => (int)DateTime.Today.Subtract(EmploymentDate).TotalDays / 30;
        }

        public override bool Equals(object obj)
        {
            if(obj is Worker)
                return (this as IEquatable<Worker>).Equals(obj as Worker);

            return false;
        }

        public bool Equals([AllowNull] Worker other)
        {
            if(other is null)
                return false;
            if(Object.ReferenceEquals(this, other)) return true;

            return (this.Name == other.Name &&
                    this.EmploymentDate == other.EmploymentDate &&
                    this.Salary == other.Salary);
        }
                    

        public override int GetHashCode() => (Name, EmploymentDate, Salary).GetHashCode();

        public static bool Equals(Worker p1, Worker p2)
        {
            if((p1 is null) && (p2 is null)) return true;
            if(p1 is null) return false;

            return p1.Equals(p2);
        }

        public static bool operator ==(Worker p1, Worker p2) => Worker.Equals(p1, p2);

        public static bool operator !=(Worker p1, Worker p2) => !(p1 == p2);

        public int CompareTo([AllowNull] Worker other)
        {
            if( other == null ) return 1;
            if( this.Equals(other) ) return 0;
            if( this.Name != other.Name )
                return this.Name.CompareTo(other.Name);
            if( this.EmploymentDate != other.EmploymentDate )
                return this.EmploymentDate.CompareTo(other.EmploymentDate);
            
            return this.Salary.CompareTo(other.Salary);
        }

        public override string ToString()
        {
            return $"({this.Name}, {this.EmploymentDate}, {this.Salary})";
        }
    }

    public class ByEmploymentTimeThenBySalaryComparer : IComparer<Worker>
    {
        public int Compare([AllowNull] Worker x, [AllowNull] Worker y)
        {
            if(x is null && y is null) return 0;
            if(x is null) return -1;
            if(y is null) return 1;

            if(x.EmploymentTime != y.EmploymentTime)
                return x.EmploymentTime.CompareTo(y.EmploymentTime);

            return x.Salary.CompareTo(y.Salary);
        }
    }

    public class BySalaryDescendingThenByEmploymentComparer : IComparer<Worker>
    {
        public int Compare([AllowNull] Worker x, [AllowNull] Worker y)
        {
            if(x is null && y is null) return 0;
            if(x is null) return  1;
            if(y is null) return -1;

            if(x.Salary != y.Salary)
            {
                if(x.Salary > y.Salary)
                    return -1;
                
                if(x.Salary < y.Salary)
                    return 1;
            }

            return x.EmploymentTime.CompareTo(y.EmploymentTime);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            DateTime start = new DateTime(2020, 02, 26);

            var workers = new List<Worker>
            {
                new Worker { Name = "Wiecheć", EmploymentDate = new DateTime(2018, 1, 1), Salary = 3500 },
                new Worker { Name = "Wiecheć", EmploymentDate = new DateTime(2017, 2, 2), Salary = 2000 },
                new Worker { Name = "Sermak", EmploymentDate = new DateTime(2016, 3, 3), Salary = 3500 },
                new Worker { Name = "Buła", EmploymentDate = start, Salary = 4000 },
                new Worker { Name = "Pawluś", EmploymentDate = start, Salary = 15000 }
            };

            Console.WriteLine(string.Join(" ", workers));

            workers.Sort();         

            foreach(var w in workers)
                Console.WriteLine(w.ToString());

            workers.Sort(new ByEmploymentTimeThenBySalaryComparer());
            
            foreach(var w in workers)
                Console.WriteLine(Environment.NewLine+"{0} {1}", w.ToString(), w.EmploymentTime);

            workers.Sort(new BySalaryDescendingThenByEmploymentComparer());
            
            foreach(var w in workers)
                Console.WriteLine(Environment.NewLine+"{0} {1}", w.ToString(), w.EmploymentTime);                
        
            Console.WriteLine(string.Join(" ", workers.OrderBy(w => w.Salary).ThenBy(w => w.Name)));        
        
            workers.CustomSort<Worker>();

            System.Console.WriteLine(string.Join(" ", workers));

            workers.CustomSort<Worker>(new ByEmploymentTimeThenBySalaryComparer());
            
            foreach(var w in workers)
                Console.WriteLine(Environment.NewLine+"{0} {1}", w.ToString(), w.EmploymentTime);
            
            workers.CustomSort<Worker>((x, y) => {
                if(x.Salary.CompareTo(y.Salary) > 0)
                    return 1;
                else if(x.Salary.CompareTo(y.Salary) < 0)
                    return -1;
                
                return 0;
            });
            
            foreach(var w in workers)
                Console.WriteLine(Environment.NewLine+"{0} {1}", w.ToString(), w.EmploymentTime);
            
        }
    }
}
