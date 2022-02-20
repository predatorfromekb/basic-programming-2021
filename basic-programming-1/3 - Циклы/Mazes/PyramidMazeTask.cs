namespace Mazes
{
	public static class PyramidMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			var iterationsCount = (height - 5) / 4;
			var initialWidth = width - 3;
			for (var i = -1; i < iterationsCount; i++)
			{
				if (i != -1)
				{
					Move(robot, 2, Direction.Up);
				}
				Move(robot, initialWidth, Direction.Right);
				initialWidth -= 2;
				Move(robot, 2, Direction.Up);
				Move(robot, initialWidth, Direction.Left);
				initialWidth -= 2;
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