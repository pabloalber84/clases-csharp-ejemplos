using System;

namespace bici
{
	class Bici
	{
		public string model;
		public int velocities;
		private int current_velocity;
		public void init(string m, int ve, int cv) {
			model = m;
			velocities = ve;
			if(cv > ve)
				current_velocity = ve;
			else
				current_velocity = cv;
		}
		public void print() {
			Console.WriteLine("Modelo: {0}\nVelocidad: {1}", model, current_velocity);
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Bici bici = new Bici();
			bici.init("BMX", 50, 49);
			bici.print();
		}
	}
}
