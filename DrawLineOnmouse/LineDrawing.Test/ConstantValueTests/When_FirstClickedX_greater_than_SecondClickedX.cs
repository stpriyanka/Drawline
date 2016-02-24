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
	public class When_FirstClickedX_greater_than_SecondClickedX
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Arrange
			var A = new Point(6, 7);
			var B = new Point(2, 2);

			//Actual
			var form1 = new Form1();
			var constantvalue = form1.GetLineEquationConstants(A, B);
			var allpoints = form1.GetAllPointsForDrawnLines(A, B);

			//Assert
			Assert.AreEqual(1.25, constantvalue[0]);
			Assert.AreEqual(-.5, constantvalue[1]);
			Assert.AreEqual(5, allpoints.Count);

		}
	}
}
