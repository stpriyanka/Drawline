using System.Drawing;
using DrawLineOnmouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineDrawing.Test.ConstantValueTests
{
	[TestClass]
	public class When_FirstClickedX_less_than_SecondClickedX
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Arrange
			var A=new Point(10,10);
			var B=new Point(85,85);

			//Actual
			var form1=new Form1();
			var constantvalue = form1.GetLineEquationConstants(A, B);
			var allpoints = form1.GetAllPointsForDrawnLines(A, B);

			//Assert
			Assert.AreEqual(1,constantvalue[0]);
			Assert.AreEqual(0, constantvalue[1]);
			Assert.AreEqual(75, allpoints.Count);

		}
	}
}
