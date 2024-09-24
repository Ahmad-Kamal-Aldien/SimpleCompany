using AutoMapper;
using Compalny.R.PL.ViewModels;
using Company.R.BLL.Interfaces;
using Company.R.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Compalny.R.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _Irepo;
        private readonly IUnitOfWork _IunitOfWork;

        private readonly IMapper _mapper;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _IunitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
           var dept= _IunitOfWork.DepartmentRepository.GetAll();
            return View(dept);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        //[HttpPost]
        //public IActionResult Create(Department dept)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var count = _IunitOfWork.DepartmentRepository.Add(dept);
        //        if (count > 0)
        //        {
        //            return RedirectToAction("index");
        //        }
        //    }

        //    return View(dept);
        //}

        [HttpPost]
        public IActionResult Create(DepartmentViewModel deptVM)
        {
            if (ModelState.IsValid)
            {

               var dept= _mapper.Map<Department>(deptVM);


                var count = _IunitOfWork.DepartmentRepository.Add(dept);
                if (count > 0)
                {
                    return RedirectToAction("index");
                }
            }

            return View(deptVM);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
          var dept= _IunitOfWork.DepartmentRepository.GetByID(id);

            return View(dept);
        }
        [HttpPost]
        public IActionResult Delete(Department department)
        {
            _IunitOfWork.DepartmentRepository.Delete(department);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if(id !=null) {
                
                var getDept = _IunitOfWork.DepartmentRepository.GetByID(id);
                if(getDept is not null)
                {
                    return View(getDept);

                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        //[HttpGet]
        //public IActionResult Update(int id)
        //{
        //    var getDept = _IunitOfWork.DepartmentRepository.GetByID(id);
        //    return View(getDept);
        //}

        //[HttpPost]
        //public IActionResult Update(Department dept)
        //{
        //    _IunitOfWork.DepartmentRepository.Update(dept);
        //    return View();
        //}


        [HttpGet]
        public IActionResult Update(int id)
        {
          

            var getDept = _IunitOfWork.DepartmentRepository.GetByID(id);
            return View(getDept);
        }

        [HttpPost]
        public IActionResult Update(Department dept)
        {
            _IunitOfWork.DepartmentRepository.Update(dept);
            return View();
        }
    }
}
