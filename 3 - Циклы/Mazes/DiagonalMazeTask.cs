using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			if (width > height)
				MoveOut(robot, width - 2, height - 2,Direction.Right, Direction.Down);
			else
				MoveOut(robot, height - 2, width - 2,Direction.Down, Direction.Right);
		}

		private static void MoveOut(Robot robot, int bigSize, int smallSize, 
			Direction primaryDirection, Direction secondaryDirection)
		{
			
			var bigStep = bigSize / smallSize;

			Move(robot, bigStep, primaryDirection);
			for (var i = 0; i < smallSize - 1; i++)
			{
				Move(robot, 1, secondaryDirection);
				Move(robot, bigStep, primaryDirection);
			}
		}
		
		private static void Move(Robot robot, int stepsCount, Direction direction)
		{
			for (var i = 0; i < stepsCount; i++)
			{
				robot.MoveTo(direction);
			}
		}
	}
}