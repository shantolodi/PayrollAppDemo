using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollApp.Models
{
    public class BenefitCost
    {
        private Employee employee;
        private IEnumerable<EmployeeDependent> dependents;
        public BenefitCost(Employee employee)
        {
            this.employee = employee;               
        }

        public void SetDependents (IEnumerable<EmployeeDependent> dependents)
        {
            this.dependents = dependents;
        }        

        public int GetTotatPayPeriod()
        {
            return 26;
        }
        public decimal GetEmployeeCostPerYear()
        {
            return 1000;
        }

        public decimal GetDependentCostPerYear()
        {
            return 500;
        }

        public decimal GetDependentsCostTotalPerYear()
        {
            decimal totalDeptCostPerYear = 0;
            decimal dependentCostPerYear = GetDependentCostPerYear();

            if (dependents != null)
            {
                foreach (var dependent in dependents)
                {
                    totalDeptCostPerYear += dependentCostPerYear;                    
                }
            }

            return totalDeptCostPerYear;
        }

        public decimal GetEmployeeDiscountPerYear()
        {
            decimal employeeDiscount = 0;
            decimal employeCostPerYear = GetEmployeeCostPerYear();

            if (employee.Name[0].ToString().ToUpper() == "A")
            {
                employeeDiscount = employeCostPerYear * 10 / 100;
            }

            return employeeDiscount;
        }

        public decimal GetDependentsDiscountTotalPerYear()
        {
            decimal dependentDiscount = 0;
            decimal dependentCostPerYear = GetDependentCostPerYear();
            if (dependents != null)
            {
                foreach (var dependent in dependents)
                {                    
                    if (dependent.Name[0].ToString().ToUpper() == "A")
                    {
                        dependentDiscount += dependentCostPerYear * 10 / 100;
                    }
                }
            }

            return dependentDiscount;
        }

        public decimal GetEmployeeSalaryPerPeriod()
        {
            return 2000;
        }

        public decimal GetBenefitCostPerYear()
        {            
            decimal employeCostPerYear = GetEmployeeCostPerYear();
            decimal totalDeptCostPerYear = GetDependentsCostTotalPerYear();
            decimal employeeDiscount = GetEmployeeDiscountPerYear();
            decimal dependentDiscount = GetDependentsDiscountTotalPerYear();

            return (employeCostPerYear + totalDeptCostPerYear - employeeDiscount - dependentDiscount);
        }

        public decimal GetBenefitCostPerPeriod()
        {
            decimal benefitCostPerYear = GetBenefitCostPerYear();
            
            int totalPeriod = GetTotatPayPeriod();

            return benefitCostPerYear / totalPeriod;
          
        }

        public decimal GetEmployeeNetSalaryPerPeriod()
        {
            return GetEmployeeSalaryPerPeriod() - GetBenefitCostPerPeriod();
        }
    }
}