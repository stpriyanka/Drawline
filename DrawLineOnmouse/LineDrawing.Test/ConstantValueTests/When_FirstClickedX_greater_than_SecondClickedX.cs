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
			var A = new Point(85, 85);
			var B = new Point(10, 10);

			//Actual
			var form1 = new Form1();
			var constantvalue = form1.GetLineEquationConstants(A, B);
			var allpoints = form1.GetAllPointsForDrawnLines(A, B);

			//Assert
			Assert.AreEqual(1, constantvalue[0]);
			Assert.AreEqual(0, constantvalue[1]);
			Assert.AreEqual(76, allpoints.Count);

		}
	}
}
