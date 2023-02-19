using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management__InfoDevelopers_.Models;
using Student_Management__InfoDevelopers_.Repository;

namespace Student_Management__InfoDevelopers_.Controllers
{
	public class StudentController : Controller
	{
		private readonly IRepo _studentDAL;

		public StudentController(IRepo studentDAL)
		{
			_studentDAL = studentDAL;
		}

		// GET: StudentController
		public ActionResult Index()
		{
			var studentlist = _studentDAL.getall().ToList();
			if (studentlist.Count==0)
			{
				TempData["InfoMessage"] = "Currently Information not available in database";
			}
			return View(studentlist);
		}


		// GET: StudentController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: StudentController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(students student)
		{
			bool isInserted = false;
			
			try
			{
                if (ModelState.IsValid)
                {
                    isInserted = _studentDAL.insertinfo(student);
                    if (isInserted)
                    {
						TempData["SuccessMessage"] = "Student information successfully inserted";
                    }
                    else
                    {
						TempData["ErrorMessage"] = "Information Already Exists/Unable to insert student information";
                    }
                }
                return RedirectToAction("Index");
			}
			catch(Exception e)
			{
				TempData["ErrorMessage"]= e.Message; 
				return View();
			}
		}

		// GET: StudentController/Edit/5
		public ActionResult Edit(int id)
		{
			var data = _studentDAL.getinfobyid(id).FirstOrDefault();
			if (data==null)
			{
				TempData["ErrorMessage"] = "Data does not exist";
				return RedirectToAction("Index");
			}
			return View(data);
		}

		// POST: StudentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(students student)
		{
			bool isUpdated = false;
			try
			{
				if (ModelState.IsValid)
				{
					isUpdated = _studentDAL.updateinfo(student);
					if (isUpdated)
					{
						TempData["SuccessMessage"] = "Data Successfully Updated";
					}
					else
					{
						TempData["ErrorMessage"] = "Data Not Updated Successfully";
					}
				}
				return RedirectToAction(("Index"));
			}
			catch(Exception e)
			{
				TempData["ErrorMessage"]=e.Message;
				return View();
			}
		}

		// GET: StudentController/Delete/5
		public ActionResult Delete(int id)
		{
			var data = _studentDAL.getinfobyid(id).FirstOrDefault();
			if (data == null)
			{
				TempData["ErrorMessage"] = "Data Not Available";
				return RedirectToAction("Index");
			}
			
			return View(data);
		}

		// POST: StudentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirm(int id)
		{
			bool isDeleted = false;
			try
			{
				if (ModelState.IsValid)
				{
					isDeleted = _studentDAL.deleteinfobyid(id);
					if(isDeleted) {
						TempData["SuccessMessage"] = "Student Information Successfully Deleted";
					}
					else
					{
						TempData["ErrorMessage"] = "Unsuccessful Delete Operation";
					}
				}
				return RedirectToAction("Index");
			}
			catch(Exception e)
			{
				TempData["ErrorMessage"]= e.Message;
				return View();
			}
		}
	}
}
