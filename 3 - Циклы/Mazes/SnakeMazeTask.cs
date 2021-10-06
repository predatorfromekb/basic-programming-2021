namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			var iterationsCount = (height - 5) / 4;
			for (var i = -1; i < iterationsCount; i++)
			{
				if (i != -1)
				{
					Move(robot, 2, Direction.Down);
				}
				Move(robot, width - 3, Direction.Right);
				Move(robot, 2, Direction.Down);
				Move(robot, width - 3, Direction.Left);
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