namespace Bonus_Task___Lecture_4
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Instructor(int id, string name, string specialization)
        {
            InstructorId = id;
            Name = name;
            Specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"ID: {InstructorId} | Name: {Name} | Specialization: {Specialization}";
        }
    }

    
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public Course(int id, string title, Instructor instructor)
        {
            CourseId = id;
            Title = title;
            Instructor = instructor;
        }

        public string PrintDetails()
        {
            string instructorName = Instructor != null ? Instructor.Name : "No Instructor Assigned";
            return $"Course ID: {CourseId} | Title: {Title} | Instructor: {instructorName}";
        }
    }

    
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }

        public Student(int id, string name, int age)
        {
            StudentId = id;
            Name = name;
            Age = age;
            Courses = new List<Course>();
        }

        public bool Enroll(Course course)
        {
            if (Courses.Any(c => c.CourseId == course.CourseId))
            {
                return false;
            }
            Courses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            string enrolledCourses = Courses.Count > 0
                ? string.Join(", ", Courses.Select(c => c.Title))
                : "None";
            return $"Student ID: {StudentId} | Name: {Name} | Age: {Age} | Enrolled Courses: [{enrolledCourses}]";
        }
    }

    
    public class StudentManager
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Instructor> Instructors { get; set; }

        public StudentManager()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
        }

        public bool AddStudent(Student student)
        {
            if (Students.Any(s => s.StudentId == student.StudentId)) return false;
            Students.Add(student);
            return true;
        }

        public bool AddCourse(Course course)
        {
            if (Courses.Any(c => c.CourseId == course.CourseId)) return false;
            Courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (Instructors.Any(i => i.InstructorId == instructor.InstructorId)) return false;
            Instructors.Add(instructor);
            return true;
        }

        public Student FindStudent(int studentId)
        {
            return Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public Course FindCourse(int courseId)
        {
            return Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public Instructor FindInstructor(int instructorId)
        {
            return Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            var student = FindStudent(studentId);
            var course = FindCourse(courseId);

            if (student != null && course != null)
            {
                return student.Enroll(course);
            }
            return false;
        }

        public bool UpdateStudent(int id, string newName, int newAge)
        {
            var student = FindStudent(id);
            if (student != null)
            {
                student.Name = newName;
                student.Age = newAge;
                return true;
            }
            return false;
        }

        public bool DeleteStudent(int id)
        {
            var student = FindStudent(id);
            if (student != null)
            {
                Students.Remove(student);
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            bool running = true;

         
            Instructor inst1 = new Instructor(1, "ENG. Ahmed elbelkasy", "Computer Science");
            manager.AddInstructor(inst1);
            Course c1 = new Course(101, "OOP C#", inst1);
            manager.AddCourse(c1);
            manager.AddStudent(new Student(10, "Ali", 20));

            while (running)
            {
                Console.WriteLine("\n=============================================");
                Console.WriteLine("    Student Management System (OOP Task)     ");
                Console.WriteLine("\n=============================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID or Name");
                Console.WriteLine("9. Find Course by ID or Name");
                Console.WriteLine("10. Update Student Information");
                Console.WriteLine("11. Delete a Student");
                Console.WriteLine("12. [Bonus] Check if student enrolled in specific course");
                Console.WriteLine("13. [Bonus] Return instructor name by course name");
                Console.WriteLine("14. Exit");
                Console.Write("\nSelect an option (1-14): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Student ID: ");
                        int sId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Student Name: ");
                        string sName = Console.ReadLine();
                        Console.Write("Enter Student Age: ");
                        int sAge = int.Parse(Console.ReadLine());

                        if (manager.AddStudent(new Student(sId, sName, sAge)))
                            Console.WriteLine("Student added successfully.");
                        else
                            Console.WriteLine("Error: Student ID already exists.");
                        break;

                    case "2":
                        Console.Write("Enter Instructor ID: ");
                        int iId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Instructor Name: ");
                        string iName = Console.ReadLine();
                        Console.Write("Enter Specialization: ");
                        string spec = Console.ReadLine();

                        if (manager.AddInstructor(new Instructor(iId, iName, spec)))
                            Console.WriteLine("Instructor added successfully.");
                        else
                            Console.WriteLine("Error: Instructor ID already exists.");
                        break;

                    case "3":
                        Console.Write("Enter Course ID: ");
                        int cId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Instructor ID for this course: ");
                        int instId = int.Parse(Console.ReadLine());

                        Instructor instructor = manager.FindInstructor(instId);
                        if (instructor == null)
                        {
                            Console.WriteLine("Error: Instructor not found! Create instructor first.");
                        }
                        else
                        {
                            if (manager.AddCourse(new Course(cId, title, instructor)))
                                Console.WriteLine("Course added successfully.");
                            else
                                Console.WriteLine("Error: Course ID already exists.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Student ID: ");
                        int studentId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int courseId = int.Parse(Console.ReadLine());

                        if (manager.EnrollStudentInCourse(studentId, courseId))
                            Console.WriteLine("Student enrolled in course successfully.");
                        else
                            Console.WriteLine("Error: Student or Course not found, or student already enrolled.");
                        break;

                    case "5":
                        Console.WriteLine("--- List of All Students ---");
                        if (manager.Students.Count == 0) Console.WriteLine("No students found.");
                        foreach (var s in manager.Students) Console.WriteLine(s.PrintDetails());
                        break;

                    case "6":
                        Console.WriteLine("--- List of All Courses ---");
                        if (manager.Courses.Count == 0) Console.WriteLine("No courses found.");
                        foreach (var c in manager.Courses) Console.WriteLine(c.PrintDetails());
                        break;

                    case "7":
                        Console.WriteLine("--- List of All Instructors ---");
                        if (manager.Instructors.Count == 0) Console.WriteLine("No instructors found.");
                        foreach (var i in manager.Instructors) Console.WriteLine(i.PrintDetails());
                        break;

                    case "8":
                        Console.Write("Enter Student ID or Name to search: ");
                        string sSearch = Console.ReadLine();
                        var foundStudents = manager.Students.Where(s => s.StudentId.ToString() == sSearch || s.Name.Equals(sSearch, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (foundStudents.Any())
                            foreach (var s in foundStudents) Console.WriteLine(s.PrintDetails());
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "9":
                        Console.Write("Enter Course ID or Title to search: ");
                        string cSearch = Console.ReadLine();
                        var foundCourses = manager.Courses.Where(c => c.CourseId.ToString() == cSearch || c.Title.Equals(cSearch, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (foundCourses.Any())
                            foreach (var c in foundCourses) Console.WriteLine(c.PrintDetails());
                        else
                            Console.WriteLine("Course not found.");
                        break;

                    case "10":
                        Console.Write("Enter Student ID to update: ");
                        int uId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Name: ");
                        string uName = Console.ReadLine();
                        Console.Write("Enter New Age: ");
                        int uAge = int.Parse(Console.ReadLine());

                        if (manager.UpdateStudent(uId, uName, uAge))
                            Console.WriteLine("Student info updated.");
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "11":
                        Console.Write("Enter Student ID to delete: ");
                        int dId = int.Parse(Console.ReadLine());
                        if (manager.DeleteStudent(dId))
                            Console.WriteLine("Student deleted successfully.");
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "12":
                        Console.Write("Enter Student ID: ");
                        int bStudId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int bCourseId = int.Parse(Console.ReadLine());

                        var bStudent = manager.FindStudent(bStudId);
                        if (bStudent != null && bStudent.Courses.Any(c => c.CourseId == bCourseId))
                            Console.WriteLine("Yes, the student is enrolled in this course.");
                        else
                            Console.WriteLine("No, student is not enrolled or data is invalid.");
                        break;

                    case "13":
                        Console.Write("Enter Course Name: ");
                        string targetCourse = Console.ReadLine();
                        var courseObj = manager.Courses.FirstOrDefault(c => c.Title.Equals(targetCourse, StringComparison.OrdinalIgnoreCase));

                        if (courseObj != null && courseObj.Instructor != null)
                            Console.WriteLine($"The instructor for '{courseObj.Title}' is: {courseObj.Instructor.Name}");
                        else
                            Console.WriteLine("Course not found or has no instructor.");
                        break;

                    case "14":
                        running = false;
                        Console.WriteLine("Exiting program... Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}