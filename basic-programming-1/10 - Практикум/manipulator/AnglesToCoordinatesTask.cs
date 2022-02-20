using System;
using System.Drawing;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        ///     По значению углов суставов возвращает массив координат суставов
        ///     в порядке new []{elbowPos, wristPos, palmEndPos}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = new PointF(
                (float) (Math.Cos(shoulder) * UpperArm),
                (float) (Math.Sin(shoulder) * UpperArm));
            var angle = shoulder + Math.PI + elbow;
            var wristPos =
                elbowPos +
                new SizeF(
                    (float) (Math.Cos(angle) * Forearm),
                    (float) (Math.Sin(angle) * Forearm)
                );
            angle += Math.PI + wrist;
            var palmEndPos =
                wristPos +
                new SizeF(
                    (float) (Math.Cos(angle) * Palm),
                    (float) (Math.Sin(angle) * Palm)
                );
            return new[] {elbowPos, wristPos, palmEndPos};
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Forearm + Palm, UpperArm)]
        [TestCase(0, 0, 0, UpperArm - Forearm + Palm, 0)]
        [TestCase(0, Math.PI, Math.PI, UpperArm + Forearm + Palm, 0)]
        [TestCase(-Math.PI / 2, Math.PI / 2, -Math.PI / 2, -Forearm, -UpperArm - Palm)]
        [TestCase(-Math.PI / 2, Math.PI / 2, Math.PI / 2, -Forearm, -UpperArm + Palm)]
        [TestCase(-Math.PI / 2, -Math.PI / 2, Math.PI / 2, Forearm, -UpperArm - Palm)]
        [TestCase(-Math.PI / 2, -Math.PI / 2, -Math.PI / 2, Forearm, -UpperArm + Palm)]
        [TestCase(Math.PI / 2, 0, 0, 0, -Forearm + UpperArm + Palm)]
        public void TestGetJointPositions(
            double shoulder, double elbow, double wrist,
            double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            var shoulderJoint = PointF.Empty;
            Assert.AreEqual(
                UpperArm, GetLength(shoulderJoint, joints[0]), 1e-5,
                "Distance between shoulder and elbow");
            Assert.AreEqual(
                Forearm, GetLength(joints[0], joints[1]), 1e-5,
                "Distance between elbow and wrist");
            Assert.AreEqual(
                Palm, GetLength(joints[1], joints[2]), 1e-5,
                "Distance between wrist and palm end");
        }

        private double GetLength(PointF a, PointF b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
    }
}