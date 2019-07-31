namespace ApplicationDB
{
	class Employee
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }

		public Employee(int id, string name, string phoneNumber)
		{
			ID = id;
			Name = name;
			PhoneNumber = phoneNumber;
		}

		public Employee()
		{
			//
		}
	}
}
