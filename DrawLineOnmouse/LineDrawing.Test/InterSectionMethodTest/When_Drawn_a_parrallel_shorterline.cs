using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawLineOnmouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineDrawing.Test.InterSectionMethodTest
{
	[TestClass]
	public class When_Drawn_a_parrallel_shorterline
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Arrange
			var A = new Point(2, 3);
			var B = new Point(6, 7);
			var C = new Point(2, 2);
			var D = new Point(6, 6);

			//Actual
			var form1 = new Form1();
			form1.PointDictionary.Add(A, B);
			var isIntersects = form1.IsIntersects(C, D);

			//Assert
			Assert.AreEqual(false, isIntersects);

		}
	}
}
