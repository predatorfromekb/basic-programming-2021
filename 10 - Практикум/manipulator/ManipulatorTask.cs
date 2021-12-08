using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var xWrist = x + Manipulator.Palm * Math.Cos(Math.PI - alpha);
            var yWrist = y + Manipulator.Palm * Math.Sin(Math.PI - alpha);
            var elbow = GetElbow(xWrist, yWrist);
            var shoulder = GetShoulder(xWrist, yWrist);
            var wrist = -alpha - shoulder - elbow;
            return new[] {shoulder, elbow, wrist};
        }

        private static double GetElbow(double xWrist, double yWrist)
        {
            return TriangleTask.GetABAngle(
                Manipulator.UpperArm,
                Manipulator.Forearm,
                GetLength(xWrist, yWrist));
        }

        private static double GetShoulder(double xWrist, double yWrist)
        {
            var wristDistance = GetLength(xWrist, yWrist);
            var shoulder1 = TriangleTask.GetABAngle(
                Manipulator.UpperArm,
                wristDistance,
                Manipulator.Forearm);
            var shoulder2 = Math.Atan2(yWrist, xWrist);
            return shoulder1 + shoulder2;
        }

        private static double GetLength(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        private static readonly Random rnd = new Random();

        [Test]
        public void TestMoveManipulatorTo()
        {
            for (var i = 0; i < 300000; i++) 
            {
                var x = (rnd.NextDouble() - 0.5) * (Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm) * 2;
                var y = (rnd.NextDouble() - 0.5) * (Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm) * 2;
                var alpha = (rnd.NextDouble() - 0.5) * 20;
                var actualJointAngles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                if (double.IsNaN(actualJointAngles[0]))
                {
                    var reachableRadiusMin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
                    var reachableRadiusMax = Manipulator.UpperArm + Manipulator.Forearm;
                    var reachableCenterX = x - Manipulator.Palm * Math.Cos(alpha);
                    var reachableCenterY = y + Manipulator.Palm * Math.Sin(alpha);
                    Assert.That(reachableCenterX * reachableCenterX + reachableCenterY * reachableCenterY
                                < reachableRadiusMin * reachableRadiusMin + 1e-2
                                || reachableCenterX * reachableCenterX + reachableCenterY * reachableCenterY
                                > reachableRadiusMax * reachableRadiusMax - 1e-2);
                }
                else 
                {
                    var joints = AnglesToCoordinatesTask.GetJointPositions(
                        actualJointAngles[0], actualJointAngles[1], actualJointAngles[2]);
                    Assert.That(joints[2].X, Is.EqualTo(x).Within(1e-2));
                    Assert.That(joints[2].Y, Is.EqualTo(y).Within(1e-2));
                }
            }
        }
    }
}