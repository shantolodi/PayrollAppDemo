using PayrollApp.Models;
using PayrollApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employoee
        

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetBenefit(string EmployeeName, IEnumerable<VMEmployeeDependent> EmployeeDependents)
        {
            
            dynamic result = new
            {
                flag = true,
                message = "Success",                
            };

            try
            {
                BenefitCost benefitCost = new BenefitCost(
                    new Employee { Name = EmployeeName });

                if (EmployeeDependents != null)
                {
                    var dependents = EmployeeDependents.Select(
                        x => new EmployeeDependent { Name = x.DependentName, Relation = x.DependentRelation });

                    benefitCost.SetDependents(dependents);
                }

                result = new
                {
                    flag = true,
                    message = "Success",
                    EmployeeCost = benefitCost.GetEmployeeCostPerYear(),
                    DependentCost = benefitCost.GetDependentsCostTotalPerYear(),
                    EmployeeDiscount = benefitCost.GetEmployeeDiscountPerYear(),
                    DependentDiscount = benefitCost.GetDependentsDiscountTotalPerYear(),
                    BenefitCostPerYear = benefitCost.GetBenefitCostPerYear(),
                    EmployeeSalaryPerStub = benefitCost.GetEmployeeSalaryPerPeriod(),
                    BenefitCostPerStub = benefitCost.GetBenefitCostPerPeriod(),
                    EmployeeNetSalaryPerPeriod = benefitCost.GetEmployeeNetSalaryPerPeriod()
                };
            }
            catch (Exception ex)
            {
                result = new
                {
                    flag = false,
                    message = ex.Message,
                };
            }         
                        
            return Json(result, JsonRequestBehavior.AllowGet);
        }




    }
}