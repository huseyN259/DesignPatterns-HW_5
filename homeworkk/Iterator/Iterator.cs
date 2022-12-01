interface IIterator<T>
{
	T Current { get; }
	bool MoveNext();
	void Reset();
}


interface IIterable<T>
{
	IIterator<T> GetEnumerator();
}


class Student
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
}


class StudentCollection : IIterable<Student>
{
	private readonly List<Student> _students = new();

	public int Count
		=> _students.Count;

	public void Add(Student student) => _students.Add(student);

	public Student this[int index]
	{
		get { return _students[index]; }
	}

	public IIterator<Student> GetEnumerator()
		=> new StudentIterator(this);
}


class StudentIterator : IIterator<Student>
{
	private int currentIndex = -1;

	private StudentCollection _students;

	public StudentIterator(StudentCollection students) 
		=> _students = students;

	public Student Current 
		=> _students[currentIndex];

	public bool MoveNext() 
		=> ++currentIndex < _students.Count;

	public void Reset() 
		=> currentIndex = -1;
}


class Program
{
	static void Main()
	{
		StudentCollection students = new();
		students.Add(new Student { Id = 9, Name = "Huseyn", Surname = "Ibrahimov" });
		students.Add(new Student { Id = 25, Name = "Nuran", Surname = "Huseynova" });

		var iterator = students.GetEnumerator();

		foreach (var s in students)
			Console.WriteLine(s.Id);

		while (iterator.MoveNext())
			Console.WriteLine((iterator.Current as Student).Name);

		foreach (var student in students)
			Console.WriteLine(student.Surname);
	}
}