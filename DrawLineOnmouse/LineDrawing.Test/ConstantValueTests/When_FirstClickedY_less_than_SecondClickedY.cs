using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawLineOnmouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineDrawing.Test.ConstantValueTests
{
	[TestClass]
	public class When_FirstClickedY_less_than_SecondClickedY
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Arrange
			var A = new Point(3, 1);
			var B = new Point(6, 7);

			//Actual
			var form1 = new Form1();
			var constantvalue = form1.GetLineEquationConstants(A, B);
			var m = constantvalue[0];
			var c = constantvalue[1];
			var allpoints = form1.GetAllPointsForDrawnLines(A, B);
			//Assert
			Assert.AreEqual(2, m);
			Assert.AreEqual(-5, c);
			Assert.AreEqual(4, allpoints.Count);


		}
	}
}
