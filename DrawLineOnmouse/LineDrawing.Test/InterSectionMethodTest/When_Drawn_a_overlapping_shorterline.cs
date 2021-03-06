﻿using System;
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
	public class When_Drawn_a_overlapping_shorterline
	{
		[TestMethod]
		public void TestMethod1()
		{
			//Arrange
			var A = new Point(1, 5);
			var B = new Point(5, 1);
			var C = new Point(1, 1);
			var D = new Point(4, 5);

			//Actual
			var form1 = new Form1();
			form1.PointDictionary.Add(A,B);
			var isIntersects = form1.IsIntersects(C, D);

			//Assert
			Assert.AreEqual(true,isIntersects);

		}
	}
}
