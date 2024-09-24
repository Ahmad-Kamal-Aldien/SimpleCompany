using AutoMapper;
using Compalny.R.PL.ViewModels;
using Company.R.BLL;
using Company.R.BLL.Interfaces;
using Company.R.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Compalny.R.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private IEmployeeRepository _EmployeeRepository;
        //private IDepartmentRepository _DepartmentRepository;
        //public EmployeeController(IEmployeeRepository EmployeeRepository,
        //    IDepartmentRepository departmentRepository)
        //{
        //    _EmployeeRepository = EmployeeRepository;
        //    _DepartmentRepository = departmentRepository;
        //}

        private readonly IUnitOfWork _IunitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _IunitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //public IActionResult Index(string name)
        //{
        //    var employees = Enumerable.Empty<Employee>();
        //    if (name == null)
        //    {
        //        employees = _IunitOfWork.EmployeeRepository.GetAll().ToList();
        //        return View(employees);
        //    }
        //    else
        //    {
        //        employees = _IunitOfWork.EmployeeRepository.getDataSearch(name);
        //        return View(employees);
        //    }

        //}


        //public IActionResult Index(string name)
        //{
        //    var employees = Enumerable.Empty<Employee>();
        //    if (name == null)
        //    {
        //        employees = _IunitOfWork.EmployeeRepository.GetAll().ToList();

        //    }
        //    else
        //    {
        //        employees = _IunitOfWork.EmployeeRepository.getDataSearch(name);

        //    }
        //    //var result = _mapper.Map<IEnumerator<EmployeeViewModel>>(employees);
        //    return View(employees);

        //    //var result = _mapper.Map<IEnumerator<EmployeeViewModel>>(employees);
        //    //return View(result);
        //}


        public IActionResult Index(string name)
        {
            var employees = Enumerable.Empty<Employee>();

            if (name == null)
            {
                employees = _IunitOfWork.EmployeeRepository.GetAll().ToList();

            }
            else
            {
                employees = _IunitOfWork.EmployeeRepository.getDataSearch(name);

            }
            //var result = _mapper.Map<IEnumerator<EmployeeViewModel>>(employees);
            return View(employees);

            //var result = _mapper.Map<IEnumerator<EmployeeViewModel>>(employees);
            //return View(result);
        }



        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Department"] = _IunitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult Add(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var count = _IunitOfWork.EmployeeRepository.Add(employee);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    return View(employee);

        //}


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Add(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageName = Helper.UploadFile(employee.Image, "Images");

                var emp = _mapper.Map<Employee>(employee);

                var count = _IunitOfWork.EmployeeRepository.Add(emp);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);

        }

        public IActionResult Details(int? id, string Viewname = "Details")
        {
            if (id is null) return BadRequest();

            var department = _IunitOfWork.EmployeeRepository.GetByID(id.Value);

            if (department is null) return NotFound();
            return View(Viewname, department);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Department"] = _IunitOfWork.DepartmentRepository.GetAll();

            var emp = _IunitOfWork.EmployeeRepository.GetByID(id);
            return View(emp);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Edit([FromRoute] int id, Employee employee)
        {

            if (id != employee.Id)
            {
                return BadRequest();
            }


            //employee.IsDelete = false;
            if (ModelState.IsValid)
            {

                if (employee.ImageName is not null)
                {
                    Helper.DeleteFile(employee.ImageName, "Images");
                }
                var count = _IunitOfWork.EmployeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(employee);
        }



        [HttpGet]
        public IActionResult Delete(int id)

        {
          var emp= _IunitOfWork.EmployeeRepository.GetByID(id);
            return View(emp);

        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Employee emp)

        {
            //if (id == emp.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var count = _IunitOfWork.EmployeeRepository.Delete(emp);
                if (count > 0)
                {
                    Helper.DeleteFile(emp.ImageName,"Images");
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(emp);
        }
    }
}
