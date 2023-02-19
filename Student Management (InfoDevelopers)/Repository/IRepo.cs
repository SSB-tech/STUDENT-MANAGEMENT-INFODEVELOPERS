using Student_Management__InfoDevelopers_.Models;

namespace Student_Management__InfoDevelopers_.Repository
{
	public interface IRepo
	{
		IEnumerable<students> getall();
        IEnumerable<students> getinfobyid(int id);
		bool insertinfo(students student);
		bool updateinfo(students student);
		bool deleteinfobyid(int id);
	}
}
